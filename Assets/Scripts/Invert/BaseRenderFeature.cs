using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public abstract class BaseRenderFeature : ScriptableRendererFeature
{
    public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    public Material material;

    protected BasePass m_ScriptablePass;

    public abstract string Name { get; }

    /// <inheritdoc/>
    public abstract override void Create();

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (material == null)
        {
            Debug.LogWarning($"{Name} Feature is missing a material. Please assign a material in the renderer asset.");
            return;
        }

        m_ScriptablePass.Setup(material, Name);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}
