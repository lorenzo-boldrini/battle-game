using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100;
    public soldier_IA _padreArma;
    public GameObject puntoDiSparo;

    float NextFire;
    bool isShooting;

    //variablie di test da sostituire con classe
    public float mobilitaArma;
    public float rateoDiFuoco;

    //aspetto grafico
    public ParticleSystem FireParticle;
    private void Start()
    {
        _padreArma = GetComponentInParent<soldier_IA>();
    }
    private void Update()
    {
        // mobilita arma
        if(_padreArma.VisibleTarget.Count > 0)
        {
            Vector3 watch_to = _padreArma.VisibleTarget[0].position - transform.position;

            Quaternion targetRotatio = Quaternion.LookRotation(watch_to);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotatio, Time.deltaTime / mobilitaArma);
        }
    }

    //fare funzione random che da precisione restitui
    public void Shoot()
    {
        if (Time.time >= NextFire)
        {
            NextFire = Time.time + 1 / rateoDiFuoco;
            FireParticle.Play();
            RaycastHit Hit;
            isShooting = true;

            if (Physics.Raycast(puntoDiSparo.transform.position, randomShot(_padreArma.soldierdata.Precisone / 2,false), out Hit, range))
            {
                Hit.transform.gameObject.GetComponent<soldier_IA>().TakeDamage(damage);
            }
            else if (!Physics.Raycast(puntoDiSparo.transform.position, transform.forward, out Hit, range))
            {
                Debug.Log("non hai colpito niente");
            }
        }
        else
        {
            isShooting = false;
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
    
    public Vector3 randomShot(float angleInDegrees, bool angleIsGlobal)
    {
        float randomizer = Random.Range(0, angleInDegrees);
        if (!angleIsGlobal)
        {
            randomizer += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(randomizer * Mathf.Deg2Rad), 0, Mathf.Cos(randomizer * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if(EditorApplication.isPlaying && isShooting)
        Gizmos.DrawRay(puntoDiSparo.transform.position, randomShot(_padreArma.soldierdata.Precisone / 2, false) * range);


        //angolo di sparo rispetto al soldato
        Gizmos.color = Color.green;

        var angoloA = DirFromAngle(_padreArma.soldierdata.Precisone / 2, false);
        var angoloB = DirFromAngle(-_padreArma.soldierdata.Precisone / 2, false);

        Gizmos.DrawLine(puntoDiSparo.transform.position,puntoDiSparo.transform.position + angoloA * _padreArma.soldierdata.Precisone);
        Gizmos.DrawLine(puntoDiSparo.transform.position, puntoDiSparo.transform.position + angoloB * _padreArma.soldierdata.Precisone);
    }
}
