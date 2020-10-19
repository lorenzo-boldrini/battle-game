using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Soldier_Data
{
    public string SoldierName;
    public float VelocitaMovimento;
    [Range(0,360)]
    public float AreaDiVista;
    public float Esperienza;
    public float TempoDiReazione;
    [Range(0, 360)]
    public float Precisone;
    public int Vita;
}
