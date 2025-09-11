using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[VolumeComponentMenu("Post-processing Custom/Invert")]
[VolumeRequiresRendererFeatures(typeof(InvertRenderFeature))]
public class InvertSettings : BaseSettings
{
    public ClampedFloatParameter invert = new ClampedFloatParameter(0f, 0f, 1f);
    public ClampedFloatParameter vignette = new ClampedFloatParameter(0f, 0f, 1f);
    public ColorParameter tint = new ColorParameter(Color.black, true, false, true);
    public FloatParameter contrast = new FloatParameter(1f);

    public override bool IsActive() => active && invert.value > 0f;
}