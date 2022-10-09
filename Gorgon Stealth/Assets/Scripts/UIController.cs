using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] float timeLimit = 300f;
    [SerializeField] float foodTimeIncrease = 0f;
    [SerializeField] int maxSuspicion = 3;
    private float minutes;
    private int seconds;
    private int totalFood;
    private int collectedFood = 0;
    private int suspicion = 0;

    private void Start()
    {
        minutes = timeLimit / 60;
        seconds = Mathf.RoundToInt(timeLimit % 60);
        totalFood = GameObject.FindGameObjectsWithTag("Food").Length;
        StartCoroutine(_timer());
        UpdateFoodTally();
        UpdateSuspicionTally();
    }

    private IEnumerator _timer()
    {
        while(timeLimit > 0)
        {
            seconds--;
            if(seconds < 0)
            {
                minutes--;
                seconds = 59;
            }
            TimerReadoutUpdate();
            yield return new WaitForSeconds(1);
        }

        //load game over screen here
    }

    private void UpdateFoodTally()
    {
        transform.Find("PlayerUI").Find("Food Readout").gameObject.GetComponent<TextMeshProUGUI>().text = "Food: " + collectedFood + "/" + totalFood;
        timeLimit += foodTimeIncrease;
        TimerReadoutUpdate();
        if(collectedFood == totalFood)
        {
            //load win screen here
        }
    }

    public void IncreaseFoodTally()
    {
        collectedFood++;
        UpdateFoodTally();
    }

    private void UpdateSuspicionTally()
    {
        transform.Find("PlayerUI").Find("Sus Readout").gameObject.GetComponent<TextMeshProUGUI>().text = "Suspicion: " + suspicion;
        if(suspicion > maxSuspicion)
        {
            //load game over screen here
        }
    }

    public void IncreaseSuspicionTally()
    {
        suspicion++;
        UpdateSuspicionTally();
    }

    private void TimerReadoutUpdate()
    {
        if (minutes < 10 && seconds < 10)
        {
            transform.Find("PlayerUI").Find("Timer").gameObject.GetComponent<TextMeshProUGUI>().text = "Time: 0" + minutes + ":0" + seconds;
        }
        else if (seconds < 10)
        {
            transform.Find("PlayerUI").Find("Timer").gameObject.GetComponent<TextMeshProUGUI>().text = "Time: " + minutes + ":0" + seconds;
        }
        else if (minutes < 10)
        {
            transform.Find("PlayerUI").Find("Timer").gameObject.GetComponent<TextMeshProUGUI>().text = "Time: 0" + minutes + ":" + seconds;
        }
        else transform.Find("PlayerUI").Find("Timer").gameObject.GetComponent<TextMeshProUGUI>().text = "Time: " + minutes + ":" + seconds;
    }
}
