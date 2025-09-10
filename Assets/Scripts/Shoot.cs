using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform barrel;
    public GameObject rocketPrefab;
    public float rocketSpeed = 20f;
    public float rocketLifetime = 5f;

    public Vector3 orientationOffsetEuler = Vector3.zero;

    public void ShootRocket()
    {
        Quaternion rot = barrel.rotation * Quaternion.Euler(orientationOffsetEuler);
        GameObject rocket = Instantiate(rocketPrefab, barrel.position, rot);

        if (rocket.TryGetComponent<Rocket>(out var r))
        {
            r.speed = rocketSpeed;
            r.lifetime = rocketLifetime;
        }
    }
}
