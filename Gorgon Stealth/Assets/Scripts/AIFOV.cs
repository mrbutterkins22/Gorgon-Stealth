using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFOV : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] float sightlineDelay = 1f;
    [SerializeField] Sprite deathSprite;

    private bool search = true;

    private void Start()
    {
        StartCoroutine(_FindVisibleTargets(sightlineDelay));
    }

    public Vector3 DirectionFromAngle(float angle, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)); 
    }

    IEnumerator _FindVisibleTargets(float delay)
    {
        while(search)
        {
            if (delay < 0) yield return new WaitForFixedUpdate();
            else yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    IEnumerator _PlayAlertSound()
    {
        AudioSource source = GameObject.Find("alertSource").GetComponent<AudioSource>();
        source.Play();
        yield return new WaitForSeconds(3);
        source.Stop();
    }

    private void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        AudioSource source = GameObject.Find("alertSource").GetComponent<AudioSource>();
        foreach (Collider c in targetsInViewRadius)
        {
            Transform target = c.transform;
            Vector3 DirectionToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, DirectionToTarget) < viewAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, DirectionToTarget, distance, obstacleMask))
                {
                    Transform healthBar = transform.Find("Sprite").Find("HealthBar");
                    float healthScale = healthBar.localScale.x;
                    Debug.Log(gameObject.name + " sees the player.");
                    if (sightlineDelay < 0) {
                        healthBar.localScale = new Vector3(healthScale -= Time.fixedDeltaTime, 1, 1);
                        if (!source.isPlaying) StartCoroutine(_PlayAlertSound());
                    } 
                    else
                    {
                        healthBar.localScale = new Vector3(healthScale -= (sightlineDelay / 2), 1, 1);
                        if (!source.isPlaying) StartCoroutine(_PlayAlertSound());
                    }
                    if(healthScale < 0)
                    {
                        source.Stop();
                        AudioSource source2 = GameObject.Find("petrifySource").GetComponent<AudioSource>();
                        source2.Play();
                        GetComponent<NavMeshAgent>().enabled = false;
                        Debug.Log(gameObject.name + " has been petrified.");
                        transform.Find("Sprite").gameObject.GetComponent<Animator>().SetBool("dead", true);
                        healthBar.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        search = false;
                        GameObject.Find("UIController").GetComponent<UIController>().IncreaseSuspicionTally();
                    }
                }
            }
        }
    }
}
