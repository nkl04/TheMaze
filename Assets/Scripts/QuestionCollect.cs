using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCollect : MonoBehaviour
{
   public bool isAnsweringQuestion = false;

   
   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.gameObject.CompareTag("Player")){
            ShowQuestionCanvas();
            Destroy(collision.gameObject);
       }
   }
   void ShowQuestionCanvas(){

   }
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
