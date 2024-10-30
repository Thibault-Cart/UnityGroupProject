using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnquit : MonoBehaviour
{
    private Button yourButton;

    // Start is called before the first frame update
    void Start()
    {
        yourButton = this.GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
