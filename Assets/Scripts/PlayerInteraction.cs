using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform raypoint;
    public float range = 1f;
    RaycastHit2D hit;
    Ray2D ray;
    int interactableLayer;
    void Awake()
    {
        ray = new Ray2D(raypoint.position, raypoint.up);
        interactableLayer = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        hit = Physics2D.Raycast(raypoint.position, raypoint.up, range, interactableLayer);
        Debug.DrawRay(raypoint.position, raypoint.up * range, Color.red);

        if (hit)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
    }
}
