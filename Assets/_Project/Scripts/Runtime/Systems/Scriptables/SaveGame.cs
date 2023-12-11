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

    public double custoMaletaComum;
    public int maletasCompradas;

    public int gemas;
    public int gemasAcumuladas;

    public int idQuest;
    public bool isQuest = true;

    public void GetMaleta()
    {
        maletasCompradas++;
        custoMaletaComum = custoMaletaComum * (maletasCompradas + 1);
    }


    public void ResetSave()
    {
        gold = 0;
        goldEarned = 0;
        custoMaletaComum = 1000;
        maletasCompradas = 0;
        gemas = 0;
        gemasAcumuladas = 0;    
        idQuest = 0;
    }

}
