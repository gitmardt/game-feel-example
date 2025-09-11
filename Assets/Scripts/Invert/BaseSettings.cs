using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

[Serializable]
[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
public abstract class BaseSettings : VolumeComponent
{
    public abstract bool IsActive();
}
