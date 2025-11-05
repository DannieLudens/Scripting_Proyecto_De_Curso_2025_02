using UnityEngine;

public class PatrolEdgeEnemy : EnemyBase
{
    [Header("Detection")]
    public Transform groundCheck;   // Posicionado al frente, cerca de los pies
    public Transform wallCheck;     // Posicionado al frente, a la altura del torso
    public float groundCheckDistance = 0.5f;
    public float wallCheckDistance = 0.1f;
    public LayerMask groundMask;

    [Header("Ataque")]
    [SerializeField] private int damage = 1;

    private int dir = 1; // 1 derecha, -1 izquierda

    [Header("Animacion")]
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        Flip(dir);

        bool groundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
        Vector2 forward = new Vector2(dir, 0f);
        bool wallAhead = Physics2D.Raycast(wallCheck.position, forward, wallCheckDistance, groundMask);

        if (!groundAhead)
        {
            Debug.Log($"{gameObject.name} volteo porque detectó BORDE");
            dir *= -1;
        }
        else if (wallAhead)
        {
            Debug.Log($"{gameObject.name} volteo porque detectó PARED");
            dir *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Causar daño al jugador
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Ya no cambia de dirección al golpear al jugador
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
        if (wallCheck != null)
        {
            Gizmos.color = Color.magenta;
            var forward = new Vector3(wallCheck.localScale.x >= 0 ? 1 : -1, 0, 0);
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + forward.normalized * wallCheckDistance);
        }
    }
}