using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : MonoBehaviour
{

    public float speed = 50.0f;
    public float lifeTime = 10.0f;

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print("avance");
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= 10.0f)
        {
            Destroy(this.gameObject);
            timer = lifeTime;
        }

    }
}
