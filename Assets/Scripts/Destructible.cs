using UnityEngine;

public class Destructible : MonoBehaviour
{
    public MeshRenderer defaultMeshRenderer;
    public Collider defaultCollider;            
    public Transform piecesParent;
    public Rigidbody[] pieces;

    [Header("Explosion")]
    public float explosionForce = 200f;
    public float explosionRadius = 5f;
    public float upwardsModifier = 0.5f;      
    public float randomTorque = 2f;             

    public void Destroy(Vector3 impactPoint)
    {
        if (defaultMeshRenderer) defaultMeshRenderer.enabled = false;
        if (defaultCollider) defaultCollider.enabled = false;

        if (piecesParent) piecesParent.gameObject.SetActive(true);

        foreach (var rb in pieces)
        {
            if (!rb) continue;

            rb.isKinematic = false;
            rb.detectCollisions = true;     
            rb.WakeUp();

            rb.AddExplosionForce(explosionForce, impactPoint, explosionRadius, upwardsModifier, ForceMode.Impulse);

            if (randomTorque > 0f)
            {
                Vector3 t = Random.onUnitSphere * randomTorque;
                rb.AddTorque(t, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(piecesParent ? piecesParent.position : transform.position, explosionRadius);
    }
}
