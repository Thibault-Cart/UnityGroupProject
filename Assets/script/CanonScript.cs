using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonScript : MonoBehaviour
{

    public GameObject ShootPoint;
    public GameObject bullet;
    public float shootingIntterval = 10f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // increment timer

        if (timer >= shootingIntterval) // if ten second passed
        {
            // Action à effectuer toutes les 10 secondes
            print("canon shoot");
            Instantiate(bullet, ShootPoint.transform.position, ShootPoint.transform.rotation);
            timer = 0f; // reinitilize
        }
    }
}
