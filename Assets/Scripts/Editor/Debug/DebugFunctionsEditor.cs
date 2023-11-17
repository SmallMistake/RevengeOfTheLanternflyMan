using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

namespace IntronDigital
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DebugFunctions), true)]
    /// <summary>
    /// Custom editor for the InventoryDisplay component
    /// </summary>
    public class DebugFunctionsEditor : Editor
    {
        /// <summary>
        /// Gets the target inventory component.
        /// </summary>
        /// <value>The inventory target.</value>
        public DebugFunctions debugFunctionsTarget
        {
            get
            {
                return (DebugFunctions)target;
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
            if (debugFunctionsTarget == null)
            {
                return;
            }

            // we add a button to kill the player
            EditorGUILayout.Space();
            if (GUILayout.Button("Kill Player"))
            {
                debugFunctionsTarget.KillPlayer();
                SceneView.RepaintAll();
            }

            // we add a button to finish the level
            EditorGUILayout.Space();
            if (GUILayout.Button("Finish Level"))
            {
                debugFunctionsTarget.FinishLevel();
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