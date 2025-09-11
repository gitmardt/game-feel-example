using SmoothShakePro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class Shoot : MonoBehaviour
{
    [Header("Feel Settings")]
    public bool cameraShake = true;
    public bool turretShake = true;
    public bool muzzleVFX = true;
    public bool trailVFX = true;
    public bool wallDestruction = false;
    public bool leaveWallOn = false;
    public bool slowMotion = false;
    public bool impactFrame = false;

    [Header("Feel impact settings")]
    public bool impactSparks = true;
    public bool impactStar = true;
    public bool impactFire = true;
    public bool impactSmoke = true;
    public bool impactGroundCircle = true;

    [Header("Refs")]
    public SmoothShakeManager shakeManager;
    public SmoothShakePreset rocketShoot;
    public SmoothShakePreset rocketImpact;
    public Transform barrel;
    public GameObject rocketPrefab;
    public BoxCollider wallCollider;
    public VisualEffect[] muzzleEffects;
    public Volume impactFrameVolume;

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
            r.impactStar = impactStar;
            r.impactFire = impactFire;
            r.impactSmoke = impactSmoke;
            r.impactGroundCircle = impactGroundCircle;
            r.impactSparks = impactSparks;
            r.rocketImpact = rocketImpact;
            r.wallDestruction = wallDestruction;
            r.wallCollider = wallCollider;
            r.leaveWallOn = leaveWallOn;
            r.slowMotion = slowMotion;
            r.impactFrameVolume = impactFrameVolume; 
            r.impactFrame = impactFrame;
            if (!trailVFX) r.trail.SetActive(false);
        }
    }
}
