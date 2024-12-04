using StarterAssets;
using UnityEngine;

public class AnimationEventForwarder : MonoBehaviour
{
    public ThirdPersonController parentController;

    private void Start()
    {
        // Encuentra autom�ticamente el controlador del padre si no est� asignado
        if (parentController == null)
        {
            parentController = GetComponentInParent<ThirdPersonController>();
        }
    }

    public void OnFootstep(AnimationEvent animationEvent)
    {
        parentController?.OnFootstep(animationEvent);
    }

    public void OnLand(AnimationEvent animationEvent)
    {
        parentController?.OnLand(animationEvent);
    }

}
