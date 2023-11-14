using System.Collections;
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

    private bool isLoopUpgrade;
    private bool isUpgrade;
    private float delayLoop = 0.5f;
    private float delayBetweenUpgrade = 0.05f;

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
            productionText.text = GameManager.Instance.MonetaryConverter(goldProduced);
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
        productionText.text = GameManager.Instance.MonetaryConverter(goldProduced);
    }

    public void UpgradeMode()
    {
        UpdateHudUpgrade();

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

    public void UpgradeSlot()
    {
        GameManager.Instance.GetCoin(slotGame.costUpgrade * -1);
        slotGame.evolutions++;
        slotGame.totalEvolutions++;

        if (slotGame.slotLevel == 1 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();
        }
        else if (slotGame.slotLevel == 2 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeTimeProduction();
            slotGame.isAutoProduction = true;

        }
        else if (slotGame.slotLevel == 3 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();

        }
        else if (slotGame.slotLevel == 4 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeTimeProduction();

        }
        else if (slotGame.slotLevel == 5 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();

        }
        else if (slotGame.slotLevel == 6 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeTimeProduction();

        }
        else if (slotGame.slotLevel == 7 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();

        }
        else if (slotGame.slotLevel == 8 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeTimeProduction();

        }
        else if (slotGame.slotLevel == 9 && slotGame.evolutions == GameManager.Instance.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();
            slotGame.isMaxLevel = true;

        }

        slotGame.InitializeSlotGame();
        UpdateHudUpgrade();

    }

    private void UpgradeProduction()
    {
        slotGame.slotLevel++;
        slotGame.evolutions = 0;

        if (slotGame.slotLevel == 2)
        {
            slotGame.multiplier++;
        }
        else
        {
            slotGame.multiplier += 2;
        }
    }

    private void UpgradeTimeProduction()
    {
        slotGame.slotLevel++;
        slotGame.evolutions = 0;

        if (slotGame.slotLevel == 3)
        {
            slotGame.reducerTimeProduction++;
        }
        else
        {
            slotGame.reducerTimeProduction += 2;
        }
    }

    private void UpdateHudUpgrade()
    {
        totalEvolText.text = slotGame.totalEvolutions.ToString();
        priceEvolText.text = GameManager.Instance.MonetaryConverter(slotGame.costUpgrade);
        levelSlotText.text = slotGame.slotLevel.ToString();

        float fillAmount = (float)slotGame.evolutions / GameManager.Instance.progressionTable[slotGame.slotLevel - 1];
        progressUpgradeSlot.fillAmount = fillAmount;
    }

    private void OnMouseEnter()
    {
        CollectGold();
    }

    private void OnMouseDown()
    {
        CollectGold();
    }

    public void OnPointerDown()
    {
        isUpgrade = true;
        StartCoroutine(UpgradeLoop());
    }

    public void OnPointerUp()
    {
        isUpgrade = false;
        isLoopUpgrade = false;
        StopCoroutine(UpgradeLoop());
    }

    private IEnumerator UpgradeLoop()
    {
        UpgradeSlot();

        if (!isLoopUpgrade)
        {
            yield return new WaitForSeconds(delayLoop);
            isLoopUpgrade = true;
        }

        yield return new WaitForSeconds(delayBetweenUpgrade);

        if (isUpgrade)
        {
            StartCoroutine(UpgradeLoop());
        }
    }
}




