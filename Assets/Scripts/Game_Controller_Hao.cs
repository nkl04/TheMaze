using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller_Hao : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowGameOverCanvas();
        }
    }
    public void PlayAgain()
    {
        // Reload lại scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    private void ShowGameOverCanvas()
    {
        // Hiển thị canvas khi player xảy ra va chạm với object
        canvas.SetActive(true);
    }
}
