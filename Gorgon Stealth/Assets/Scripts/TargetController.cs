using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] Vector3[] destinations;
    private int index = 0;

    private void Awake()
    {
        transform.position = destinations[index];
        index++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AI")
        {
            transform.position = destinations[index];
            other.gameObject.GetComponent<AIMovement>().SetDestination(transform);
            index++;
            if(index > destinations.Length-1)
            {
                index = 0;
            }
        }
    }
}
