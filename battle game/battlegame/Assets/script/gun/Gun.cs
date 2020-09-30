using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100;
    public soldier_IA _padreArma;
    public GameObject puntoDiSparo;

    float NextFire;

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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotatio, Time.deltaTime / mobilitaArma /*da sostituire*/);


            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit Hit;
       if(Physics.Raycast(puntoDiSparo.transform.position, transform.forward, out Hit, range) && Time.time >= NextFire)
        {
            FireParticle.Play();
            NextFire = Time.time + 1/rateoDiFuoco;
            Debug.Log(Hit.transform.name);
            Hit.transform.gameObject.GetComponent<soldier_IA>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(puntoDiSparo.transform.position, transform.forward * range);
    }
}
