using SmoothShakePro;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Feel Settings")]
    public bool cameraShake = true;
    public bool turretShake = true;

    [Header("Refs")]
    public SmoothShakeManager shakeManager;
    public SmoothShakePreset rocketShoot;
    public SmoothShakePreset rocketImpact;
    public Transform barrel;
    public GameObject rocketPrefab;

    public Vector3 orientationOffsetEuler = Vector3.zero;

    public void ShootRocket()
    {
        Quaternion rot = barrel.rotation * Quaternion.Euler(orientationOffsetEuler);
        GameObject rocket = Instantiate(rocketPrefab, barrel.position, rot);

        if(cameraShake) shakeManager.StartShake("Camera", rocketShoot);
        if(turretShake) shakeManager.StartShake("Turret");

        if (rocket.TryGetComponent<Rocket>(out var r))
        {
            if(cameraShake) r.shakeManager = shakeManager;
            r.rocketImpact = rocketImpact;
        }
    }
}
