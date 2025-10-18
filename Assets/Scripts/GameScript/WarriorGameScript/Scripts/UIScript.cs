using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CoinText;
    [SerializeField] private Image HpBarImage;

    [SerializeField] private HealthScript PlayerHp;
    void Start()
    {
        CoinManager.OnAddCoin.AddListener(UiCoin);
    }
    void FixedUpdate()
    {
        HpBarImage.fillAmount =  PlayerHp.GetHealth() / 100;
    }
    private void UiCoin(int coin)
    {
        CoinText.text = CoinManager.coinsCount.ToString();
    }
    void OnDestroy()
    {
        CoinManager.OnAddCoin.RemoveListener(UiCoin);
    }
}
