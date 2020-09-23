﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_Data : ScriptableObject
{
    //aspetti grafici
    public GameObject ModelloSoldato;
    public Sprite immagineSoldato;
    // statistiche base soldato
    public float VelocitaMovimento;
    public float AreaDiVista;
    public float Esperienza;
    public float TempoDiReazione;
    public float Vita;
}