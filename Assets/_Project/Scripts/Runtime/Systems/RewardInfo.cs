using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardInfo : MonoBehaviour
{
    private GameManager gameManager;

    public Image icone;
    public TextMeshProUGUI descricao;
    public Sprite icoMoeda, icoGema;
    public Card carta;

    public void ExibirRecompensa(int tipoRecompensa, double qtdRecebida)
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        // 0 - ouro, 1 - gema, 2 - carta

        switch (tipoRecompensa)
        {
            case 0:
                icone.sprite = icoMoeda;
                descricao.text = gameManager.MonetaryConverter(qtdRecebida) + " Moedas";
                break;
            case 1:
                icone.sprite = icoGema;
                descricao.text = qtdRecebida + " Gemas";
                break;
            case 2:
                icone.sprite = carta.spriteCard;
                descricao.text = qtdRecebida + "" + carta.cardName;
                break;
        }
    }
}
