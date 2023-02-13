/*
 * This is an editor helper script.
 * It's made to make the component pure.
 * Please, don't edit it if you want to see the component clean.
 * It removes the Script field, making it look just like a Unity built-in component.
 * 
 * Ignore the Unity warning: "No MonoBehaviour scripts in file, or their names do not match the file name."
 * It's not intended to be and addible component.
 */



using System;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class HideScriptFieldAttribute : Attribute { }

[CustomEditor(typeof(MonoBehaviour), true)]
public class DefaultMonoBehaviourEditor : Editor
{
    private bool hideScriptField;

    private void OnEnable()
    {
        hideScriptField = target.GetType().GetCustomAttributes(typeof(HideScriptFieldAttribute), false).Length > 0;
    }

    public override void OnInspectorGUI()
    {
        if (hideScriptField)
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            DrawPropertiesExcluding(serializedObject, "m_Script");
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
        else
        {
            base.OnInspectorGUI();
        }
    }
}