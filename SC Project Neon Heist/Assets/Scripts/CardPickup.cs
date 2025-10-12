using UnityEngine;

public class CardPickup : MonoBehaviour
{
    public int amount = 1; // cuántas tarjetas suma este pickup

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si quien colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            // Suma tarjetas al inventario
            CollectibleManager.Instance.AddCollectible("Card", amount);

            // Destruye la tarjeta recogida
            Destroy(gameObject);
        }
    }
}
