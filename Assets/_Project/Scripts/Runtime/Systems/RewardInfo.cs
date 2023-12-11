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

    public void ExibirRecompensa(int tipoRecompensa)
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        // 0 - ouro, 1 - gema, 2 - carta

        double qtdRecebida;

        switch (tipoRecompensa)
        {
            case 0:
                qtdRecebida = Random.Range((float)gameManager.saveGame.goldEarned / 5, 
                    (float)gameManager.saveGame.goldEarned / 3);
                gameManager.GetCoin(qtdRecebida);
                icone.sprite = icoMoeda;
                descricao.text = gameManager.MonetaryConverter(qtdRecebida) + " Moedas";
                break;
            case 1:
                qtdRecebida = Random.Range(1,51);
                gameManager.GetGemas((int)qtdRecebida);
                icone.sprite = icoGema;
                descricao.text = qtdRecebida + " Gemas";
                break;
            case 2:
                if (!carta.isRelesead)
                {
                    carta.isRelesead = true;
                }

                carta.SetCardCollect(1);
                gameManager.AtualizarCartaRecebida();
                icone.sprite = carta.spriteCard;
                descricao.text = 1 + carta.cardName;
                break;
        }
    }
}
