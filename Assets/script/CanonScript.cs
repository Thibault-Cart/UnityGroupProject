using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CanonScript : MonoBehaviour
{

    public GameObject ShootPoint;
    public GameObject bullet;
    public float shootingIntterval = 10f;
    private float timer = 0f;
    private AudioSource boomsound;


    // Start is called before the first frame update
    void Start()
    {
        boomsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // increment timer

        if (timer >= shootingIntterval) // if ten second passed
        {
            // Action à effectuer toutes les 10 secondes
            boomsound.Play();
            Instantiate(bullet, ShootPoint.transform.position, ShootPoint.transform.rotation, transform);
            timer = 0f; // reinitilize
        }
    }
}
