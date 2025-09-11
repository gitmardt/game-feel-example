using SmoothShakePro;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject impactPrefab;
    public GameObject trail;

    public float speed = 20f;
    public float lifetime = 5f;

    [HideInInspector] public SmoothShakeManager shakeManager;
    [HideInInspector] public SmoothShakePreset rocketImpact;
    [HideInInspector] public BoxCollider wallCollider;
    [HideInInspector] public bool impactSparks = true;
    [HideInInspector] public bool impactStar = true;
    [HideInInspector] public bool impactFire = true;
    [HideInInspector] public bool impactSmoke = true;
    [HideInInspector] public bool impactGroundCircle = true;
    [HideInInspector] public bool wallDestruction = false;
    [HideInInspector] public bool trailVFX = false;
    [HideInInspector] public bool leaveWallOn = false;

    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject impactObj = Instantiate(impactPrefab, transform.position, Quaternion.identity);

        if (impactObj.TryGetComponent<Impact>(out var impact))
        {
            impact.impactStar = impactStar;
            impact.fire = impactFire;
            impact.smoke = impactSmoke;
            impact.groundCircle = impactGroundCircle;
            impact.Initialize(wallCollider, impactSparks, leaveWallOn);
        }

        if (wallDestruction)
        {
            if (collision.gameObject.TryGetComponent<Destructible>(out var destructible))
            {
                destructible.Destroy(transform.position);
            }
        }

        //Unparent trail and destroy after 2 seconds
        if (trailVFX)
        {
            trail.transform.parent = null;
            Destroy(trail, 2f);
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
