using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public Vector2Int cameraBoundsX = Vector2Int.zero;
    public Vector2Int cameraBoundsY = Vector2Int.zero;


    void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            new Vector2(cameraBoundsX.x, cameraBoundsY.x),
            new Vector2(cameraBoundsX.y, cameraBoundsY.x)
        );
        Gizmos.DrawLine(
            new Vector2(cameraBoundsX.x, cameraBoundsY.y),
            new Vector2(cameraBoundsX.y, cameraBoundsY.y)
        );
        Gizmos.DrawLine(
            new Vector2(cameraBoundsX.x, cameraBoundsY.x),
            new Vector2(cameraBoundsX.x, cameraBoundsY.y)
        );
        Gizmos.DrawLine(
            new Vector2(cameraBoundsX.y, cameraBoundsY.x),
            new Vector2(cameraBoundsX.y, cameraBoundsY.y)
        );

        Gizmos.color = Color.white;
#endif
    }
}
