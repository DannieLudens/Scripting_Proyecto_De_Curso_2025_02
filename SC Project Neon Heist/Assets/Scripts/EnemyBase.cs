using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    protected Rigidbody2D rb;
    protected bool facingRight = true;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // 0 para volador
        rb.freezeRotation = true;
    }

    protected void Flip(float dirX)
    {
        if ((dirX > 0 && !facingRight) || (dirX < 0 && facingRight))
        {
            facingRight = !facingRight;
            var s = transform.localScale;
            s.x *= -1f;
            transform.localScale = s;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Flip(rb.linearVelocity.x); 
            Debug.Log("Choque con el Player, giro!");
        }
    }
}