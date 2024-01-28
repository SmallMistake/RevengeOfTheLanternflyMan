using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

namespace IntronDigital
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MinimapTilemapDuplicator), true)]
    /// <summary>
    /// Custom editor for the Setup Functions component
    /// </summary>
    public class MinimapTilemapDuplicatorEditor : Editor
    {
        /// <summary>
        /// Gets the target inventory component.
        /// </summary>
        /// <value>The inventory target.</value>
        public MinimapTilemapDuplicator minimapTilemapDuplicatorTarget
        {
            get
            {
                return (MinimapTilemapDuplicator)target;
            }
        }

        /// <summary>
        /// Custom editor for the inventory panel.
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            // if there's a change in the inspector, we resize our inventory and grid, and redraw the whole thing.

            Editor.DrawPropertiesExcluding(serializedObject, new string[] { });

            // if for some reason we don't have a target inventory, we do nothing and exit
            if (minimapTilemapDuplicatorTarget == null)
            {
                return;
            }

            // we add a button to manually empty the inventory
            EditorGUILayout.Space();
            if (GUILayout.Button("Duplicate Tilemap"))
            {
                minimapTilemapDuplicatorTarget.DuplicateTileMap();
                SceneView.RepaintAll();
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("Clear Tilemap"))
            {
                minimapTilemapDuplicatorTarget.ClearTilemap();
                SceneView.RepaintAll();
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                SceneView.RepaintAll();
            }

            // we apply our changes
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
    }
}