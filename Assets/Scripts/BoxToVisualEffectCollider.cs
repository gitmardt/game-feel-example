using UnityEngine;
using UnityEngine.VFX;

public class BoxToVisualEffectCollider : MonoBehaviour
{
    public BoxCollider boxCollider;
    public VisualEffect visualEffect;
    public string positionProperty = "BoxColliderPosition";
    public string sizeProperty = "BoxColliderSize";
    public string rotationProperty = "BoxColliderRotation";

    private void Update()
    {
        if (boxCollider == null || visualEffect == null) return;

        visualEffect.SetVector3(positionProperty, (boxCollider.transform.position + boxCollider.center) - visualEffect.transform.position);
        visualEffect.SetVector3(sizeProperty, boxCollider.size);
        visualEffect.SetVector3(rotationProperty, boxCollider.transform.eulerAngles);
    }

    //Gizmos to visualize the box collider in the editor
    private void OnDrawGizmosSelected()
    {
        if (boxCollider == null) return;
        Gizmos.color = Color.cyan;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(boxCollider.transform.position + boxCollider.center, boxCollider.transform.rotation, boxCollider.transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, boxCollider.size);
    }
}
