using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class InterfaceReference<T> where T : class
{
    [SerializeField] private UnityEngine.Object _object;
    public T Value => _object as T;
    public bool IsValid => _object is T;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InterfaceReference<>))]
public class InterfaceReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty objProp = property.FindPropertyRelative("_object");
        Type tInterface = fieldInfo.FieldType.GetGenericArguments()[0];

        EditorGUI.BeginProperty(position, label, property);
        label.text = $"{property.displayName} ({tInterface.Name})";

        var newObj = EditorGUI.ObjectField(position, label, objProp.objectReferenceValue, typeof(UnityEngine.Object),
            true);

        if (newObj != objProp.objectReferenceValue)
        {
            if (newObj == null || tInterface.IsInstanceOfType(newObj))
            {
                objProp.objectReferenceValue = newObj;
            }
            else if (newObj is GameObject go)
            {
                var comp = go.GetComponent(tInterface);
                objProp.objectReferenceValue = comp ? (UnityEngine.Object)comp : null;
                if (objProp.objectReferenceValue == null)
                    Debug.LogError($"Assigned object must implement {tInterface.Name}");
            }
            else if (newObj is Component c && tInterface.IsInstanceOfType(c))
            {
                objProp.objectReferenceValue = c;
            }
            else
            {
                Debug.LogError($"Assigned object must implement {tInterface.Name}");
                objProp.objectReferenceValue = null;
            }
        }

        EditorGUI.EndProperty();
    }
}
#endif