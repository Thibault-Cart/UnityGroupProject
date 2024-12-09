using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_UI : MonoBehaviour
{
    public player player;

    void Start()
    {
        if (!Application.isMobilePlatform)
            gameObject.SetActive(false);
    }

    public void Activate()
    {
        if (Application.isMobilePlatform)
            gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
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
