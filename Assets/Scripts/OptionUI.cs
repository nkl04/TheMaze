using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance {get;private set;}
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private Button player1_jumpButton;
    [SerializeField] private Button player1_moveDownButton;
    [SerializeField] private Button player1_moveLeftButton;
    [SerializeField] private Button player1_moveRightButton;
    [SerializeField] private Button player2_jumpButton;
    [SerializeField] private Button player2_moveDownButton;
    [SerializeField] private Button player2_moveLeftButton;
    [SerializeField] private Button player2_moveRightButton;
    [SerializeField] private TextMeshProUGUI player1_jumpText;
    [SerializeField] private TextMeshProUGUI player1_moveDownText;
    [SerializeField] private TextMeshProUGUI player1_moveLeftText;
    [SerializeField] private TextMeshProUGUI player1_moveRightText;
    [SerializeField] private TextMeshProUGUI player2_jumpText;
    [SerializeField] private TextMeshProUGUI player2_moveDownText;
    [SerializeField] private TextMeshProUGUI player2_moveLeftText;
    [SerializeField] private TextMeshProUGUI player2_moveRightText;
    [SerializeField] private Transform pressKeyToRebindTransform;

    private void Awake() {
        Instance = this;
        UpdateTextVisual();
        HidePressKeyToRebindTransform();

        //===========Audio==========
        soundEffectsButton.onClick.AddListener(() =>
        {
            Debug.Log("SoundEff btn");
            AudioManager.Instance.ChangeVolumeSFX();
            UpdateVisual();

        });
        musicButton.onClick.AddListener(() => {
            Debug.Log("Music btn");
            AudioManager.Instance.ChangeVolumeMusic();
            UpdateVisual();
        });
        //========================================

        player1_jumpButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player1_Jump);});
        player1_moveDownButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player1_MoveDown);});
        player1_moveLeftButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player1_MoveLeft);});
        player1_moveRightButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player1_MoveRight);});
        player2_jumpButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player2_Jump);});
        player2_moveDownButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player2_MoveDown);});
        player2_moveLeftButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player2_MoveLeft);});
        player2_moveRightButton.onClick.AddListener(() => {RebindBinding(GameInput.Binding.Player2_MoveRight);});
    }

    private void UpdateTextVisual()
    {
        player1_jumpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player1_Jump);
        player1_moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player1_MoveDown);
        player1_moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player1_MoveLeft);
        player1_moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player1_MoveRight);
        player2_jumpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player2_Jump);
        player2_moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player2_MoveDown);
        player2_moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player2_MoveLeft);
        player2_moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Player2_MoveRight);
    }

    private void ShowPressKeyToRebindTransform()
    {
        pressKeyToRebindTransform.gameObject.SetActive(true);
    }

    private void HidePressKeyToRebindTransform()
    {
        pressKeyToRebindTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressKeyToRebindTransform();
        GameInput.Instance.RebindBinding(binding,() => {
            HidePressKeyToRebindTransform();
            UpdateTextVisual();
        });
    }
    private void UpdateVisual()
    {
        soundEffectsText.text ="Sound Effects: " + Mathf.Round( AudioManager.Instance.GetvolumeSFX() *10f);
        musicText.text ="Music: " + Mathf.Round( AudioManager.Instance.GetvolumeMusic() *10f);
        Debug.Log("UpdateVisual");


    }
}
