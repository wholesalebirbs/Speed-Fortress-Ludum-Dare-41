using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon2DColliderVisualizer : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;

        PolygonCollider2D col = GetComponent<PolygonCollider2D>();

        if (col == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        //Gizmos.DrawWireCube(transform.position, col.bounds.extents * 2);


        for (int i = 0; i < col.points.Length - 1; i++)
        {
            Gizmos.DrawLine(transform.position + (Vector3)col.points[i], transform.position+ (Vector3)col.points[i + 1] );
        }

        Gizmos.DrawLine(transform.position + (Vector3)col.points[col.points.Length - 1], transform.position + (Vector3)col.points[0]);
        
        
    }
}
