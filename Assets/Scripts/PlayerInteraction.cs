using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform raypoint;
    public float range = 1f;
    RaycastHit2D hit;
    Ray2D ray;
    int interactableLayer;
    IInteractable interactable;
    void Awake()
    {
        ray = new Ray2D(raypoint.position, raypoint.up);
        interactableLayer = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        hit = Physics2D.Raycast(raypoint.position, raypoint.up, range, interactableLayer);
        Debug.DrawRay(raypoint.position, raypoint.up * range, Color.red);

        if (hit && hit.collider.GetComponent<IInteractable>() != null)
        {
            UI_Manager.instance.interactButton.gameObject.SetActive(true); 
        }
        else
        {
            UI_Manager.instance.interactButton.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {

        hit = Physics2D.Raycast(raypoint.position, raypoint.up, range, interactableLayer);
        Debug.DrawRay(raypoint.position, raypoint.up * range, Color.red);

        if (hit)
        {
            interactable = hit.collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                Debug.Log("Interacting");
                interactable.Interact();
            }
        }
    }
}
