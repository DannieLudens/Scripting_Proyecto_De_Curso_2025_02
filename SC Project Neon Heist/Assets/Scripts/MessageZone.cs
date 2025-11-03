using UnityEngine;
using UnityEngine.UI;

public class MessageZone : MonoBehaviour
{
    [Tooltip("Mensaje que aparecerá cuando el jugador no tenga la tarjeta")]
    public GameObject messageUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica si el jugador tiene la tarjeta usando el CollectibleManager
            int cardCount = CollectibleManager.Instance.GetCollectibleCount("Card");

            if (cardCount == 0)
            {
                // Si no tiene tarjeta, muestra el mensaje
                if (messageUI != null)
                    messageUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Oculta el mensaje cuando el jugador se aleja
            if (messageUI != null)
                messageUI.SetActive(false);
        }
    }
}
