using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int damage = 1; // Daño que causa la bomba
    [SerializeField] private float lifetime = 5f; // Por si no impacta, se destruye sola

    private void Start()
    {
        // Autodestruirse después de unos segundos para no llenar la escena
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si golpea al jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }

        // Destruir la bomba al impactar con cualquier cosa
        Destroy(gameObject);
    }
}
