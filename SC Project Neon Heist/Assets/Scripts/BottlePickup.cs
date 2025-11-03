using UnityEngine;

public class BottlePickup : MonoBehaviour
{
    public int amount = 1; // cuánta vida suma este pickup

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si quien colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            // Suma tarjetas al inventario
            CollectibleManager.Instance.AddCollectible("Life", amount);

            // Destruye la tarjeta recogida
            Destroy(gameObject);
        }
    }
}
