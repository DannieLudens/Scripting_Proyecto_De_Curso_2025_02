using UnityEngine;

public class PatrolEdgeEnemy : EnemyBase
{
    [Header("Detection")]
    public Transform groundCheck;   // Posicionado al frente, cerca de los pies
    public Transform wallCheck;     // Posicionado al frente, a la altura del torso
    public float groundCheckDistance = 0.5f;
    public float wallCheckDistance = 0.1f;
    public LayerMask groundMask;

    private int dir = 1; // 1 derecha, -1 izquierda
    private void FixedUpdate()
    {
        // Mover
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        Flip(dir);

        // Raycast al suelo (hacia abajo desde groundCheck)
        bool groundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
        // Raycast a pared (hacia adelante desde wallCheck)
        Vector2 forward = new Vector2(dir, 0f);
        bool wallAhead = Physics2D.Raycast(wallCheck.position, forward, wallCheckDistance, groundMask);

        // Comprobaciones con mensajes en consola
        if (!groundAhead)
        {
            Debug.Log($"{gameObject.name} voleto porque detecto BORDE");
            dir *= -1;
        }
        else if (wallAhead)
        {
            Debug.Log($"{gameObject.name} volteo porque detecto PARED");
            dir *= -1;
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