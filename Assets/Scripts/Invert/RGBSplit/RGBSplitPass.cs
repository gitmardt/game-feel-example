using UnityEngine;

public class RGBSplitPass : BasePass
{
    public override void GetSettings() => m_Settings = GetStackComponent<RGBSplitSettings>();

    public override void UpdateSettings()
    {
         if (!HasBlitMaterial())
            return;

        var settings = m_Settings as RGBSplitSettings;

        if (settings == null) return;

        m_BlitMaterial.SetVector("_OffsetR", settings.offsetR.value);
        m_BlitMaterial.SetVector("_OffsetG", settings.offsetG.value);
        m_BlitMaterial.SetVector("_OffsetB", settings.offsetB.value);
        m_BlitMaterial.SetFloat("_Vignette", settings.vignette.value);
    }
}