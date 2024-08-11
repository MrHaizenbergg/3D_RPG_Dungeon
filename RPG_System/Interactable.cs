using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    public float Radius { get { return radius; } }

    [SerializeField] private protected Transform interactionTransform;

    public bool isFocus { get; set; } = false;
    public bool hasInteracted { get; set; } = false;
    private protected Transform player;

    private float distance;

    public virtual void Interact()
    {
        //Debug.Log("Interacting with: " + transform.name);
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
        Debug.Log("GO Name: "+player.name);
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    protected virtual void Update()
    {
        if (isFocus && !hasInteracted)
        {
            distance = Vector3.Distance(player.position, interactionTransform.position);

            //distance = ((player.position - interactionTransform.position).sqrMagnitude);

            if (distance <= radius)
            {
                Interact();

                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
