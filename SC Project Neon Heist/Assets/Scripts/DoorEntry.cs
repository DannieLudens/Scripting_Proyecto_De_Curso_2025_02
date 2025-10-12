using UnityEngine;

public class DoorEntry : MonoBehaviour
{
    public bool IsOpen { get; private set; } = false;
    private Animator animator;
    private Collider2D doorCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }

    public void OpenDoor()
    {
        if (IsOpen) return;

        // Lanza la animación de abrir
        animator.SetTrigger("OpenDoor");
        IsOpen = true;

        // Opcional: desactivar el collider para que no bloquee más al jugador
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
    }
}
