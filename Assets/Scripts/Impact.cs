using UnityEngine;
using UnityEngine.VFX;

public class Impact : MonoBehaviour
{
    public float lifetime = 5f;
    public BoxCollider wallCollider;
    public VisualEffect[] effects;
    public BoxToVisualEffectCollider[] boxToVisualEffectColliders;

    public void Initialize(BoxCollider wallCollider, bool impactVFX, bool wallDestruction = false)
    {
        this.wallCollider = wallCollider;

        foreach (var boxToVisualEffectCollider in boxToVisualEffectColliders)
        {
            boxToVisualEffectCollider.boxCollider = wallCollider;
        }

        if (impactVFX)
        {
            foreach (var effect in effects)
            {
                effect.Play();
            }
        }

        if (!wallDestruction)
        {
            wallCollider.gameObject.SetActive(false);
        }
        else
        {
            //Proper wall destruction
        }

        Destroy(gameObject, lifetime);
    }
}
