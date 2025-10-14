using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool enSuelo = false;
    private Transform plataformaActual = null;
    private Vector3 ultimaPosicionPlataforma;

    private Animator animator;
    private float horizontal = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcesarMovimiento();

        // Conexión con tu Animator
        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        animator.SetFloat("VelocidadY", rb.linearVelocity.y);
        animator.SetBool("enSuelo", enSuelo);
    }

    private void ProcesarMovimiento()
    {
        // Movimiento lateral
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            horizontal = -1.0f;
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            horizontal = 1.0f;
        else
            horizontal = 0.0f;

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        // Salto
        if ((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            enSuelo = false;
        }

        // Voltear el sprite según dirección
        if (horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        if (plataformaActual != null)
        {
            Vector3 deltaMovimiento = plataformaActual.position - ultimaPosicionPlataforma;
            transform.position += deltaMovimiento;
            ultimaPosicionPlataforma = plataformaActual.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta suelo
        if (collision.collider.CompareTag("Ground"))
            enSuelo = true;

        // Detecta plataformas móviles
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
}
