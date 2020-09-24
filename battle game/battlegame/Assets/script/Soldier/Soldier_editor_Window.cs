using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Soldier_editor_Window : EditorWindow
{
    public Basic_Soldier newsoldierdata;
    [MenuItem("Window/Soldier Editor")]

    
   static void OpenWindow()
    {
        Soldier_editor_Window window = (Soldier_editor_Window)GetWindow(typeof(Soldier_editor_Window));
        window.minSize = new Vector2(200, 300);
        window.Show();
    }

    private void OnEnable()
    {
        newsoldierdata = new Basic_Soldier();
    }

    private void OnGUI()
    {
        drawsettings(newsoldierdata);
    }
    void drawsettings(Basic_Soldier soldierdata)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("prefab");
        soldierdata.ModelloSoldato = (GameObject) EditorGUILayout.ObjectField(soldierdata.ModelloSoldato, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("nome soldato");
        soldierdata.SoldierName = EditorGUILayout.TextField(soldierdata.SoldierName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Velocità Movimento");
        soldierdata.VelocitaMovimento = EditorGUILayout.FloatField(soldierdata.VelocitaMovimento);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("area di vista");
        soldierdata.AreaDiVista = EditorGUILayout.FloatField(soldierdata.AreaDiVista);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Esperienza");
        soldierdata.Esperienza = EditorGUILayout.FloatField(soldierdata.Esperienza);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Tempo Di Reazione");
        soldierdata.TempoDiReazione = EditorGUILayout.FloatField(soldierdata.TempoDiReazione);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Vita");
        soldierdata.Vita = EditorGUILayout.FloatField(soldierdata.Vita);
        EditorGUILayout.EndHorizontal();

        if(soldierdata.ModelloSoldato == null)
        {
            EditorGUILayout.HelpBox("manca il prefab", MessageType.Warning);
        }else if (soldierdata.SoldierName == null)
        {
            EditorGUILayout.HelpBox("manca il nome del soldato", MessageType.Warning);
        }
        else if(GUILayout.Button("Save Soldier", GUILayout.Height(30)))
        {
            save_soldier();
        }
    }

    void save_soldier()
    {
        string prefabpath;
        string newprefabPath = "Asset/soldier/prefab";
        string datapath = "Asset/soldier/data";

        datapath += newsoldierdata.SoldierName + ".asset";
        AssetDatabase.CreateAsset(newsoldierdata, datapath);

        newprefabPath += newsoldierdata.name + ".prefab";
        prefabpath = AssetDatabase.GetAssetPath(newsoldierdata.ModelloSoldato);
        AssetDatabase.CopyAsset(prefabpath, newprefabPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        GameObject soldato = (GameObject)AssetDatabase.LoadAssetAtPath(newprefabPath, typeof(GameObject));
        if (!soldato.GetComponent<soldato>())
            soldato.AddComponent(typeof(soldato));
        soldato.GetComponent<soldato>().soldier_data_prefab = newsoldierdata;
    }
}


