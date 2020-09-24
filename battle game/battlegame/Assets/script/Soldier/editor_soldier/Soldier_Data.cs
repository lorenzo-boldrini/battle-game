using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_Data : ScriptableObject
{
    //aspetti grafici
    public GameObject ModelloSoldato;
    // statistiche base soldato
    public string SoldierName;
    public float VelocitaMovimento;
    public float AreaDiVista;
    public float Esperienza;
    public float TempoDiReazione;
    public float Vita;
}
