using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground_0 : MonoBehaviour
{
    [Header("Layer Setting")]
    public float[] Layer_Speed = new float[5];
    public GameObject[] Layer_Objects = new GameObject[5];

    private Transform _camera;
    private Vector2[] startPos = new Vector2[5]; // Now using Vector2 to store both x and y start positions
    private Vector2 boundSize;
    private Vector2 size;

    void Start()
    {
        _camera = Camera.main.transform;

        size.x = Layer_Objects[0].transform.localScale.x;    
        size.y = Layer_Objects[0].transform.localScale.y;

        SpriteRenderer spriteRenderer = Layer_Objects[0].GetComponent<SpriteRenderer>();
        boundSize.x = spriteRenderer.sprite.bounds.size.x;
        boundSize.y = spriteRenderer.sprite.bounds.size.y;

        for (int i = 0; i < 5; i++){
            startPos[i] = new Vector2(0, 1.5f);
        }
    }

    void Update(){

        for (int i = 0; i < 5; i++)
        {
            float tempX = (_camera.position.x * (1 - Layer_Speed[i]));
            float distanceX = _camera.position.x * Layer_Speed[i];
            float tempY = (_camera.position.y * (1 - Layer_Speed[i]));
            float distanceY = _camera.position.y * Layer_Speed[i];

            Layer_Objects[i].transform.position = new Vector3(startPos[i].x + distanceX, startPos[i].y + distanceY, 15);

            if (tempX > startPos[i].x + boundSize.x * size.x)
            {
                startPos[i].x += boundSize.x * size.x;
            }
            else if (tempX < startPos[i].x - boundSize.x * size.x)
            {
                startPos[i].x -= boundSize.x * size.x;
            }

            if (tempY > startPos[i].y + boundSize.y * size.y)
            {
                startPos[i].y += boundSize.y * size.y;
            }
            else if (tempY < startPos[i].y - boundSize.y * size.y)
            {
                startPos[i].y -= boundSize.y * size.y;
            }
        }
    }
}
