using SmoothShakePro;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    public SmoothShakeManager shakeManager;
    public SmoothShakePreset rocketImpact;

    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
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
