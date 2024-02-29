using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
   [SerializeField] float timeToAnswerQuestion = 30f; 
    float timeValue;
    public float fillFraction;
    public bool isAnsweringQuestion = false;
    void UpdateTimer()
    {
        timeValue -= Time.deltaTime;
        
        if (isAnsweringQuestion)
        {
            if (timeValue > 0){
                fillFraction = timeValue / timeToAnswerQuestion;
                
            }
            else
            {
                isAnsweringQuestion = false;
                timeValue = timeToAnswerQuestion;
            }
        }
        else
        {
            if (timeValue > 0)
            {
                fillFraction = timeValue / timeToAnswerQuestion;
            }
            else
            {
                isAnsweringQuestion = true;
                timeValue = timeToAnswerQuestion;
            }
        }
    }

    void Update()
    {
       UpdateTimer();
    }
}
