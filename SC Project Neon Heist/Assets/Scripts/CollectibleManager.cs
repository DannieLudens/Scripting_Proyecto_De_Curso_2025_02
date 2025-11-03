using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; private set; }

    // Diccionario: clave = nombre del ítem, valor = cantidad
    private Dictionary<string, int> collectibles = new Dictionary<string, int>()
    {
        { "Money", 0 },
        { "Card", 0 },
        { "Life", 0 }
    };

    public Text moneyText;
    public Text cardText;

    private void Awake()
    {
        // Singleton: aseguramos que solo exista un MoneyManager
        if (Instance != null && Instance != this) // Comprueba si ya existe otra instancia de MoneyManager distinta a esta
        {
            Destroy(gameObject); // Si ya existe otra instancia, destruye este GameObject (evita duplicados)
            return; // Sale del método para no ejecutar el resto
        }

        Instance = this; // Si no existe, asigna esta instancia como la única
        DontDestroyOnLoad(gameObject); // no se destruye al cambiar de escena

}

    public void AddCollectible(string itemName, int amount = 1)
    {
        if (amount > 0)
        {
            if (collectibles.ContainsKey(itemName))
            {
                collectibles[itemName] += amount;
            }
            else
            {
                collectibles[itemName] = amount;
            }

            Debug.Log("AddCollectible llamado por: " + itemName + " +" + amount +
              " | Total: " + collectibles[itemName] +
              " | Stacktrace: " + System.Environment.StackTrace);
        }
        
        Debug.Log(itemName + ": " + collectibles[itemName]);

        UpdateUI(itemName);
    }

    public void WithdrawCollectible(string itemName, int amount = 1)
    {
        if (amount > 0)
        {
            if (collectibles.ContainsKey(itemName))
            {
                collectibles[itemName] -= amount;
            }
            else
            {
                collectibles[itemName] = amount;
            }
        }

        Debug.Log(itemName + ": " + collectibles[itemName]);

        UpdateUI(itemName);
    }

    public int GetCollectibleCount(string itemName) // Obtener la cantidad de un ítem
    {
        if (collectibles.ContainsKey(itemName))
        {
            return collectibles[itemName];
        }
        return 0;
    }

    private void UpdateUI(string itemName)
    {
        if (itemName == "Money" && moneyText != null)
        {
            moneyText.text = "Money: " + GetCollectibleCount("Money");
        }
        else if (itemName == "Card" && cardText != null)
        {
            cardText.text = "Cards: " + GetCollectibleCount("Card");
        }
    }
}
