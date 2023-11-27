using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public string value;
    public TextMeshPro popUpText;
    public Color colorEnd;

    private Rigidbody2D popUpRb;

    private void Start()
    {
        popUpRb = GetComponent<Rigidbody2D>();
        popUpRb.AddForce(Vector2.up * 150);
        popUpText.GetComponent<MeshRenderer>().sortingLayerName = "HUD";
        popUpText.text = value;
        Destroy(gameObject, 0.5f);
    }


}
