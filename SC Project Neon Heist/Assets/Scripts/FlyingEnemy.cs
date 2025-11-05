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
    [SerializeField] private float detectRadius = 8f; 
    [SerializeField] private float alignThreshold = 1.5f; // Rango horizontal para detectar al jugador debajo
    public LayerMask playerMask;
    public Transform bombSpawn;
    public GameObject bombPrefab;
    [SerializeField] private float bombCooldown = 2f;

    private Transform player;
    private bool canDrop = true;
    private int patrolDir = 1;

    protected override void Awake()
    {
        base.Awake();
        rb.gravityScale = 0f; // No le afecta la gravedad
        
        if (hoverY == 0) hoverY = transform.position.y;
    }

    private void Start()
    {
        if (leftPoint == null || rightPoint == null)
        {
            Debug.LogError($"{gameObject.name}: No se asignaron leftPoint o rightPoint en el Inspector!");
            enabled = false;
            return;
        }

        if (bombSpawn == null)
        {
            Debug.LogWarning($"{gameObject.name}: No se asignó BombSpawn. Creando uno automáticamente...");
            GameObject spawnObj = new GameObject("BombSpawnPoint");
            spawnObj.transform.SetParent(transform);
            spawnObj.transform.localPosition = new Vector3(0, -0.5f, 0);
            bombSpawn = spawnObj.transform;
        }
    }

    private void Update()
    {
        // Siempre patrullar entre los puntos
        Patrol();
        
        // Detectar si el jugador está debajo
        DetectPlayerBelow();
        
        // Intentar soltar bomba si el jugador está debajo y alineado
        if (player != null && IsPlayerBelowAndAligned())
        {
            TryDropBomb();
        }
    }

    private void Patrol()
    {
        // Calcular movimiento horizontal
        float targetX = transform.position.x + patrolDir * patrolSpeed * Time.deltaTime;
        
        // Cambiar dirección en los límites
        if (patrolDir > 0 && transform.position.x >= rightPoint.position.x)
        {
            patrolDir = -1;
            Flip(patrolDir);
        }
        else if (patrolDir < 0 && transform.position.x <= leftPoint.position.x)
        {
            patrolDir = 1;
            Flip(patrolDir);
        }

        // Mover en la trayectoria fija (solo eje X)
        Vector2 targetPos = new Vector2(targetX, hoverY);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, patrolSpeed * Time.deltaTime);
    }

    private void DetectPlayerBelow()
    {
        // Verificar que playerMask esté configurado
        if (playerMask == 0)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            return;
        }

        // Buscar si hay un jugador dentro del radio
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius, playerMask);

        if (hit != null)
        {
            player = hit.transform;
        }
        else
        {
            player = null;
        }
    }

    private bool IsPlayerBelowAndAligned()
    {
        if (player == null) return false;
        
        // Verificar que el jugador esté debajo (Y del jugador menor que Y del enemigo)
        bool isBelow = player.position.y < transform.position.y;
        
        // Verificar alineación horizontal
        float horizontalDistance = Mathf.Abs(player.position.x - transform.position.x);
        bool isAligned = horizontalDistance <= alignThreshold;
        
        // Verificar que esté dentro del radio de detección
        float distance = Vector2.Distance(transform.position, player.position);
        bool inRange = distance <= detectRadius;
        
        return isBelow && isAligned && inRange;
    }

    private void TryDropBomb()
    {
        if (!canDrop || bombPrefab == null || bombSpawn == null) return;
        
        // Instanciar la bomba
        GameObject bomb = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
        
        // Asegurar que la bomba tenga Rigidbody2D configurado correctamente
        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
        if (bombRb != null)
        {
            bombRb.gravityScale = 1f; // Asegurar que caiga
            bombRb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            Debug.LogWarning($"La bomba no tiene Rigidbody2D. Añadiéndolo automáticamente...");
            bombRb = bomb.AddComponent<Rigidbody2D>();
            bombRb.gravityScale = 1f;
        }
        
        Debug.Log($"{gameObject.name} lanzó una bomba en {bombSpawn.position}");
        
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
        // Radio de detección
        Gizmos.color = new Color(1, 0.5f, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        
        // Línea de patrulla
        if (leftPoint != null && rightPoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 leftPos = new Vector3(leftPoint.position.x, hoverY, 0);
            Vector3 rightPos = new Vector3(rightPoint.position.x, hoverY, 0);
            Gizmos.DrawLine(leftPos, rightPos);
        }
        
        // Zona de detección debajo del enemigo
        Gizmos.color = Color.red;
        Vector3 leftAlign = new Vector3(transform.position.x - alignThreshold, transform.position.y - detectRadius, 0);
        Vector3 rightAlign = new Vector3(transform.position.x + alignThreshold, transform.position.y - detectRadius, 0);
        Gizmos.DrawLine(leftAlign, rightAlign);
        
        // Punto de spawn de bomba
        if (bombSpawn != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(bombSpawn.position, 0.2f);
        }
    }
}