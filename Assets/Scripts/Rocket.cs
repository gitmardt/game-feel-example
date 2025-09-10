using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Rocket collided with " + collision.gameObject.name);
    }

    void OnEnable()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
