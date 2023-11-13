using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public SlotGame slotGame;

    public GameObject baseHud;
    public Transform hudPos;

    [Header("HUD active")]
    public GameObject hudActive;
    public GameObject iconCoin;
    public Image loadBar;
    public TextMeshProUGUI productionText;


    [Header("HUD purchase")]
    public GameObject hudPurchase;
    public TextMeshProUGUI priceText;
    public Image iconCoinPurchase;


    [Header("HUD upgrade")]
    public GameObject hudUpgrade;
    public Image hudUpgradeImage;
    public Image progressUpgradeSlot;
    public TextMeshProUGUI totalEvolText;
    public TextMeshProUGUI priceEvolText;
    public TextMeshProUGUI levelSlotText;

    private float tempTime, fillAmount;
    private double goldProduced;

    private void Start()
    {
        baseHud.transform.position = hudPos.position;
        slotGame.InitializeSlotGame();
    }

    private void Update()
    {
        if (goldProduced == 0)
        {
            Production();
        }
        else if (goldProduced > 0 && slotGame.isAutoProduction)
        {
            Production();
        }

    }

    private void Production()
    {
        tempTime += Time.deltaTime;
        fillAmount = tempTime / slotGame.timeProduction;
        loadBar.fillAmount = fillAmount;

        if (tempTime >= slotGame.timeProduction)
        {
            tempTime = 0;
            goldProduced += slotGame.production;
            productionText.text = GameManager.Instance.ConversorMonetario(goldProduced);
        }

        if (goldProduced > 0)
        {
            iconCoin.SetActive(true);
        }
        else
        {
            iconCoin.SetActive(false);
        }
    }

    public void CollectGold()
    {
        if (GameManager.Instance.currentState != GameState.Gameplay)
        {
            return;
        }

        GameManager.Instance.GetCoin(goldProduced);
        goldProduced = 0;
        productionText.text = GameManager.Instance.ConversorMonetario(goldProduced);
    }

    public void UpgradeMode()
    {
        if (slotGame.isPurchased)
        {
            switch (GameManager.Instance.isUpgradeMode)
            {
                case true:
                    hudActive.SetActive(false);
                    hudUpgrade.SetActive(true);
                    break;
                case false:
                    hudActive.SetActive(true);
                    hudUpgrade.SetActive(false);
                    break;
            }

        }
    }

    private void OnMouseEnter()
    {
        CollectGold();
    }

    private void OnMouseDown()
    {
        CollectGold();
    }
}
