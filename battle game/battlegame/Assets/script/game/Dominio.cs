using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dominio : MonoBehaviour
{
    public int dominio = 0;

    public bool dominance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dominio == 30)
        {
            Debug.Log("dominio");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("soldier"))
        {
            dominance = true;
            StartCoroutine(DominioCounter());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("soldier"))
        {
            dominance = false;
            StopCoroutine(DominioCounter());
        }
    }

    IEnumerator DominioCounter()
    {
        while (dominance == true)
        {
            dominio += 1;
            yield return new WaitForSeconds(1f);
        }      
    }
}
