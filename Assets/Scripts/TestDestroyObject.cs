using UnityEngine;

public class TestDestroyObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(this.gameObject);
    }
}
