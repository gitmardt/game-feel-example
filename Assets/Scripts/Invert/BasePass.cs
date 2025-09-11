using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;

public abstract class BasePass : ScriptableRenderPass
{
    string m_PassName = "BasePass";
    protected BaseSettings m_Settings;

    protected Material m_BlitMaterial;

    public void Setup(Material blitMaterial, string name)
    {
        m_BlitMaterial = blitMaterial;
        requiresIntermediateTexture = true;
        GetSettings();
        m_PassName = name;
    }

    protected bool HasBlitMaterial() => m_BlitMaterial != null;

    public abstract void UpdateSettings();
    public virtual void UpdateSettings(TextureDesc refDesc) { }
    public abstract void GetSettings();

    public T GetStackComponent<T>() where T : BaseSettings
    {
        VolumeStack stack = VolumeManager.instance.stack;
        return stack.GetComponent<T>();
    }

    public bool IsActive(BaseSettings settings)
    {
        if (settings == null || !settings.IsActive())
            return false;

        return true;
    }

    public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
    {
        if (IsActive(m_Settings))
        {
            UpdateSettings();
            Render(renderGraph, frameData);
        }
    }

    public void Render(RenderGraph renderGraph, ContextContainer frameData)
    {
        var resourceData = frameData.Get<UniversalResourceData>();

        if (resourceData.isActiveTargetBackBuffer)
        {
            Debug.LogError($"{m_PassName} requires an intermediate texture. From the current injection point it cannot access this.");
            return;
        }

        var source = resourceData.activeColorTexture;

        UpdateSettings(renderGraph.GetTextureDesc(source));

        var destinationDesc = renderGraph.GetTextureDesc(source);
        destinationDesc.name = $"CameraColor-{m_PassName}";
        destinationDesc.clearBuffer = false;

        TextureHandle destination = renderGraph.CreateTexture(destinationDesc);

        RenderGraphUtils.BlitMaterialParameters para = new(source, destination, m_BlitMaterial, 0);
        renderGraph.AddBlitPass(para, m_PassName);

        resourceData.cameraColor = destination;
    }
}
