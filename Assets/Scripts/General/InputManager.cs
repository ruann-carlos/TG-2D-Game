using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private float horizontal, vertical;
    private Vector3 playerDirection;
    public static Action<Vector3, float, float> OnPlayerMoved;
    public static Func<IEnumerator> OnPlayerDashed;

    void Update()
    {
        if (Input.GetButtonDown(buttonName: "Jump"))
        {
            if (OnPlayerDashed != null)
            {
                Debug.Log("entrou");
                StartCoroutine(OnPlayerDashed());
            }
        }
    }

    private void FixedUpdate()
    {
        GetPlayerDirection();
        NotificateMove();
    }

    private Vector3 GetPlayerDirection()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical   = Input.GetAxisRaw("Vertical");
        return playerDirection = new Vector3(horizontal, vertical, 0).normalized;
    }

    private void NotificateMove()
    {
        if(OnPlayerMoved != null)
        {
            OnPlayerMoved(playerDirection, horizontal, vertical);
        }
    }
}
