using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[VolumeComponentMenu("Post-processing Custom/Invert")]
[VolumeRequiresRendererFeatures(typeof(InvertRenderFeature))]
public class InvertSettings : BaseSettings
{
    public TextureParameter maskTexture = new TextureParameter(null);
    public ClampedFloatParameter maskThreshold = new ClampedFloatParameter(0f, 0f, 1f);
    public ClampedFloatParameter invert = new ClampedFloatParameter(0f, 0f, 1f);
    public ClampedFloatParameter vignette = new ClampedFloatParameter(0f, 0f, 1f);

    public override bool IsActive() => active && invert.value > 0f;
}