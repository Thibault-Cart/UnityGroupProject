using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{

    // Public variable to control the rotation speed from the Unity editor
    public float speed = 30f;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the amount of rotation based on time and speed
        float rotationAmount = speed * Time.deltaTime;

        // Apply rotation around the Y axis (vertical axis)
        transform.Rotate(0, 0, rotationAmount);
    }
}

