using UnityEngine;

public class TrampaVertical : MonoBehaviour
{
    public Transform puntoArriba;          // Límite superior (empty en la escena)
    public Transform puntoAbajo;           // Límite inferior (empty en la escena)
    public float velocidadBajada = 7f;     // Rápido hacia abajo
    public float velocidadSubida = 2f;     // Lento hacia arriba
    public float epsilon = 0.01f;          // Umbral para “llegó” al destino

    private enum Estado { EnEsperaArriba, Bajando, Subiendo }
    private Estado estado = Estado.EnEsperaArriba;
    private bool jugadorDentro = false;

    void Start()
    {
        if (puntoArriba == null || puntoAbajo == null)
        {
            enabled = false;
            return;
        }

        // Arranca arriba y quieta
        transform.position = puntoArriba.position;
        estado = Estado.EnEsperaArriba;
    }

    void Update()
    {
        switch (estado)
        {
            case Estado.EnEsperaArriba:
                if (jugadorDentro)
                    estado = Estado.Bajando; //se inicia el ciclo al entrar el jugador
                break;

            case Estado.Bajando:
                transform.position = Vector3.MoveTowards(
                    transform.position, puntoAbajo.position, velocidadBajada * Time.deltaTime); //Vector3.MoveTowards(posActual, posObjetivo, maxDistancia)

                if (Vector3.Distance(transform.position, puntoAbajo.position) <= epsilon)
                {
                    transform.position = puntoAbajo.position; // snap
                    estado = Estado.Subiendo;
                    // el ciclo siempre va a terminar en subida y despues verifica si el jugador esta o no dentro
                    // Si el jugador sale mientras baja: termina de bajar y luego sube; al llegar arriba, como jugadorDentro == false, se queda quieta.
                }
                break;

            case Estado.Subiendo:
                transform.position = Vector3.MoveTowards(
                    transform.position, puntoArriba.position, velocidadSubida * Time.deltaTime); //permite el movimiento hacia arriba

                if (Vector3.Distance(transform.position, puntoArriba.position) <= epsilon)
                {
                    transform.position = puntoArriba.position; // snap
                    // Si el jugador sigue dentro, repite ciclo; si no, queda quieta
                    //Si jugadorDentro == true, volvemos a Bajando (otro ciclo).
                    //Si jugadorDentro == false, pasamos a EnEsperaArriba y se queda quieta.
                    estado = jugadorDentro ? Estado.Bajando : Estado.EnEsperaArriba;
                    
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorDentro = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorDentro = false; // termina el ciclo actual y luego se detiene arriba
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("daño recibido");
            // TODO: Llama aquí al método de daño de tu PlayerController
        }
    }
}
