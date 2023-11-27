using UnityEngine;

[CreateAssetMenu(fileName = "New Save", menuName = "Save Game/Save")]
public class SaveGame : ScriptableObject
{
    public double gold;
    public double goldEarned; // ouro acumulado

    public int multiplierBonus;
    public int multiplierTemp;

    public int reducerTimeBonus;
    public int reducerTimeTemp;


    public void ResetSave()
    {
        gold = 0;
        goldEarned = 0;
    }

}
