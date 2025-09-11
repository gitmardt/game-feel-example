public class InvertRenderFeature : BaseRenderFeature
{
    public override string Name => "Invert";

    public override void Create()
    {
        m_ScriptablePass = new InvertPass();
        m_ScriptablePass.renderPassEvent = renderPassEvent;
    }
}