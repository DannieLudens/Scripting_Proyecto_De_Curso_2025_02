using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;      // Velocidad horizontal. Cuántas unidades por segundo quieres que se mueva al 100% (cuando horizontal es -1 o 1)
    public float jumpForce = 7f;      // Fuerza del salto

    private Rigidbody2D rb; //variable para luego referenciar el rigidbody
    private bool isGrounded = false;  // Saber si está en el suelo
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Referencia al Rigidbody2D del jugador
    }

    void Update()
    {
        // Movimiento horizontal
        float horizontal = 0.0f; //variable para indicar la dirección del movimiento horizontal
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            horizontal = -1.0f; // si se presiona izq o tecla a, va a la izquierda (hacia el lado de los negativos en el plano cartesiano)
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            horizontal = 1.0f; // si se presiona der o tecla d, va a la derecha (hacia el lado de los positivos en el plano cartesiano)
        }

        // Aplicar velocidad horizontal manteniendo la vertical
        //linearVelocity: es la velocidad del rigidbody en 2D, medida en unidades por segundo.
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y); //new Vector2(x, y): crea un vector 2D con esas dos componentes. Entonces se coloca 
        //Conserva la velocidad vertical actual colocando rb.linearVelocity.y  No tocamos la Y para no romper la gravedad ni el salto. Si estabas cayendo o subiendo por un salto, eso sigue igual.


        // --- Salto ---
        if ((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && isGrounded) // si está en el suelo y se presiona la tecla arriba o w
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // se mantiene la velocidad horizontal en x y en y se impulsa al personaje hacia arriba
            isGrounded = false; // Ya no está en el suelo
        }
    }

    // Detecta si está en contacto con el suelo
    private void OnCollisionEnter2D(Collision2D collision) // Este método especial de Unity se llama automáticamente cuando el Rigidbody2D del jugador choca con algo que tenga un collider.
    {
        if (collision.collider.CompareTag("Ground")) // si el objeto con el que chocaste tiene el tag de suelo
        {
            isGrounded = true;// es porque estás en el suelo y puedes saltar
        }
    }
}
