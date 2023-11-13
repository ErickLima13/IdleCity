using UnityEngine;

[CreateAssetMenu(fileName = "New Slot", menuName = "Slot Game/Slot")]
public class SlotGame : ScriptableObject
{
    private bool isRelesead;

    public Card card;

    public int slotLevel;
    public int evolutions;
    public int totalEvolutions;
    public int multiplier = 1;

    public float timeProduction;
    public float reducerTimeProduction = 1;

    public double costSlot;
    public double production;
    public double costUpgrade;

    public bool isPurchased;
    public bool isMaxLevel;
    public bool isAutoProduction;

    public void InitializeSlotGame()
    {
        int multi = 1;
        if(totalEvolutions > 0)
        {
            multi = totalEvolutions;
        }

        production = card.production * card.multiplier * multi * multiplier;
        costUpgrade = production * multiplier * 1.5f;
        timeProduction = card.timeProduction / card.reducerTimeProduction / reducerTimeProduction;
    }
    

}
