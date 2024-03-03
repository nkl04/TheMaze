using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> question = new List<QuestionSO>();
    QuestionSO currentQuestion;
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;
    [Header("Button Image")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start(){
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }
    void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }

    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++){
        TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = currentQuestion.GetAnswer(i);
        }
    }
    public void OnSelectedAnswer(int index){
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "That's Good!";
            
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }else {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Try Again IdiO";
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        SetButtonState(false);
        timer.CancelTimer();
    }

    void GetNextQuestion(){
        if (question.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();

        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0,question.Count);
        currentQuestion = question[index];
        if (question.Contains(currentQuestion))
        {
            question.Remove(currentQuestion);
        }
    }

    void SetButtonState( bool state){
        for (int i = 0; i < answerButtons.Length; i++){
        Button button = answerButtons[i].GetComponent<Button>();
        button.interactable = state;
        }
    }

    void SetDefaultButtonSprites(){
        for (int i = 0; i < answerButtons.Length; i++){
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

}
