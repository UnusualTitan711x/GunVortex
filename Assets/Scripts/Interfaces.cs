using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public interface IDamagable
{
    void TakeDamage(int damage);
}
