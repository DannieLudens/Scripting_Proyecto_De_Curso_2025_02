using UnityEngine;

public class DoorDetector : MonoBehaviour
{
    public DoorEntry door;         // Referencia a la puerta asociada
    public int requiredCards = 1;  // Cuántas tarjetas pide esta puerta

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && door != null)
        {
            // Si ya está abierta, no hacer nada
            if (door.IsOpen) return;

            int currentCards = CollectibleManager.Instance.GetCollectibleCount("Card");

            if (currentCards >= requiredCards)
            {
                // Gastar tarjetas
                CollectibleManager.Instance.WithdrawCollectible("Card", requiredCards);

                // Abrir la puerta
                door.OpenDoor();
            }
            else
            {
                Debug.Log("No tienes tarjetas suficientes para abrir esta puerta.");
            }
        }
    }
}
