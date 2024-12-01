using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform raypoint;
    RaycastHit2D hit;
    Ray2D ray;
    void Awake()
    {
        ray = new Ray2D(raypoint.position, raypoint.up);
    }

    void Update()
    {
        hit = Physics2D.Raycast(raypoint.position, raypoint.up, 0.5f);

        Debug.DrawRay(raypoint.position, raypoint.up * 0.5f, Color.red);

        if (hit)
        {
            Debug.Log("We hit something: " + hit.collider.name);
        }
    }
}
