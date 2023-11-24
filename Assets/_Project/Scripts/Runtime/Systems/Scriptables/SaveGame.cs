using UnityEngine;

[CreateAssetMenu(fileName = "New Save", menuName = "Save Game/Save")]
public class SaveGame : ScriptableObject
{
    public double gold;
    public double goldEarned; // ouro acumulado


    public void ResetSave()
    {
        gold = 0;
        goldEarned = 0;
    }

}
