using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[VolumeComponentMenu("Post-processing Custom/RGBSplit")]
[VolumeRequiresRendererFeatures(typeof(RGBSplitRenderFeature))]
public class RGBSplitSettings : BaseSettings
{
    public BoolParameter enabled = new(false);
    public Vector2Parameter offsetR = new(new Vector2(0f, 0f));
    public Vector2Parameter offsetG = new(new Vector2(0f, 0f));
    public Vector2Parameter offsetB = new(new Vector2(0f, 0f));
    public ClampedFloatParameter vignette = new(0, 0, 10);

    public override bool IsActive() => active && enabled.value;
}