using UnityEditor;
using UnityEngine;

public enum TypeCard
{
    COMMON, RARE, EPIC, LEGENDARY
}

[CreateAssetMenu(fileName = "New Card", menuName = "Collection/Card")]
public class Card : ScriptableObject
{
    private GameManager gameManager;

    [Header("Fixed information")]
    public int idCarta;
    public string cardName;
    public Sprite spriteCard;
    public Sprite spriteCardDisable;
    public TypeCard rarity;
    public double production;
    public float timeProduction;

    [Header("Initial Data")]
    public bool isReleseadInitial;

    public bool isRelesead;
    public int multiplier = 1;
    public float reducerTimeProduction = 1;
    public int cardLevel = 1;
    public int cardsCollected = 0;

    public bool maxLevel;

    public void ResetCard()
    {
        isRelesead = isReleseadInitial;
        multiplier = 1;
        reducerTimeProduction = 1;
        cardLevel = 1;
        cardsCollected = 0;
        maxLevel = false;
    }

    public void SetCardCollect(int qtd)
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        if (!maxLevel)
        {
            cardsCollected += qtd;
            if (cardsCollected >= gameManager.progressionTableCard[cardLevel])
            {
                int dif = cardsCollected - gameManager.progressionTableCard[cardLevel];
                cardLevel++;
                cardsCollected = dif;
                switch (cardLevel)
                {
                    case 2:
                        multiplier = 5;
                        break;
                    case 3:
                        reducerTimeProduction = 5;
                        maxLevel = true;
                        break;
                }

                foreach (SlotController s in gameManager.slots)
                {
                    s.slotGame.InitializeSlotGame();
                }

                gameManager.cartaEvolucao.sprite = spriteCard;
                gameManager.mensagemEvolucao.text = "A construção <color=#FFFF00>" 
                    + cardName + "</color> evoluiu.";
                gameManager.painelFumeEvolucao.SetActive(true);
                gameManager.SaveCardDataGame();
            }
        }
        else
        {
            // carta maxima gerar ouro ou gema
        }


    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Card))]
public class ResetCard : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(10f);
        Card controller = (Card)target;
        if (GUILayout.Button("Reset My card"))
        {
            controller.ResetCard();
            GameManager.Instance.SaveCardDataGame();
        }
    }
}
#endif