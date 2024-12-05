using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_UI : MonoBehaviour
{
    // Delay of 3 frame for remote to connect;
    int i = 3;
    public player player;

    void Update()
    {
        if (i > 0)
        {
            i--;
            #if UNITY_ANDROID
            if (i == 0 && !UnityEditor.EditorApplication.isRemoteConnected && !Application.isMobilePlatform)
                gameObject.SetActive(false);
            #endif
        }
    }

    public void Left()
    {
        player.hor -= 1;
    }

    public void Right()
    {
        player.hor += 1;
    }
    public void Jump()
    {
        player.jump = true;
    }
    public void UnJump()
    {
        player.jump = false;
    }
}
