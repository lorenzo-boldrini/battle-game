using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health_bar : MonoBehaviour
{
    public float starting_health;
    public Image Healthbar;

    // Start is called before the first frame update
    void Start()
    {
        Healthbar = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    //creare una funzione a cui viene passata la var la soldierdata.vita 
    public void removeLife(float currentLife)
    {
        Healthbar.fillAmount = (currentLife / starting_health);
    }

}
