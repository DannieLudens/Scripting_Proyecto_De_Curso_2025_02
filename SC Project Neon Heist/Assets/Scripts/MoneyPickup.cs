using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public int value = 1; // valor del billete
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            collected = true; // evita que se ejecute dos veces
            CollectibleManager.Instance.AddCollectible("Money", value);
            Destroy(gameObject);
        }
    }
}
