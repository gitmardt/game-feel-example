public class InvertPass : BasePass
{
    public override void GetSettings() => m_Settings = GetStackComponent<InvertSettings>();

    public override void UpdateSettings()
    {
         if (!HasBlitMaterial())
            return;

        var settings = m_Settings as InvertSettings;

        if (settings == null) return;

        m_BlitMaterial.SetFloat("_Invert", settings.invert.value);
        m_BlitMaterial.SetFloat("_Vignette", settings.vignette.value);
        m_BlitMaterial.SetColor("_Tint", settings.tint.value);
        m_BlitMaterial.SetFloat("_Contrast", settings.contrast.value);
    }
}