using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    Gameplay,
    Upgrade
}

public class GameManager : PainfulSmile.Runtime.Core.Singleton<GameManager>
{
    public GameState currentState;

    public double gold;
    public double ouroAcumalado;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI gemText;

    public bool isUpgradeMode;

    public List<SlotController> slots;

    private void Start()
    {
        goldText.text = ConversorMonetario(gold);


        foreach (GameObject g in GameObject.FindGameObjectsWithTag("terreno"))
        {
            slots.Add(g.GetComponentInChildren<SlotController>());
        }
    }

    private void Update()
    {

    }

    public void GetCoin(double value)
    {
        gold += value;
        if (gold > 0)
        {
            ouroAcumalado += value;
        }

        goldText.text = ConversorMonetario(gold);
    }

    public string ConversorMonetario(double value)
    {
        string retorno = "";
        retorno = value.ToString();
        return retorno;
    }

    public void UpgradeMode()
    {
        isUpgradeMode = !isUpgradeMode;

        switch (isUpgradeMode)
        {
            case true:
                ChangeState(GameState.Upgrade);
                break;
            case false:
                ChangeState(GameState.Gameplay);
                break;
        }

        foreach (SlotController slotController in slots)
        {
            slotController.UpgradeMode();
        }
    }

    public void ChangeState(GameState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
