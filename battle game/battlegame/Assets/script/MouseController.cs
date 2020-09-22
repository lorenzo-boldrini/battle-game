using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseController : MonoBehaviour
{
    GameObject Soldier;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Soldier == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.tag == "soldier")
                {
                    Soldier = hit.transform.gameObject;
                    Soldier.GetComponent<MeshRenderer>().material.SetColor("_color", Color.green);
                    print(Soldier);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && Soldier != null)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.tag == "Plane")
                {
                    Soldier.GetComponent<NavMeshAgent>().destination = hit.point;
                    Soldier.GetComponent<MeshRenderer>().material.SetColor("_color", Color.white);
                    Soldier = null;
                }
            }
        }
    }
}
