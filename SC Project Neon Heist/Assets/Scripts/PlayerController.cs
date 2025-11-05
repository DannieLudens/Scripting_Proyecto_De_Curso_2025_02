using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool enSuelo = false;
    private Transform plataformaActual = null;
    private Vector3 ultimaPosicionPlataforma;
    private Animator animator;
    private float horizontal = 0.0f;

    [Header("Vida del jugador")]
    [SerializeField] private int maxVida = 6;
    [SerializeField] private int vidaActual;
    [SerializeField] private Slider barraVida;
    [SerializeField] private float tiempoInvulnerable = 1f;
    private bool invulnerable = false;

    [Header("Feedback Visual de Daño")]
    [SerializeField] private float duracionParpadeo = 1f;
    [SerializeField] private float intervaloParpadeo = 0.1f;
    [SerializeField] private Color colorBarraVidaNormal = Color.green;
    [SerializeField] private Color colorBarraVidaDanio = Color.red;
    [SerializeField] private float duracionColorDanio = 0.3f;
    
    private SpriteRenderer spriteRenderer;
    private Image fillBarraVida;

    [Header("UI de Muerte")]
    [SerializeField] private Image imagenGameOver; // Imagen opcional (puede dejarse vacío si usas el GameOverManager)

    [Header("Detección")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private LayerMask groundLayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        vidaActual = maxVida;

        if (imagenGameOver != null)
            imagenGameOver.gameObject.SetActive(false);

        if (barraVida != null)
        {
            fillBarraVida = barraVida.fillRect.GetComponent<Image>();
            if (fillBarraVida != null)
            {
                fillBarraVida.color = colorBarraVidaNormal;
            }
        }

        ActualizarBarraVida();
    }

    void Update()
    {
        ProcesarMovimiento();

        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        animator.SetFloat("VelocidadY", rb.linearVelocity.y);
        animator.SetBool("enSuelo", enSuelo);
    }

    private void ProcesarMovimiento()
    {
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            horizontal = -1.0f;
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            horizontal = 1.0f;
        else
            horizontal = 0.0f;

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        if ((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            enSuelo = false;
        }

        if (horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        enSuelo = hit.collider != null;

        if (plataformaActual != null)
        {
            Vector3 deltaMovimiento = plataformaActual.position - ultimaPosicionPlataforma;
            transform.position += deltaMovimiento;
            ultimaPosicionPlataforma = plataformaActual.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            enSuelo = true;

        if (collision.collider.GetComponent<movimiento>() != null)
        {
            plataformaActual = collision.collider.transform;
            ultimaPosicionPlataforma = plataformaActual.position;
            enSuelo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<movimiento>() != null)
            plataformaActual = null;
    }

    public void TakeDamage(int damage)
    {
        if (invulnerable) return;

        vidaActual -= damage;
        vidaActual = Mathf.Clamp(vidaActual, 0, maxVida);
        Debug.Log("Jugador herido: -" + damage + " | Vida actual: " + vidaActual);
        ActualizarBarraVida();

        if (vidaActual <= 0)
        {
            Morir();
        }
        else
        {
            StartCoroutine(EfectoDanioVisual());
            StartCoroutine(InvulnerabilidadTemporal());
        }
    }

    private IEnumerator EfectoDanioVisual()
    {
        if (fillBarraVida != null)
        {
            fillBarraVida.color = colorBarraVidaDanio;
        }

        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracionParpadeo)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }

            yield return new WaitForSeconds(intervaloParpadeo);
            tiempoTranscurrido += intervaloParpadeo;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        yield return new WaitForSeconds(duracionColorDanio - duracionParpadeo);
        
        if (fillBarraVida != null)
        {
            fillBarraVida.color = colorBarraVidaNormal;
        }
    }

    private IEnumerator InvulnerabilidadTemporal()
    {
        invulnerable = true;
        yield return new WaitForSeconds(tiempoInvulnerable);
        invulnerable = false;
    }

    private void Morir()
    {
        Debug.Log("Jugador ha muerto");

        rb.linearVelocity = Vector2.zero;
        this.enabled = false;

        // Mostrar imagen de Game Over (opcional, si la tienes)
        if (imagenGameOver != null)
            imagenGameOver.gameObject.SetActive(true);

        // Activar el panel de Game Over con opciones
        GameOverManager.MostrarGameOver();
    }

    private void ActualizarBarraVida()
    {
        if (barraVida != null)
            barraVida.value = (float)vidaActual / maxVida;
    }

    public void Heal(int amount)
    {
        vidaActual += amount;
        vidaActual = Mathf.Clamp(vidaActual, 0, maxVida);
        ActualizarBarraVida();
        Debug.Log("Jugador curado: +" + amount + " | Vida actual: " + vidaActual);
        
        StartCoroutine(EfectoCuracionVisual());
    }

    private IEnumerator EfectoCuracionVisual()
    {
        if (fillBarraVida != null)
        {
            Color originalColor = fillBarraVida.color;
            fillBarraVida.color = Color.cyan;
            yield return new WaitForSeconds(0.3f);
            fillBarraVida.color = originalColor;
        }
    }

    public int GetVidaActual()
    {
        return vidaActual;
    }

    public int GetMaxVida()
    {
        return maxVida;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = enSuelo ? Color.green : Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}               