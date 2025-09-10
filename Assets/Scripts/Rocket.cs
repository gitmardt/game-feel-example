using SmoothShakePro;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject impactPrefab;

    public float speed = 20f;
    public float lifetime = 5f;

    [HideInInspector] public SmoothShakeManager shakeManager;
    [HideInInspector] public SmoothShakePreset rocketImpact;
    [HideInInspector] public BoxCollider wallCollider;
    [HideInInspector] public bool impactVFX = false;
    [HideInInspector] public bool wallDestruction = false;

    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject impactObj = Instantiate(impactPrefab, transform.position, Quaternion.identity);

        if (impactObj.TryGetComponent<Impact>(out var impact))
        {
            impact.Initialize(wallCollider, impactVFX, wallDestruction);
        }

        Destroy(gameObject);  
        if(shakeManager) shakeManager.StartShake("Camera", rocketImpact);
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;        
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }
}
