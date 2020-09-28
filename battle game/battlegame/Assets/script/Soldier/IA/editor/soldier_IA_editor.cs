using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(soldier_IA))]
public class soldier_IA_editor : Editor
{
    private void OnSceneGUI()
    {
        soldier_IA Fow = (soldier_IA)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(Fow.transform.position, Vector3.up, Vector3.forward, 360, Fow.AreaDiVista);
        Vector3 viewangleA = Fow.DirFromAngle(-Fow.soldierdata.AreaDiVista / 2, false);
        Vector3 viewangleB = Fow.DirFromAngle(Fow.soldierdata.AreaDiVista / 2, false);

        Handles.DrawLine(Fow.transform.position, Fow.transform.position + viewangleA * Fow.AreaDiVista);
        Handles.DrawLine(Fow.transform.position, Fow.transform.position + viewangleB * Fow.AreaDiVista);

        Handles.color = Color.red;
        foreach(Transform visTarget in Fow.VisibleTarget)
        {
            Handles.DrawLine(Fow.transform.position, visTarget.position);
        }
    }
}
#endif
