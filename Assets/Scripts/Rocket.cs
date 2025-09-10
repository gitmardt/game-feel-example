using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;        
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }
}
