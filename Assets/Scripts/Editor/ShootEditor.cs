using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Shoot))]
public class ShootEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Shoot shoot = (Shoot)target;
        if (GUILayout.Button("Shoot", GUILayout.Height(40)))
        {
            shoot.ShootRocket();
        }
    }
}