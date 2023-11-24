using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private GameManager gameManager;

    public SlotGame slotGame;

    public SpriteRenderer construction;

    public GameObject baseHud;
    public Transform hudPos;

    [Header("HUD active")]
    public GameObject hudActive;
    public GameObject iconCoin;
    public Image loadBar;
    public TextMeshProUGUI productionText;

    [Header("HUD purchase")]
    public GameObject hudPurchase;
    public SpriteRenderer bgSlot;
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
        gameManager = GameManager.Instance;
        baseHud.transform.position = hudPos.position;
        slotGame.InitializeSlotGame();
        productionText.text = gameManager.MonetaryConverter(goldProduced);

        if (slotGame.isPurchased)
        {
            construction.sprite = slotGame.card.spriteCard;
            hudActive.SetActive(true);
            hudPurchase.SetActive(false);
            hudUpgrade.SetActive(false);
        }
        else
        {
            hudPurchase.SetActive(true);
            hudActive.SetActive(false);
            hudUpgrade.SetActive(false);
            bgSlot.sprite = gameManager.bgSlot[0];
            priceText.text = gameManager.MonetaryConverter(slotGame.costSlot);
        }
    }

    private void Update()
    {
        if (slotGame.isPurchased)
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
        else
        {
            if (CheckGoldToBuySlot())
            {
                bgSlot.sprite = gameManager.bgSlot[1];
                iconCoinPurchase.sprite = gameManager.iconCoin[1];
            }
            else
            {
                bgSlot.sprite = gameManager.bgSlot[0];
                iconCoinPurchase.sprite = gameManager.iconCoin[0];
            }
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
            productionText.text = gameManager.MonetaryConverter(goldProduced);
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
        if (gameManager.currentState != GameState.Gameplay)
        {
            return;
        }

        gameManager.GetCoin(goldProduced);
        goldProduced = 0;
        productionText.text = gameManager.MonetaryConverter(goldProduced);
    }

    public void UpgradeMode()
    {
        UpdateHudUpgrade();

        if (slotGame.isPurchased)
        {
            switch (gameManager.isUpgradeMode)
            {
                case true:
                    CheckSlotUpgrade();
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

    public void CheckSlotUpgrade()
    {
        if (slotGame.isMaxLevel)
        {
            hudUpgradeImage.sprite = gameManager.bgHud[2];
        }
        else
        {
            if (gameManager.saveGame.gold >= slotGame.costUpgrade)
            {
                hudUpgradeImage.sprite = gameManager.bgHud[1];
                priceEvolText.color = gameManager.textColor[1];
            }
            else
            {
                hudUpgradeImage.sprite = gameManager.bgHud[0];
                priceEvolText.color = gameManager.textColor[0];
            }
        }
    }

    public void UpgradeSlot()
    {
        gameManager.GetCoin(slotGame.costUpgrade * -1);
        gameManager.UpdateHudSlots();
        slotGame.evolutions++;
        slotGame.totalEvolutions++;

        if (slotGame.slotLevel == 1 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();
        }
        else if (slotGame.slotLevel == 2 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeTimeProduction();
            slotGame.isAutoProduction = true;

        }
        else if (slotGame.slotLevel == 3 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();

        }
        else if (slotGame.slotLevel == 4 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeTimeProduction();

        }
        else if (slotGame.slotLevel == 5 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();

        }
        else if (slotGame.slotLevel == 6 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeTimeProduction();

        }
        else if (slotGame.slotLevel == 7 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeProduction();

        }
        else if (slotGame.slotLevel == 8 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
        {
            UpgradeTimeProduction();

        }
        else if (slotGame.slotLevel == 9 && slotGame.evolutions == gameManager.progressionTable[slotGame.slotLevel - 1])
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
        priceEvolText.text = gameManager.MonetaryConverter(slotGame.costUpgrade);
        levelSlotText.text = slotGame.slotLevel.ToString();

        float fillAmount = (float)slotGame.evolutions / gameManager.progressionTable[slotGame.slotLevel - 1];
        progressUpgradeSlot.fillAmount = fillAmount;
    }

    private void OnMouseEnter()
    {
        if (gameManager.currentState == GameState.Gameplay && slotGame.isPurchased)
        {
            CollectGold();
        }

    }

    private void OnMouseDown()
    {
        if (gameManager.currentState == GameState.Gameplay)
        {
            if (slotGame.isPurchased && goldProduced > 0)
            {
                CollectGold();
            }
            else if (!slotGame.isPurchased && CheckGoldToBuySlot())
            {
                BuySlot();
            }
        }

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
        if (gameManager.saveGame.gold < slotGame.costUpgrade)
        {
            print("AQUI ESTOU");
            yield break;
        }

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

    private void BuySlot()
    {
        gameManager.GetCoin(slotGame.costSlot * -1);

        gameManager.imageCard.sprite = slotGame.card.spriteCard;
        gameManager.messageCard.text = "A construção <color=#FFFF00>"
            + slotGame.card.cardName + "</color> foi construida";

        gameManager.OpenCutPurchaseSlot();
        slotGame.isPurchased = true;
        construction.sprite = slotGame.card.spriteCard;
        hudActive.SetActive(true);
        hudPurchase.SetActive(false);
        hudUpgrade.SetActive(false);
    }

    private bool CheckGoldToBuySlot()
    {
        return gameManager.saveGame.gold >= slotGame.costSlot;
    }
}




