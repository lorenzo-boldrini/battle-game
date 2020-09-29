using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseController : MonoBehaviour
{
    GameObject Soldier;

    public LayerMask ignoreRaycast;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && Soldier == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out hit, ignoreRaycast))
            {
                if (hit.transform.gameObject.tag == "soldier")
                {
                    Soldier = hit.transform.gameObject;
                    Soldier.GetComponent<MeshRenderer>().material.SetColor("_color", Color.green);
                    print(Soldier);
                }
            }
        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && Soldier != null)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.tag == "Plane")
                {
                    Soldier.GetComponent<NavMeshAgent>().destination = hit.point;
                    Soldier = null;
                }
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && Soldier == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, ignoreRaycast))
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
#endif
    }
}
