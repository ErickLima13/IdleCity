using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    private Rigidbody2D coinRb;

    public float posY;

    private bool isJump;

    private void Start()
    {
        coinRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.y <= posY -0.5f && !isJump)
        {
            isJump = true;
            coinRb.velocity = Vector2.zero;
            coinRb.AddForce(new Vector2(35, 300));
            Destroy(gameObject,1f);
        }
    }
}
