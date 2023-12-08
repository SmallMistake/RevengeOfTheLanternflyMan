using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

namespace IntronDigital
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ProgressManager), true)]
    /// <summary>
    /// Custom editor for the Setup Functions component
    /// </summary>
    public class ProgressManagerEditor : Editor
    {
        /// <summary>
        /// Gets the target inventory component.
        /// </summary>
        /// <value>The inventory target.</value>
        public ProgressManager progressManagerTarget
        {
            get
            {
                return (ProgressManager)target;
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
            if (progressManagerTarget == null)
            {
                return;
            }

            // Test Save Can make this more versitile to save to other slots in the future
            EditorGUILayout.Space();
            if (GUILayout.Button("Save to Slot 1"))
            {
                progressManagerTarget.CreateSaveGame(1);
                SceneView.RepaintAll();
            }

            if (GUILayout.Button("Load Slot 1"))
            {
                progressManagerTarget.LoadSavedProgress();
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