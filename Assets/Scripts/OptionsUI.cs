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
    
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    

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