using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : MonoBehaviour
{

    public float speed = 50.0f;
    public float lifeTime = 10.0f;

    private float timer = 0f;


    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= 10.0f)
        {
            Destroy(this.gameObject);
            timer = lifeTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
