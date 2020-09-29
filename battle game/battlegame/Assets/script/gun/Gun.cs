using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10;
    public float range = 100;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit Hit;
       if(Physics.Raycast(transform.position, transform.forward, out Hit, range))
        {
            Debug.Log(Hit.transform.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * range);
    }
}
