using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewQuestion", menuName = "Quiz Question", order = 0)]
public class QuestionSO : ScriptableObject {
    //Create Questions
    [TextArea(2,6)]
    [SerializeField] string question = "Enter New Question";
    //
    public string GetQuestion(){
        return question;
    }
    //Get Answer
    [SerializeField] int correctAnswerIndex;
    [SerializeField] string[] answers = new string[4];
    public string GetAnswer(int index){
        return answers[index];
    }
    public int GetCorrectAnswerIndex(){
        return correctAnswerIndex;
    }

}
