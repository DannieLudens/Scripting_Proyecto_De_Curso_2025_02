using UnityEngine;
using System.Collections;

public class FlyingEnemy : EnemyBase
{
    [Header("Patrol")]
    public Transform leftPoint;
    public Transform rightPoint;
    public float hoverY = 4f;
    public float patrolSpeed = 1.5f;

    [Header("Detection & Attack")]
    public float detectRadius = 6f;
    public float alignThreshold = 0.3f;
    public LayerMask playerMask;
    public Transform bombSpawn;
    public GameObject bombPrefab;
    public float bombCooldown = 1.2f;

    private enum State { Patrol, Chase, Attack }
    private State state = State.Patrol;
    private Transform player;
    private bool canDrop = true;
    private int patrolDir = 1;

    protected override void Awake()
    {
        base.Awake();
        rb.gravityScale = 0f; // No le afecta la gravedad
    }

    private void Update()
    {
        DetectPlayer();

        switch (state)
        {
            case State.Patrol:
                Patrol();
                if (PlayerInRange()) state = State.Chase;
                break;

            case State.Chase:
                Chase();
                if (!PlayerInRange()) state = State.Patrol;
                else if (AlignedWithPlayerX()) state = State.Attack;
                break;

            case State.Attack:
                Chase(); // Mantener alineación
                if (!PlayerInRange()) state = State.Patrol;
                else
                {
                    if (AlignedWithPlayerX()) TryDropBomb();
                    else state = State.Chase;
                }
                break;
        }
    }

    private void Patrol()
    {
        float targetX = transform.position.x + patrolDir * patrolSpeed * Time.deltaTime * 60f * 0.02f;
        if (transform.position.x >= rightPoint.position.x) patrolDir = -1;
        if (transform.position.x <= leftPoint.position.x) patrolDir = 1;

        Vector2 targetPos = new Vector2(targetX, hoverY);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        Flip(patrolDir);
    }

    private void Chase()
    {
        if (player == null) return;
        Vector2 targetPos = new Vector2(player.position.x, hoverY);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        Flip(Mathf.Sign(player.position.x - transform.position.x));
    }

    private void DetectPlayer()
    {
        // Buscar si hay un jugador dentro del radio
        var hit = Physics2D.OverlapCircle(transform.position, detectRadius, playerMask);

        if (hit)
        {
            // Si se detecta un jugador, guardarlo
            player = hit.transform;
        }
        else
        {
            // Si no hay jugador en el rango, limpiar la referencia
            player = null;
        }
    }


    private bool PlayerInRange()
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.position) <= detectRadius;
    }

    private bool AlignedWithPlayerX()
    {
        if (player == null) return false;
        return Mathf.Abs(player.position.x - transform.position.x) <= alignThreshold;
    }

    private void TryDropBomb()
    {
        if (!canDrop || bombPrefab == null || bombSpawn == null) return;
        Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
        Debug.Log($"{gameObject.name} lanzó una bomba");
        StartCoroutine(BombCooldown());
    }

    private IEnumerator BombCooldown()
    {
        canDrop = false;
        yield return new WaitForSeconds(bombCooldown);
        canDrop = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.5f, 0, 0.25f);
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        if (leftPoint != null && rightPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(leftPoint.position, rightPoint.position);
        }
    }
}