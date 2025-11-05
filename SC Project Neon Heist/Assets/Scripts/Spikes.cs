using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Configuración de Spikes")]
    [SerializeField] private int damage = 999; // Daño letal (puedes ajustarlo)
    [SerializeField] private bool esInstantaneo = true; // Si mata instantáneamente o hace daño normal

    [Header("Configuración de Repulsión")]
    [SerializeField] private bool aplicarRepulsion = true; // Si repulsa al jugador
    [SerializeField] private float fuerzaRepulsion = 10f; // Fuerza de la repulsión
    [SerializeField] private float fuerzaRepulsionVertical = 5f; // Fuerza vertical (hacia arriba)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si quien colisionó es el jugador
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                if (esInstantaneo)
                {
                    // Muerte instantánea
                    player.TakeDamage(player.GetMaxVida());
                    Debug.Log("¡Jugador murió por los spikes!");
                }
                else
                {
                    // Daño normal
                    player.TakeDamage(damage);
                    Debug.Log("¡Jugador tocó los spikes!");

                    // Aplicar repulsión si está activada
                    if (aplicarRepulsion)
                    {
                        RepulsarJugador(other.transform);
                    }
                }
            }
        }
    }

    private void RepulsarJugador(Transform jugador)
    {
        Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();

        if (rbJugador != null)
        {
            // Calcular la dirección de repulsión (del spike hacia el jugador)
            Vector2 direccionRepulsion = (jugador.position - transform.position).normalized;

            // Aplicar fuerza en dirección opuesta al spike
            Vector2 fuerzaTotal = new Vector2(
                direccionRepulsion.x * fuerzaRepulsion,
                fuerzaRepulsionVertical // Siempre empuja hacia arriba
            );

            // Reiniciar la velocidad antes de aplicar la repulsión
            rbJugador.linearVelocity = Vector2.zero;

            // Aplicar la fuerza de repulsión
            rbJugador.AddForce(fuerzaTotal, ForceMode2D.Impulse);

            Debug.Log($"Jugador repulsado con fuerza: {fuerzaTotal}");
        }
    }
}