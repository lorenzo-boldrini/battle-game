using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class soldier_IA : MonoBehaviour
{
    NavMeshAgent _nma;
    public Basic_Soldier soldierdata;

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

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }



    //
}
