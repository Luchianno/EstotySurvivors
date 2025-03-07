using UnityEditor;
using UnityEngine;

// custom inspector for the EnemySpawner component, with buttons to spawn enemies 
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var spawner = (EnemySpawner)target;

        if (GUILayout.Button("Spawn Individual Enemy", GUILayout.Height(50)))
        {
            spawner.SpawnIndividualEnemy();
        }

        if (GUILayout.Button("Spawn Group of Enemies", GUILayout.Height(50)))
        {
            spawner.SpawnEnemyGroup(5);
        }
    }
}
