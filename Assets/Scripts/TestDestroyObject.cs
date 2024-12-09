using UnityEngine;

public class TestDestroyObject : MonoBehaviour, IInteractable, IDamagable
{
    public int health = 100;

    public void Interact()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (health > damage) health -= damage;
        else Destroy(this.gameObject);
    }
}
