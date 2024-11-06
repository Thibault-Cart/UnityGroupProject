using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnquit : MonoBehaviour
{
    private Button yourButton;
    void Start()
    {
        yourButton = this.GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        Application.Quit();
    }
}
