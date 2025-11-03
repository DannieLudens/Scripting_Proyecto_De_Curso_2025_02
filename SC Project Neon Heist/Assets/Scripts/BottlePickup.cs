using UnityEngine;

public class BottlePickup : MonoBehaviour
{
    public int amount = 1; // cuánto cura esta botella (1 = media vida, 2 = una vida completa)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                // Si la vida no está al máximo, se recoge y cura
                if (player.GetVidaActual() < player.GetMaxVida())
                {
                    player.Heal(amount);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Vida completa: no se puede recoger la botella.");
                }
            }
        }
    }
}
