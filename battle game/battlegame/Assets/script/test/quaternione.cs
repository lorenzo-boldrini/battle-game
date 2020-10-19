using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quaternione : MonoBehaviour
{
    [Range(0,360)]
    public float angolo;
    
    [Range(0,360)]
    public float lunghezza;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward + new Vector3(0, angolo, 0) * lunghezza);
        Gizmos.DrawRay(transform.position, transform.forward + new Vector3(0, -angolo, 0) * lunghezza);
    }
}
