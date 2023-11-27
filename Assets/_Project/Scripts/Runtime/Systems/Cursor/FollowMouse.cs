using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float distance = 1f;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distance;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
