using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject.Find("UIController").GetComponent<UIController>().IncreaseFoodTally();
            Destroy(gameObject);
        }
    }
}
