using SmoothShakePro;
using UnityEngine;
using UnityEngine.VFX;

public class Shoot : MonoBehaviour
{
    [Header("Feel Settings")]
    public bool cameraShake = true;
    public bool turretShake = true;
    public bool impactVFX = true;
    public bool muzzleVFX = true;
    public bool wallDestruction = false;

    [Header("Refs")]
    public SmoothShakeManager shakeManager;
    public SmoothShakePreset rocketShoot;
    public SmoothShakePreset rocketImpact;
    public Transform barrel;
    public GameObject rocketPrefab;
    public BoxCollider wallCollider;
    public VisualEffect[] muzzleEffects;

    public Vector3 orientationOffsetEuler = Vector3.zero;

    public void ShootRocket()
    {
        wallCollider.gameObject.SetActive(true);

        Quaternion rot = barrel.rotation * Quaternion.Euler(orientationOffsetEuler);
        GameObject rocket = Instantiate(rocketPrefab, barrel.position, rot);

        if(cameraShake) shakeManager.StartShake("Camera", rocketShoot);
        if(turretShake) shakeManager.StartShake("Turret");

        if (muzzleVFX)
        {
            foreach (var effect in muzzleEffects)
            {
                effect.Play();
            }
        }

        if (rocket.TryGetComponent<Rocket>(out var r))
        {
            if(cameraShake) r.shakeManager = shakeManager;
            r.rocketImpact = rocketImpact;
            r.impactVFX = impactVFX;
            r.wallDestruction = wallDestruction;
            r.wallCollider = wallCollider;
        }
    }
}
