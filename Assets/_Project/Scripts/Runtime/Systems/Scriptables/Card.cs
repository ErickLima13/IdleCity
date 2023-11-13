using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Collection/Card")]
public class Card : ScriptableObject
{
    public enum TypeCard
    {
        COMMON, RARE, EPIC, LEGENDARY
    }

    [Header("Fixed information")]
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

    public void ResetCard()
    {
        isRelesead = isReleseadInitial;
        multiplier = 1;
        reducerTimeProduction = 1;
        cardLevel = 1;
        cardsCollected = 0;
    }

}
