using UnityEngine;

public class TrampaVertical : MonoBehaviour
{
    public Transform puntoArriba;
    public Transform puntoAbajo;
    public float velocidadBajada = 7f;
    public float velocidadSubida = 2f;
    public float epsilon = 0.01f;
    public int damage = 20;

    private enum Estado { EnEsperaArriba, Bajando, Subiendo }
    private Estado estado = Estado.EnEsperaArriba;
    private bool jugadorDentro = false;

    [Header("Animación de la prensa")]
    public Animator animator; // 👉 arrastra aquí el Animator de la prensa

    void Start()
    {
        if (puntoArriba == null || puntoAbajo == null)
        {
            enabled = false;
            return;
        }

        transform.position = puntoArriba.position;
        estado = Estado.EnEsperaArriba;

        // Opcional: si el Animator está en un hijo puedes buscarlo automáticamente
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float distanciaTotal = Vector3.Distance(puntoArriba.position, puntoAbajo.position);

        switch (estado)
        {
            case Estado.EnEsperaArriba:
                if (jugadorDentro)
                {
                    estado = Estado.Bajando;

                    // Calcular duración del movimiento
                    float tiempoBajada = distanciaTotal / velocidadBajada;

                    // Reproducir animación sincronizada
                    animator.Play("Hammer_Down", 0, 0);
                    animator.speed = 1f / tiempoBajada; // Ajusta velocidad para que dure igual que el recorrido
                }
                break;

            case Estado.Bajando:
                transform.position = Vector3.MoveTowards(
                    transform.position, puntoAbajo.position, velocidadBajada * Time.deltaTime);

                if (Vector3.Distance(transform.position, puntoAbajo.position) <= epsilon)
                {
                    transform.position = puntoAbajo.position;
                    estado = Estado.Subiendo;

                    float tiempoSubida = distanciaTotal / velocidadSubida;
                    animator.Play("Hammer_Up", 0, 0);
                    animator.speed = 1f / tiempoSubida;
                }
                break;

            case Estado.Subiendo:
                transform.position = Vector3.MoveTowards(
                    transform.position, puntoArriba.position, velocidadSubida * Time.deltaTime);

                if (Vector3.Distance(transform.position, puntoArriba.position) <= epsilon)
                {
                    transform.position = puntoArriba.position;
                    estado = jugadorDentro ? Estado.Bajando : Estado.EnEsperaArriba;

                    if (estado == Estado.Bajando)
                    {
                        float tiempoBajada = distanciaTotal / velocidadBajada;
                        animator.Play("Hammer_Down", 0, 0);
                        animator.speed = 1f / tiempoBajada;
                    }
                    else
                    {
                        // Volvemos a estado idle
                        animator.Play("Hammer_Idle");
                        animator.speed = 1f;
                    }
                }
                break;
        }
    }


    void ActualizarAnimacion(string estadoAnim)
    {
        if (animator == null) return;

        animator.ResetTrigger("Quieto");
        animator.ResetTrigger("Caer");
        animator.ResetTrigger("Subir");
        animator.SetTrigger(estadoAnim);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorDentro = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorDentro = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
                player.TakeDamage(damage);
        }
    }
}
