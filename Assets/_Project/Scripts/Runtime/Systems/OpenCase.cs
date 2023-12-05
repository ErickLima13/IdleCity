using TMPro;
using UnityEngine;

public class OpenCase : MonoBehaviour
{
    public enum CaseType
    {
        Comum, rara, Epica, Lendaria
    }


    private GameManager gameManager;
    public TextMeshProUGUI textValue;

    public GameObject btnMaleta;
    public GameObject btnMaletaFechar;

    public CaseType caseType;
    public int quantityReward;

    private bool getRare, getEpic, getLegendary;
    private int idRecompensa;

    public GameObject[] cartaRecompensa;


    private void Start()
    {
        btnMaleta.SetActive(true);
        btnMaletaFechar.SetActive(false);
        gameManager = GameManager.Instance;
        textValue.text = quantityReward.ToString();

        idRecompensa = 0;

        foreach (GameObject item in cartaRecompensa)
        {
            item.SetActive(false);
        }
    }

    public void Open()
    {
        if (quantityReward > 0)
        {

            switch (caseType)
            {
                case CaseType.Comum:
                    RandomReward();
                    break;
                case CaseType.rara:
                    if (!getRare)
                    {
                        GetCard(TypeCard.RARE);
                        getRare = true;
                    }
                    else
                    {
                        RandomReward();
                    }
                    break;
                case CaseType.Epica:
                    if (!getEpic)
                    {
                        GetCard(TypeCard.EPIC);
                        getEpic = true;
                    }
                    else if (!getRare)
                    {
                        GetCard(TypeCard.RARE);
                        getRare = true;
                    }
                    else
                    {
                        RandomReward();
                    }

                    break;
                case CaseType.Lendaria:
                    if (!getLegendary)
                    {
                        GetCard(TypeCard.LEGENDARY);
                        getLegendary = true;
                    }
                    else if (!getEpic)
                    {
                        GetCard(TypeCard.EPIC);
                        getEpic = true;
                    }
                    else if (!getRare)
                    {
                        GetCard(TypeCard.RARE);
                        getRare = true;
                    }
                    else
                    {
                        RandomReward();
                    }
                    break;

            }

            quantityReward--;
            textValue.text = quantityReward.ToString();

            if (quantityReward <= 0)
            {
                btnMaleta.SetActive(false);
                btnMaletaFechar.SetActive(true);
            }

            idRecompensa++;
        }

    }

    private void CardType()
    {
        int randB = Random.Range(0, 100);

        if (randB >= 99) // 1% 
        {
            GetCard(TypeCard.LEGENDARY);
        }
        else if (randB >= 94) // 5%
        {
            GetCard(TypeCard.EPIC);
        }
        else if (randB >= 79) // 15%
        {
            GetCard(TypeCard.RARE);
        }
        else  // 79%
        {
            GetCard(TypeCard.COMMON);
        }
    }

    private void RandomReward()
    {
        //definir a recompensa 
        //10 % chance carta, 15 % chance de ganhar gema, 75% ouro

        cartaRecompensa[idRecompensa].SetActive(true);

        int randA = Random.Range(0, 100);
        if (randA >= 25)
        {
            print("OURO");
            cartaRecompensa[idRecompensa].GetComponent<RewardInfo>().ExibirRecompensa(0, Random.Range(100, 1000));         
        }
        else if (randA >= 10)
        {
            print("GEMA");
            cartaRecompensa[idRecompensa].GetComponent<RewardInfo>().ExibirRecompensa(1, Random.Range(10, 30));
        }
        else
        {
            CardType();
        }
    }


    public void SetType(int value, CaseType type)
    {
        btnMaleta.SetActive(true);
        btnMaletaFechar.SetActive(false);
        idRecompensa = 0;

        foreach (GameObject item in cartaRecompensa)
        {
            item.SetActive(false);
        }

        quantityReward = value;
        caseType = type;
        textValue.text = quantityReward.ToString();

        // ver se isso dos bools vai dar problema
        getRare = false;
        getEpic = false;
        getLegendary = false;
    }

    public void GetCard(TypeCard type)
    {
        RewardInfo rInfo = cartaRecompensa[idRecompensa].GetComponent<RewardInfo>();
        cartaRecompensa[idRecompensa].SetActive(true);

        switch (type)
        {
            case TypeCard.LEGENDARY:
                print("RECEBI LENDARIA");
                rInfo.carta = gameManager.lendario[Random.Range(0, gameManager.lendario.Length)];
                rInfo.ExibirRecompensa(2, 1);
                break;
            case TypeCard.EPIC:
                print("RECEBI epica");
                rInfo.carta = gameManager.epico[Random.Range(0, gameManager.epico.Length)];
                rInfo.ExibirRecompensa(2, Random.Range(1,4));
                break;
            case TypeCard.RARE:
                print("RECEBI rara");
                rInfo.carta = gameManager.raro[Random.Range(0, gameManager.raro.Length)];
                rInfo.ExibirRecompensa(2, Random.Range(3, 10));
                break;
            case TypeCard.COMMON:
                print("RECEBI comum");
                rInfo.carta = gameManager.comum[Random.Range(0, gameManager.comum.Length)];
                rInfo.ExibirRecompensa(2, Random.Range(5, 16));
                break;

        }
    }
}
