public class RGBSplitRenderFeature : BaseRenderFeature
{
    public override string Name => "RGBSplit";

    public override void Create()
    {
        m_ScriptablePass = new RGBSplitPass();
        m_ScriptablePass.renderPassEvent = renderPassEvent;
    }
}