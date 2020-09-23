using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    private void OnEnable()
    {
        Singleton = this;
    }

    public bool dominioYBuoni;
    public bool dominioZBuoni;
    public bool dominioYCattivi;
    public bool dominioZCattivi;

    public bool TimeOut;

    public int tempoDominioYBuoni;
    public int tempoDominioYCattivi;
    public int tempoDominioZBuoni;
    public int tempoDominioZCattivi;

    public void Update()
    {
        if (dominioYBuoni && dominioZBuoni)
        {
            //hanno vinto i buoni
        }
        else if (dominioYCattivi && dominioZCattivi)
        {
            //hannovinto i cattivi 
        }
        else if (dominioYBuoni && dominioZCattivi || dominioYCattivi && dominioZBuoni)
        {
            //pareggio
        }

        if (TimeOut == true)
        {
            int tempoTotaleBuoni = tempoDominioYBuoni + tempoDominioZBuoni;
            int tempoTotaleCattivi = tempoDominioYCattivi + tempoDominioZCattivi;

            if (tempoTotaleBuoni > tempoTotaleCattivi)
            {
                //hanno vinto i buoni
            }
            else if (tempoTotaleCattivi > tempoTotaleBuoni)
            {
                //hanno vinto i cattivi
            }
            else
            {
                //pareggio
            }
        }
    }
}
