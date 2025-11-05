using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject panelGameOver; // Panel completo de Game Over
    [SerializeField] private Button botonReintentar;
    [SerializeField] private Button botonMenuPrincipal;
    [SerializeField] private Button botonSalir;

    [Header("Configuración")]
    [SerializeField] private float tiempoAntesDeGameOver = 1f; // Delay antes de mostrar el panel

    private static GameOverManager instance;

    private void Awake()
    {
        // Singleton para acceder desde otros scripts
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Ocultar el panel al inicio
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }

        // Configurar botones
        if (botonReintentar != null)
        {
            botonReintentar.onClick.AddListener(Reintentar);
        }

        if (botonMenuPrincipal != null)
        {
            botonMenuPrincipal.onClick.AddListener(VolverAlMenu);
        }

        if (botonSalir != null)
        {
            botonSalir.onClick.AddListener(SalirDelJuego);
        }
    }

    // Método público para activar el Game Over desde otros scripts
    public static void MostrarGameOver()
    {
        if (instance != null)
        {
            instance.StartCoroutine(instance.MostrarPanelGameOver());
        }
    }

    private IEnumerator MostrarPanelGameOver()
    {
        // Esperar un momento antes de mostrar el panel
        yield return new WaitForSeconds(tiempoAntesDeGameOver);

        // Pausar el juego
        Time.timeScale = 0f;

        // Mostrar el panel
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }

        Debug.Log("Game Over - Panel mostrado");
    }

    public void Reintentar()
    {
        Debug.Log("Reiniciando nivel...");
        
        // Restaurar el tiempo
        Time.timeScale = 1f;

        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú principal...");
        
        // Restaurar el tiempo
        Time.timeScale = 1f;

        // Cargar el menú principal
        SceneManager.LoadScene("MainMenu");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        
        // Restaurar el tiempo por si acaso
        Time.timeScale = 1f;

        // Salir de la aplicación
        Application.Quit();

        // En el editor, detener el modo Play
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void OnDestroy()
    {
        // Limpiar listeners de botones
        if (botonReintentar != null)
        {
            botonReintentar.onClick.RemoveListener(Reintentar);
        }

        if (botonMenuPrincipal != null)
        {
            botonMenuPrincipal.onClick.RemoveListener(VolverAlMenu);
        }

        if (botonSalir != null)
        {
            botonSalir.onClick.RemoveListener(SalirDelJuego);
        }
    }
}