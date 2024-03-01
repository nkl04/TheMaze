using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
   [SerializeField] float timeToAnswerQuestion = 30f; 
   [SerializeField] float timeToTurnOffQuestion = 5f;
    float timeValue;
    public float fillFraction;
    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion;
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
                timeValue = timeToTurnOffQuestion;
            }
        }
        else
        {
            if (timeValue > 0)
            {
                fillFraction = timeValue / timeToTurnOffQuestion;
            }
            else
            {
                isAnsweringQuestion = true;
                timeValue = timeToAnswerQuestion;
                loadNextQuestion = true;
            }
        }
    }

    public void CancelTimer()
    {
        timeValue = 0;
    }

    void Update()
    {
       UpdateTimer();
    }
}
