using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveDownButton1;
    [SerializeField] private Button moveLeftButton1;
    [SerializeField] private Button moveRightButton1;
    [SerializeField] private Button jumpButton1;
    [SerializeField] private Button moveDownButton2;
    [SerializeField] private Button moveLeftButton2;
    [SerializeField] private Button moveRightButton2;
    [SerializeField] private Button jumpButton2;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveDownText1;
    [SerializeField] private TextMeshProUGUI moveLeftText1;
    [SerializeField] private TextMeshProUGUI moveRightText1;
    [SerializeField] private TextMeshProUGUI jumpText1;
    [SerializeField] private TextMeshProUGUI moveDownText2;
    [SerializeField] private TextMeshProUGUI moveLeftText2;
    [SerializeField] private TextMeshProUGUI moveRightText2;
    [SerializeField] private TextMeshProUGUI jumpText2;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI restartText;

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => 
        {
            // SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            // MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            Hide();
        });
    }
    private void Start()
    {
        UpdateVisual() ;

        Hide();
    }
    private void UpdateVisual()
    {
        // soundEffectsText.text ="Sound Effects: " + Mathf.Round( SoundManager.Instance.Getvolume() *10f);
        // soundEffectsText.text ="Music: " + Mathf.Round( MusicManager.Instance.Getvolume() *10f);


    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    { gameObject.SetActive(false); }
}
