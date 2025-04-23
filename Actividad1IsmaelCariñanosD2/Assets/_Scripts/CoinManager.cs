using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int totalCoins;
    private int collectedCoins;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject monstro;

    void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        collectedCoins = 0;
        coinText.text = "Monedas: 0/" + totalCoins;
    }

    public void CollectCoin()
    {
        collectedCoins++;
        coinText.text = "Monedas: " + collectedCoins + "/" + totalCoins;

        if (collectedCoins >= totalCoins)
        {
            Destroy(monstro);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            win.SetActive(true);
        }
    }
}
