using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[ExecuteInEditMode]
public class soldier_IA : MonoBehaviour
{
    //*----------> inserire un gameobject vuoto in cui viene inserita l'arma e nello start viene caricata l'arma
    //*----------> inserire gameobject vuoto in cui viene inserito l'accessorio
    //*----------> istanziare healthbar e dare un riferimento e inserirlo nella funzione damage
    NavMeshAgent _nma;
    public Soldier_Data soldierdata;
    Gun _soldierGun;


    GameObject HealthBar;
    public GameObject HealthBarPrefab;
    public GameObject HealthbarPather;


    private void Awake()
    {
        _nma = GetComponent<NavMeshAgent>();
        _soldierGun = GetComponentInChildren<Gun>();

        //genera healthbar e passa i dati
        HealthBar = Instantiate(HealthBarPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, HealthbarPather.transform.parent);
        HealthBar.GetComponent<health_bar>().starting_health = soldierdata.Vita;
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
        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 DirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, DirToTarget) < soldierdata.AreaDiVista / 2)
            {
                float disTarget = Vector3.Distance(transform.position, target.transform.position);
                if (!Physics.Raycast(transform.position, DirToTarget, disTarget, ObstacleMask))
                {
                    VisibleTarget.Add(target);
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (VisibleTarget.Count > 0)
        {
            _nma.updateRotation = false;
            Vector3 watch_to = VisibleTarget[0].position - transform.position;

            Quaternion targetRotatio = Quaternion.LookRotation(watch_to);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotatio, Time.deltaTime / soldierdata.TempoDiReazione);

            //spara  ---> nella funzione Shoot deve essere fornita la precisone del soldato
            _soldierGun.Shoot(/*soldierdata.Precisone*/);
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
            HealthBar.GetComponent<health_bar>().removeLife(soldierdata.Vita);
        }
        else if (soldierdata.Vita == 0)
        {
            Death();
        }
    }

    void Death()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, AreaDiVista);
        Vector3 viewangleA = DirFromAngle(soldierdata.AreaDiVista / 2, false);
        Vector3 viewangleB = DirFromAngle(-soldierdata.AreaDiVista / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewangleA * AreaDiVista);
        Gizmos.DrawLine(transform.position, transform.position + viewangleB * AreaDiVista);

        //angolo precisione

        Gizmos.color = Color.red;
        foreach (Transform visTarget in VisibleTarget)
        {
            Gizmos.DrawLine(transform.position, visTarget.position);
        }
    }
}
   
