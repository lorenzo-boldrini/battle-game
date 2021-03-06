﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dominio : MonoBehaviour
{
    public bool dominioY;

    int tempoPerDominare = 20;

    public int tempoDominioBuoni;
    public int tempoDominioCattivi;

    public bool dominanceBuoni;
    public bool dominanceCattivi;

    public List<Soldier> buoni = new List<Soldier>();
    public List<Soldier> cattivi = new List<Soldier>();

    void Start()
    {
        StartCoroutine(DominioCounter());
    }

    void Update()
    {
        if (dominioY == true)
        {
            if (tempoDominioBuoni >= tempoPerDominare)
            {
                GameManager.Singleton.dominioYBuoni = true;
            }

            if (tempoDominioCattivi >= tempoPerDominare)
            {
                GameManager.Singleton.dominioYCattivi = true;
            }
        }
        else
        {
            if (tempoDominioBuoni >= tempoPerDominare)
            {
                GameManager.Singleton.dominioZBuoni = true;
            }

            if (tempoDominioCattivi >= tempoPerDominare)
            {
                GameManager.Singleton.dominioZCattivi = true;
            }
        }

        if (GameManager.Singleton.timeOut)
        {
            if (dominioY == true)
            {
                GameManager.Singleton.tempoDominioYBuoni = tempoDominioBuoni;
                GameManager.Singleton.tempoDominioYCattivi = tempoDominioCattivi;
            }
            else
            {
                GameManager.Singleton.tempoDominioZBuoni = tempoDominioBuoni;
                GameManager.Singleton.tempoDominioZCattivi = tempoDominioCattivi;
            }
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("soldier"))
        {
            bool isBuono = other.GetComponent<Soldier>().buono;
            
            if (isBuono == true)
            {
                buoni.Add(other.GetComponent<Soldier>());
            }
            else
            {
                cattivi.Add(other.GetComponent<Soldier>());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("soldier"))
        {
            if (buoni.Count > cattivi.Count)
            {
                dominanceBuoni = true;
                dominanceCattivi = false;
            }
            else if (buoni.Count < cattivi.Count)
            {
                dominanceCattivi = true;
                dominanceBuoni = false;
            }  
            else
            {
                dominanceCattivi = false;
                dominanceBuoni = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("soldier"))
        {
            bool isBuono = other.GetComponent<Soldier>().buono;

            if (isBuono == true)
            {
                buoni.Remove(other.GetComponent<Soldier>());

                if (buoni.Count == 0)
                {
                    dominanceBuoni = false;
                }
            }
            else
            {
                cattivi.Remove(other.GetComponent<Soldier>());

                if (cattivi.Count == 0)
                {
                    dominanceCattivi = false;
                }
            }
        }
    }

    IEnumerator DominioCounter()
    {

       while (true)
       {
            if (dominanceBuoni == true)
            {
                tempoDominioBuoni += 1;
                yield return new WaitForSeconds(1f);
            }
            else if (dominanceCattivi == true)
            {
                tempoDominioCattivi += 1;
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(0.2f);
            //POTREBBE RALLENTARE IL GIOCO, QUESTA CONDIZIONE SI VERIFICA QUANDO IL PUNTO DI DOMINIO E' VUOTO, AUMENTARE I SECONDI PER DIMINUIRE I CICLI DEL WHILE MA COMPORTA UN RITARDO NEL MOMENTO IN CUI ENTRA UN PERSONAGGIO E IL CICLO E' GIA' PARTITO 
       }
    }
}

