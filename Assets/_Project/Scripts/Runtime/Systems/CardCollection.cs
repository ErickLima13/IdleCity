using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardCollection : MonoBehaviour
{
    private GameManager gameManager;
    private Button button;

    public Card card;
    public TextMeshProUGUI nameCard;
    public TextMeshProUGUI levelCard;
    public TextMeshProUGUI progresionLevel;

    public Image imageCard;
    public Image typeCard;
    public Image barProgress;

    private void Start()
    {
        gameManager = GameManager.Instance;
        
        if (TryGetComponent(out button))
        {
            button.onClick.AddListener(ChooseCard);
        }

        SetupCard();
    }

    public void SetupCard()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        int idbg = 0;

        switch (card.rarity)
        {
            case TypeCard.COMMON:
                idbg = 0;
                break;
            case TypeCard.RARE:
                idbg = 1;
                break;
            case TypeCard.EPIC:
                idbg = 2;
                break;
            case TypeCard.LEGENDARY:
                idbg = 3;
                break;
        }

        if (card.isRelesead)
        {
            imageCard.sprite = card.spriteCard;
            nameCard.text = card.cardName;
            levelCard.text = card.cardLevel.ToString();
            progresionLevel.text = card.cardsCollected + "/" + gameManager.progressionTableCard[card.cardLevel];
            typeCard.sprite = gameManager.typeCards[idbg];

            float fiilAmount = (float)card.cardsCollected / gameManager.progressionTableCard[card.cardLevel];
            barProgress.fillAmount = fiilAmount;
        }
        else
        {
            nameCard.text = "???";
            imageCard.sprite = card.spriteCardDisable;
            levelCard.text = "0";
            progresionLevel.text = "";
            typeCard.sprite = gameManager.typeCards[4];
        }
    }

    public void ChooseCard()
    {
        gameManager.slotC.slotGame.card = card;
        gameManager.slotC.slotGame.InitializeSlotGame();
        gameManager.slotC.BuySlot();
        gameManager.panelEscolheCarta.SetActive(false);

    }

}
