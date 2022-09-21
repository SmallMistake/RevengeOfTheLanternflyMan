using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PointInDirection : MonoBehaviour
{
    public Color color = Color.magenta;
    [Range(0.1f, 4)]
    public float radius = 0.4f;
    public Vector3 direction = new Vector3(1f, 0, 0);

    #if UNITY_Editor
        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Handles.color = new Color(0.2F, 0.3F, 0.4F);
            Handles.Label(new Vector3(transform.position.x - 1f, transform.position.y + 1f, transform.position.z), gameObject.name);
            Gizmos.DrawSphere(transform.position, radius);
            Gizmos.DrawLine(transform.position, transform.TransformPoint(direction));
        }
    #endif
}
