using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager;
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
    public int[] progressionTableCard;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI gemText;

    public bool isUpgradeMode, isCollection;

    public List<SlotController> slots;
    private string[] acumulado;

    [Header("Hud Upgrade")]
    public Sprite[] bgHud;
    public Color[] textColor;

    [Header("Hud Purchase")]
    public Sprite[] bgSlot;
    public Sprite[] iconCoin;

    [Header("Cut")]
    public GameObject painelFume;
    public GameObject painelCut;
    public Card card;
    public Image imageCard;
    public TextMeshProUGUI messageCard;
    public Button buttonClose;
    public GameObject painelCollection;

    [Header("card info")]
    public GameObject painelCardInfo;
    public Image typeCardInfo;
    public TextMeshProUGUI producionText;
    public TextMeshProUGUI producionMinuteText;
    public TextMeshProUGUI levelSlotText;
    public CardCollection cardInfoCollection;
    public Slider sliderSlot;
    public Card tempCard;

    [Header("Prefabs")]
    public GameObject coinPrefab;
    public GameObject popUpProduction;

    [Header("Type cards")]
    public Sprite[] typeCards;

    [Header("Open Case")]
    public GameObject casePanel;
    public OpenCase openCase;

    [Header("colecao carta")]
    public Card[] comum;
    public Card[] raro;
    public Card[] epico;
    public Card[] lendario;
    public List<Card> cartas;
    public List<SlotGame> terrenos;

    [Header("evolucao carta")]
    public GameObject painelFumeEvolucao;
    public Image cartaEvolucao;
    public TextMeshProUGUI mensagemEvolucao;

    [Header("Maleta Comum")]
    public TextMeshProUGUI custoMaletaTxt;

    [Header("custo gemas")]
    public int[] custoMaletasGemas;

    [Header("Quest")]
    public GameObject panelQuest;
    public TextMeshProUGUI missaoTxt;
    [TextArea] public string[] questDescricao;
    public GameObject btnColecao, btnUpgrade;

    [Header("Selecao carta")]
    public SlotController slotC;
    public GameObject panelEscolheCarta;


    private void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("terreno"))
        {
            slots.Add(g.GetComponentInChildren<SlotController>());
        }

        cartas.AddRange(comum);
        cartas.AddRange(raro);
        cartas.AddRange(epico);
        cartas.AddRange(lendario);

        // print(Application.persistentDataPath + "/saveData.dat");

        if (!File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            SaveDataGame();
            SaveLoteDataGame();
            SaveCardDataGame();
            LoadAll();
        }
        else
        {
            LoadAll();
        }

        buttonClose.onClick.AddListener(OpenCutPurchaseSlot);
        custoMaletaTxt.text = MonetaryConverter(saveGame.custoMaletaComum);
        goldText.text = MonetaryConverter(saveGame.gold);
        gemText.text = saveGame.gemas.ToString();

        UpdateQuest();
 
    }

    private void Update()
    {
        if (saveGame.isQuest)
        {
            switch (saveGame.idQuest)
            {
                case 0:
                    if (saveGame.gold >= 20)
                    {
                        saveGame.idQuest++;
                        UpdateQuest();
                    }
                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
            }
        }
    }

    public void LoadAll()
    {
        LoadDataGame();
        LoadLoteDataGame();
        LoadCardDataGame();
    }

    public void UpdateQuest()
    {
        if (saveGame.isQuest)
        {
            switch (saveGame.idQuest)
            {
                case 0:
                    btnColecao.SetActive(false);
                    btnUpgrade.SetActive(false);
                    break;
                case 1:
                case 2:
                    btnColecao.SetActive(false);
                    btnUpgrade.SetActive(true);
                    break;
                case 3:
                    btnColecao.SetActive(true);
                    btnUpgrade.SetActive(true);
                    break;
            }

            if (saveGame.idQuest < questDescricao.Length)
            {
                panelQuest.SetActive(true);
                missaoTxt.text = questDescricao[saveGame.idQuest];
            }
            else
            {
                panelQuest.SetActive(false);

            }

        }
        else
        {
            panelQuest.SetActive(false);
        }

        SaveDataGame();
    }

    public void GetCoin(double value)
    {
        saveGame.gold += value;
        if (saveGame.gold > 0)
        {
            saveGame.goldEarned += value;
        }

        goldText.text = MonetaryConverter(saveGame.gold);
        SaveDataGame();
    }

    public void GetGemas(int value)
    {
        saveGame.gemas += value;
        if (saveGame.gemas > 0)
        {
            saveGame.gemasAcumuladas += value;
        }

        gemText.text = saveGame.gemas.ToString();
        SaveDataGame();
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
        if (currentState == GameState.Cut)
        {
            return;
        }

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

    public void FecharSelecaoCarta()
    {
        panelEscolheCarta.SetActive(false);
        painelFume.SetActive(panelEscolheCarta.activeSelf);
        ChangeState(GameState.Gameplay);
    }

    public void OpenEscolherCarta()
    {
        //foreach (SlotController slotController in slots)
        //{
        //    slotController.ControlHud(false);
        //}

        panelEscolheCarta.SetActive(true);
        painelFume.SetActive(panelEscolheCarta.activeSelf);
        ChangeState(GameState.Cut);


    }

    public void OpenCollection()
    {
        painelCollection.SetActive(!painelCollection.activeSelf);
        painelFume.SetActive(painelCollection.activeSelf);

        switch (painelCollection.activeSelf)
        {
            case true:
                ChangeState(GameState.Cut);

                foreach (CardCollection c in FindObjectsOfType<CardCollection>())
                {
                    c.SetupCard();
                }

                break;
            case false:
                ChangeState(GameState.Gameplay);
                break;
        }
    }

    public void AtualizarCartaRecebida()
    {
        foreach (CardCollection c in FindObjectsOfType<CardCollection>())
        {
            c.SetupCard();
        }
    }

    public void OpenCardInfo(Card cardInfo)
    {
        tempCard = cardInfo;
        cardInfoCollection.card = cardInfo;
        cardInfoCollection.SetupCard();
        painelCardInfo.SetActive(true);
        double prod = (cardInfo.production * cardInfo.multiplier
            / cardInfo.timeProduction / cardInfo.reducerTimeProduction);
        producionText.text = MonetaryConverter(prod * 60 * 60);
        producionMinuteText.text = MonetaryConverter(prod * 60);
        sliderSlot.value = sliderSlot.minValue;
    }

    public void ChangeLevelSlot()
    {
        int baseSlot = 1;
        double prod = 0;
        int multEvolucoes = 1;
        int multTerreno = 1;
        float redutorTempo = 1;
        int nTerreno = (int)sliderSlot.value;
        levelSlotText.text = nTerreno.ToString();

        switch (nTerreno)
        {
            case 1:
                multEvolucoes = 1;
                multTerreno = 1;
                redutorTempo = 1;
                break;
            case 2:
                baseSlot = 1;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 2;
                redutorTempo = 1;
                break;
            case 3:
                baseSlot = 2;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 2;
                redutorTempo = 2;
                break;
            case 4:
                baseSlot = 3;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 4;
                redutorTempo = 2;
                break;
            case 5:
                baseSlot = 4;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 4;
                redutorTempo = 4;
                break;
            case 6:
                baseSlot = 5;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 6;
                redutorTempo = 4;
                break;
            case 7:
                baseSlot = 6;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 6;
                redutorTempo = 6;
                break;
            case 8:
                baseSlot = 7;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 8;
                redutorTempo = 6;
                break;
            case 9:
                baseSlot = 8;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 8;
                redutorTempo = 8;
                break;
            case 10:
                baseSlot = 9;
                multEvolucoes = progressionTable[baseSlot - 1];
                multTerreno = 10;
                redutorTempo = 8;
                break;

        }

        prod = ((tempCard.production * tempCard.multiplier *
            multEvolucoes * multTerreno)
            / (tempCard.timeProduction / tempCard.reducerTimeProduction / redutorTempo));
        producionText.text = MonetaryConverter(prod * 60 * 60);
        producionMinuteText.text = MonetaryConverter(prod * 60);


    }

    public void GetBooster(int boosterType)
    {
        switch (boosterType)
        {
            case 0:

                if (saveGame.gold >= saveGame.custoMaletaComum)
                {
                    openCase.SetType(3, OpenCase.CaseType.Comum);
                    GetCoin(saveGame.custoMaletaComum * -1);
                    saveGame.GetMaleta();
                    custoMaletaTxt.text = MonetaryConverter(saveGame.custoMaletaComum);
                    casePanel.SetActive(true);

                    if (saveGame.idQuest == 3)
                    {
                        saveGame.idQuest++;
                        UpdateQuest();
                    }
                }

                break;
            case 1:

                if (saveGame.gemas >= custoMaletasGemas[boosterType])
                {
                    openCase.SetType(4, OpenCase.CaseType.rara);
                    casePanel.SetActive(true);
                    GetGemas(custoMaletasGemas[boosterType] * -1);
                }
                break;
            case 2:

                if (saveGame.gemas >= custoMaletasGemas[boosterType])
                {
                    openCase.SetType(6, OpenCase.CaseType.Epica);
                    casePanel.SetActive(true);
                    GetGemas(custoMaletasGemas[boosterType] * -1);
                }
                break;
            case 3:

                if (saveGame.gemas >= custoMaletasGemas[boosterType])
                {
                    openCase.SetType(8, OpenCase.CaseType.Lendaria);
                    casePanel.SetActive(true);
                    GetGemas(custoMaletasGemas[boosterType] * -1);
                }
                break;
        }


    }

    public void SaveLoteDataGame()
    {
        int i = 0;
        foreach (SlotGame s in terrenos)
        {
            bool valorInicial = false;
            if (File.Exists(Application.persistentDataPath + "/saveLoteData" + i + ".dat"))
            {
                valorInicial = true;
            }

            BinaryFormatter bf = new();
            FileStream file = File.Create(Application.persistentDataPath + "/saveLoteData" + i + ".dat");
            LoteGameData data = new()
            {
                isPurchased = s.isPurchased,
                idCarta = s.card.idCarta,
                isMaxLevel = s.isMaxLevel,
                evolutions = s.evolutions,
                totalEvolutions = s.totalEvolutions,
                timeProduction = s.timeProduction,
                isAutoProduction = s.isAutoProduction,
                costUpgrade = s.costUpgrade,
                production = s.production

            };
            if (!valorInicial)
            {
                data.slotLevel = 1;
                data.multiplier = 1;
                data.reducerTimeProduction = 1;
            }
            else
            {
                data.slotLevel = s.slotLevel;
                data.multiplier = s.multiplier;
                data.reducerTimeProduction = s.reducerTimeProduction;
            }
            bf.Serialize(file, data);
            file.Close();
            i++;
        }
    }

    public void LoadLoteDataGame()
    {
        int i = 0;
        foreach (SlotGame s in terrenos)
        {
            BinaryFormatter bf = new();
            FileStream file = File.Open(Application.persistentDataPath + "/saveLoteData" + i + ".dat", FileMode.Open);
            LoteGameData data = (LoteGameData)bf.Deserialize(file);

            s.isPurchased = data.isPurchased;
            s.card = cartas[data.idCarta];
            s.isMaxLevel = data.isMaxLevel;
            s.slotLevel = data.slotLevel;
            s.evolutions = data.evolutions;
            s.totalEvolutions = data.totalEvolutions;
            s.multiplier = data.multiplier;
            s.timeProduction = data.timeProduction;
            s.reducerTimeProduction = data.reducerTimeProduction;
            s.isAutoProduction = data.isAutoProduction;
            s.costUpgrade = data.costUpgrade;
            s.production = data.production;

        
            file.Close();
            i++;
        }
        foreach (SlotController slot in slots)
        {
            slot.Initialize();
        }
    }

    public void SaveDataGame()
    {
        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData.dat");
        SaveGameData data = new()
        {
            gold = saveGame.gold,
            goldEarned = saveGame.goldEarned,
            gemas = saveGame.gemas,
            gemasAcumuladas = saveGame.gemasAcumuladas,
            maletasCompradas = saveGame.maletasCompradas,
            custoMaletaComum = saveGame.custoMaletaComum,
            multiplierBonus = saveGame.multiplierBonus,
            multiplierTemp = saveGame.multiplierTemp,
            reducerTimeBonus = saveGame.reducerTimeBonus,
            reducerTimeTemp = saveGame.reducerTimeTemp,
            idQuest = saveGame.idQuest,
            isQuest = saveGame.isQuest
        };

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadDataGame()
    {
        BinaryFormatter bf = new();
        FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
        SaveGameData data = (SaveGameData)bf.Deserialize(file);

        saveGame.gold = data.gold;
        saveGame.goldEarned = data.goldEarned;
        saveGame.gemas = data.gemas;
        saveGame.gemasAcumuladas = data.gemasAcumuladas;
        saveGame.maletasCompradas = data.maletasCompradas;
        saveGame.custoMaletaComum = data.custoMaletaComum;
        saveGame.multiplierBonus = data.multiplierBonus;
        saveGame.multiplierTemp = data.multiplierTemp;
        saveGame.reducerTimeBonus = data.reducerTimeBonus;
        saveGame.reducerTimeTemp = data.reducerTimeTemp;
        saveGame.idQuest = data.idQuest;
        saveGame.isQuest = data.isQuest;

        file.Close();
    }

    public void SaveCardDataGame()
    {
        int i = 0;
        foreach (Card s in cartas)
        {
            BinaryFormatter bf = new();
            FileStream file = File.Create(Application.persistentDataPath + "/saveCardData" + i + ".dat");
            CartaGameData data = new()
            {
                isRelesead = cartas[i].isRelesead,
                multiplier = cartas[i].multiplier,
                reducerTimeProduction = cartas[i].reducerTimeProduction,
                cardLevel = cartas[i].cardLevel,
                cardsCollected = cartas[i].cardsCollected,
                maxLevel = cartas[i].maxLevel,
            };

            bf.Serialize(file, data);
            file.Close();
            i++;
        }
    }

    public void LoadCardDataGame()
    {
        int i = 0;
        foreach (Card s in cartas)
        {
            BinaryFormatter bf = new();
            FileStream file = File.Open(Application.persistentDataPath + "/saveCardData" + i + ".dat", FileMode.Open);
            CartaGameData data = (CartaGameData)bf.Deserialize(file);

            s.isRelesead = data.isRelesead;
            s.multiplier = data.multiplier;
            s.reducerTimeProduction = data.reducerTimeProduction;
            s.cardLevel = data.cardLevel;
            s.cardsCollected = data.cardsCollected;
            s.maxLevel = data.maxLevel;

            file.Close();
            i++;
        }
    }



}

[Serializable]
public class SaveGameData
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
    public bool isQuest;
}

[Serializable]
public class LoteGameData
{
    public int idCarta;
    public int slotLevel;
    public int evolutions;
    public int totalEvolutions;
    public int multiplier;

    public float timeProduction;
    public float reducerTimeProduction;

    public double production;
    public double costUpgrade;

    public bool isPurchased;
    public bool isMaxLevel;
    public bool isAutoProduction;


}

[Serializable]
public class CartaGameData
{
    public bool isRelesead;
    public int multiplier;
    public float reducerTimeProduction;
    public int cardLevel;
    public int cardsCollected;
    public bool maxLevel;
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
            controller.SaveDataGame();
        }
    }
}
#endif
