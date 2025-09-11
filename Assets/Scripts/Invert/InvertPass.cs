public class InvertPass : BasePass
{
    public override void GetSettings() => m_Settings = GetStackComponent<InvertSettings>();

    public override void UpdateSettings()
    {
         if (!HasBlitMaterial())
            return;

        var settings = m_Settings as InvertSettings;

        if (settings == null) return;

        m_BlitMaterial.SetTexture("_MaskTexture", settings.maskTexture.value);
        m_BlitMaterial.SetFloat("_MaskThreshold", settings.maskThreshold.value);
        m_BlitMaterial.SetFloat("_Invert", settings.invert.value);
        m_BlitMaterial.SetFloat("_Vignette", settings.vignette.value);
    }
}