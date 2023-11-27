using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Gameplay,
    Upgrade,
    Cut
}

public class GameManager : PainfulSmile.Runtime.Core.Singleton<GameManager>
{
    public GameState currentState;
    public SaveGame saveGame;

    public int[] progressionTable;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI gemText;

    public bool isUpgradeMode;

    public List<SlotController> slots;
    private string[] acumulado;

    [Header("Hud Upgrade")]
    public Sprite[] bgHud;
    public Color[] textColor;

    [Header("Hud Purchase")]
    public Sprite[] bgSlot;
    public Sprite[] iconCoin;

    [Header("Hud Purchase Slot")]
    public GameObject painelFume;
    public GameObject painelCut;
    public Card card;
    public Image imageCard;
    public TextMeshProUGUI messageCard;
    public Button buttonClose;

    [Header("Prefabs")]
    public GameObject coinPrefab;
    public GameObject popUpProduction;


    private void Start()
    {
        buttonClose.onClick.AddListener(OpenCutPurchaseSlot);

        goldText.text = MonetaryConverter(saveGame.gold);

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("terreno"))
        {
            slots.Add(g.GetComponentInChildren<SlotController>());
        }
    }

    public void GetCoin(double value)
    {
        saveGame.gold += value;
        if (saveGame.gold > 0)
        {
            saveGame.goldEarned += value;
        }

        goldText.text = MonetaryConverter(saveGame.gold);
    }

    public string MonetaryConverter(double value)
    {
        string retorno = "";
        string valueTemp = "";
        double temp = 0;

        if (value >= 1e+30D)
        {
            temp = value / 1e+30D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "ff";
        }
        else if (value >= 1e+27D)
        {
            temp = value / 1e+27D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "ee";
        }
        else if (value >= 1e+24D)
        {
            temp = value / 1e+24D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "dd";
        }
        else if (value >= 1e+21D)
        {
            temp = value / 1e+21D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "cc";
        }
        else if (value >= 1e+18D)
        {
            temp = value / 1e+18D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "bb";
        }
        else if (value >= 1e+15D)
        {
            temp = value / 1e+15D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "aa";
        }
        else if (value >= 1e+12D)
        {
            temp = value / 1e+12D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "T";
        }
        else if (value >= 1e+9D)
        {
            temp = value / 1e+9D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "B";
        }
        else if (value >= 1e+6D)
        {
            temp = value / 1e+6D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "M";
        }

        else if (value >= 1e+3D)
        {
            temp = value / 1e+3D;
            valueTemp = temp.ToString("N1");
            retorno = RemoverZero(valueTemp) + "K";
        }
        else
        {
            retorno = value.ToString("N0");
        }

        return retorno;
    }

    private string RemoverZero(string s)
    {
        string retorno;
        acumulado = s.Split(',');

        if (acumulado[1] != "0")
        {
            retorno = acumulado[0] + "." + acumulado[1];
        }
        else
        {
            retorno = acumulado[0];
        }

        return retorno;
    }

    public void UpgradeMode()
    {
        isUpgradeMode = !isUpgradeMode;
        painelFume.SetActive(isUpgradeMode);

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

    public void UpdateHudSlots()
    {
        foreach (SlotController slotController in slots)
        {
            slotController.CheckSlotUpgrade();
        }
    }

    public void ChangeState(GameState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void OpenCutPurchaseSlot()
    {
        painelCut.SetActive(!painelCut.activeSelf);
        painelFume.SetActive(painelCut.activeSelf);

        switch (painelCut.activeSelf)
        {
            case true:
                ChangeState(GameState.Cut);
                break;
            case false:
                ChangeState(GameState.Gameplay);
                break;
        }


    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class ResetSaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(10f);
        GameManager controller = (GameManager)target;
        if (GUILayout.Button("Reset Save"))
        {
            controller.saveGame.ResetSave();
        }
    }
}
#endif
