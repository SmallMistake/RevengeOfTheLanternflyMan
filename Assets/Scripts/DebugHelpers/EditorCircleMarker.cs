using UnityEditor;
using UnityEngine;

public class EditorCircleMarker : MonoBehaviour
{
    public Color color = Color.cyan;
    [Range(0.1f, 4)]
    public float radius = 0.4f;

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Handles.color = new Color(0.2F, 0.3F, 0.4F);
            Gizmos.DrawSphere(transform.position, radius);
            Handles.Label(new Vector3(transform.position.x - 1f, transform.position.y + 1f, transform.position.z), gameObject.name);
        }
    #endif
}
