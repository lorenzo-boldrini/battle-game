using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class soldier_IA : MonoBehaviour
{
    NavMeshAgent _nma;
    public Soldier_Data soldierdata;

    private void Awake()
    {
        _nma = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _nma.speed = soldierdata.VelocitaMovimento;
        //avvio tempo di reazione su area di vista
        StartCoroutine("TempoDiReazioneVista", soldierdata.TempoDiReazione);
    }

    // area di vista

    public float AreaDiVista;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    public List<Transform> VisibleTarget = new List<Transform>();

    IEnumerator TempoDiReazioneVista(float ritardo)
    {
        while (true)
        {
            yield return new WaitForSeconds(ritardo);
            RagruppaNemici();
        }
    }

    void RagruppaNemici()
    {
        VisibleTarget.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, AreaDiVista, TargetMask);
        for(int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 DirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward,DirToTarget) < soldierdata.AreaDiVista / 2)
            {
                float disTarget = Vector3.Distance(transform.position, target.transform.position);
                if(!Physics.Raycast(transform.position, DirToTarget,disTarget, ObstacleMask))
                {
                    VisibleTarget.Add(target);
                }
            }
        }

    }

    private void Update()
    {
        //RagruppaNemici();
        if (VisibleTarget.Count > 0)
        {
            _nma.updateRotation = false;
            Vector3 watch_to = VisibleTarget[0].position - transform.position;

            Quaternion targetRotatio = Quaternion.LookRotation(watch_to);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotatio,Time.deltaTime / soldierdata.TempoDiReazione);
        }
        else
        {
            _nma.updateRotation = true;
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }



    public void TakeDamage(int damage)
    {
        if (soldierdata.Vita > 0)
        {
            soldierdata.Vita -= damage;
            Debug.Log(soldierdata.Vita);
        }
        if(soldierdata.Vita == 0)
        {
            Death();
        }
    }

    void Death()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
