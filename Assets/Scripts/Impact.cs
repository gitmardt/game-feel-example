using UnityEngine;
using UnityEngine.VFX;

public class Impact : MonoBehaviour
{
    public float lifetime = 5f;
    public bool groundCircle, impactStar, fire, smoke;
    public string groundCircleProperty, impactStarProperty, fireProperty, smokeProperty;
    public BoxCollider wallCollider;
    public VisualEffect sparks, explosion;
    public BoxToVisualEffectCollider[] boxToVisualEffectColliders;

    public void Initialize(BoxCollider wallCollider, bool sparks, bool wallDestruction = false)
    {
        this.wallCollider = wallCollider;

        foreach (var boxToVisualEffectCollider in boxToVisualEffectColliders)
        {
            boxToVisualEffectCollider.boxCollider = wallCollider;
        }

        if (sparks) this.sparks.Play();
        if (explosion)
        {
            explosion.SetBool(groundCircleProperty, groundCircle);
            explosion.SetBool(impactStarProperty, impactStar);
            explosion.SetBool(fireProperty, fire);
            explosion.SetBool(smokeProperty, smoke);
            explosion.Play();
        }

        if (!wallDestruction)
        {
            wallCollider.gameObject.SetActive(false);
        }

        Destroy(gameObject, lifetime);
    }
}
