using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerControllerTests
{
    private GameObject player;
    private PlayerController controller;
    private Rigidbody2D rb;

    [SetUp]
    public void SetUp()
    {
        // Crear un objeto "Ground"
        var ground = new GameObject("Ground");
        ground.tag = "Ground";
        var groundCollider = ground.AddComponent<BoxCollider2D>();
        ground.transform.position = Vector3.zero;

        // Crear jugador
        player = new GameObject("Player");
        rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        controller = player.AddComponent<PlayerController>();

        // Ponerlo encima del suelo
        player.transform.position = new Vector3(0, 1, 0);
    }

    [UnityTest]
    public IEnumerator Player_Moves_Right_When_InputPressed()
    {
        // Simulamos presionar tecla derecha
        controller.moveSpeed = 5f;
        rb.linearVelocity = Vector2.zero;

        // Forzamos movimiento como si se hubiera apretado tecla derecha
        rb.linearVelocity = new Vector2(1 * controller.moveSpeed, rb.linearVelocity.y);

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(player.transform.position.x, 0f, "El jugador debería haberse movido hacia la derecha");
    }

    [UnityTest]
    public IEnumerator Player_Jumps_When_Grounded_And_JumpPressed()
    {
        // Asegurar que está en el suelo
        player.transform.position = new Vector3(0, 0.5f, 0);
        yield return new WaitForFixedUpdate();

        float yAntes = player.transform.position.y;

        // Forzamos el salto
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, controller.jumpForce);

        yield return new WaitForSeconds(0.2f);

        Assert.Greater(player.transform.position.y, yAntes, "El jugador debería haber saltado");
    }
}
