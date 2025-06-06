using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<CoinManager>().CollectCoin();
            Destroy(gameObject);
        }
    }
}
