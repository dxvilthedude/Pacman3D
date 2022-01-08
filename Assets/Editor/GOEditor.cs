using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gm = (GameManager)target;

        if (GUILayout.Button("CHECK"))
        {
            Debug.Log("insert function to check in GOEditor");
            // FUNCTION TO CHECK
        }
    }
}
