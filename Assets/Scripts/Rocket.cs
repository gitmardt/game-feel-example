using SmoothShakePro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Rocket : MonoBehaviour
{
    public GameObject impactPrefab;
    public GameObject trail;

    public float speed = 20f;
    public float lifetime = 5f;
    public float slowMotionDuration = 0.5f;
    public float slowMotionScale = 0.2f;
    public float impactFrameDuration = 0.5f;

    [HideInInspector] public SmoothShakeManager shakeManager;
    [HideInInspector] public SmoothShakePreset rocketImpact;
    [HideInInspector] public BoxCollider wallCollider;
    [HideInInspector] public Volume impactFrameVolume;
    [HideInInspector] public bool impactSparks = true;
    [HideInInspector] public bool impactStar = true;
    [HideInInspector] public bool impactFire = true;
    [HideInInspector] public bool impactSmoke = true;
    [HideInInspector] public bool impactGroundCircle = true;
    [HideInInspector] public bool wallDestruction = false;
    [HideInInspector] public bool trailVFX = false;
    [HideInInspector] public bool leaveWallOn = false;
    [HideInInspector] public bool slowMotion = false;
    [HideInInspector] public bool impactFrame = false;

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

        if (impactFrame && impactFrameVolume)
        {
            var helper = impactFrameVolume.gameObject.GetComponent<ImpactFramePulseHelper>();
            if (!helper) helper = impactFrameVolume.gameObject.AddComponent<ImpactFramePulseHelper>();

            helper.PulseNow(impactFrameVolume, impactFrameDuration);
        }

        if (wallDestruction)
        {
            if (collision.gameObject.TryGetComponent<Destructible>(out var destructible))
            {
                destructible.Destroy(transform.position);
            }
        }

        if (slowMotion)
        {
            SlowMotion.SlowMoPulse(slowMotionScale, slowMotionDuration);
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

    private class ImpactFramePulseHelper : MonoBehaviour
    {
        private Coroutine running;

        public void PulseNow(Volume vol, float duration)
        {
            if (running != null) StopCoroutine(running);
            running = StartCoroutine(Pulse(vol, duration));
        }

        private System.Collections.IEnumerator Pulse(Volume vol, float duration)
        {
            if (!vol) yield break;

            vol.weight = 1f;
            float t = 0f;

            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                yield return null;
            }

            if (vol) vol.weight = 0f;
            running = null;
        }
    }
}
