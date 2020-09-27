using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int tempoDominioYBuoni;
    public int tempoDominioYCattivi;
    public int tempoDominioZBuoni;
    public int tempoDominioZCattivi;

    public Text dominioY;
    public Text dominioZ;
    public Text Victory;

    public bool timeOut;
    public int tempoDelGioco;
    public int tempoMaxDelGioco = 50;

    private void Start()
    {
        StartCoroutine(TempoDelGioco());
    }

    public void Update()
    {
        if (dominioYBuoni)
        {
            dominioY.text = "I buoni hanno ottenuto il dominio di Y";
        }
        else if (dominioYCattivi)
        {
            dominioY.text = "I cattivi hanno ottenuto il dominio di Y";
        }

        if (dominioZBuoni)
        {
            dominioZ.text = "I buoni hanno ottenuto il dominio di Z";
        }
        else if (dominioZCattivi)
        {
            dominioZ.text = "I cattivi hanno ottenuto il dominio di Z";
        }

        if (dominioYBuoni && dominioZBuoni)
        {
            Victory.text = "Hanno vinto i Buoni";
        }
        else if (dominioYCattivi && dominioZCattivi)
        {
            Victory.text = "Hanno vinto i Cattivi";
        }
        else if (dominioYBuoni && dominioZCattivi || dominioYCattivi && dominioZBuoni)
        {
            Victory.text = "Pareggio, nessun vincitore";
        }

        if (tempoDelGioco >= tempoMaxDelGioco)
        {
            timeOut = true;

            int tempoTotaleBuoni = tempoDominioYBuoni + tempoDominioZBuoni;
            int tempoTotaleCattivi = tempoDominioYCattivi + tempoDominioZCattivi;

            if (tempoTotaleBuoni > tempoTotaleCattivi)
            {
                Victory.text = "Hanno vinto i Buoni";
            }
            else if (tempoTotaleCattivi > tempoTotaleBuoni)
            {
                Victory.text = "Hanno vinto i Cattivi";
            }
            else
            {
                Victory.text = "Pareggio, nessun vincitore";
            }
        }
    }

    IEnumerator TempoDelGioco()
    {
        while (true)
        {
            tempoDelGioco++;
            yield return new WaitForSeconds(1f);
        }
    }
}
