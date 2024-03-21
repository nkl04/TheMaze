using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ReverseGravityZone : MonoBehaviour
{
    public static ReverseGravityZone Instance {private set; get;}
    public event EventHandler OnReverseGravity;
    [SerializeField] private float reverseGravityTime = 3f;
    [SerializeField] private Slider slider;
    private bool canReverseGravity = false;
    private float timeCounter;
    private List<GameObject> gameObjectList = new List<GameObject>();

    private void Awake() {
        Instance = this;
        timeCounter = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("BlockPlatform") || other.CompareTag("OneWayPlatform"))
        {
            gameObjectList.Add(other.gameObject);
            if (canReverseGravity)
            {
                ReverseGravityOfGameObject(other.gameObject);
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("BlockPlatform") || other.CompareTag("OneWayPlatform"))
        {
            ResetGravityOfGameObject(other.gameObject);
            gameObjectList.Remove(other.gameObject);
        }
    }

    private void LateUpdate() {
        if (gameObjectList!=null)
        {
            if (!canReverseGravity)
            {
                if (timeCounter < reverseGravityTime)
                {
                    slider.value = 1 - timeCounter/reverseGravityTime;
                    timeCounter += Time.deltaTime;
                }
                if (timeCounter >= reverseGravityTime)
                {
                    ReverseGravity(gameObjectList);
                    timeCounter = 0f;
                    canReverseGravity = !canReverseGravity;
                }
            }
            else
            {
                if (timeCounter < reverseGravityTime)
                {
                    slider.value = 1 - timeCounter/reverseGravityTime;
                    timeCounter += Time.deltaTime;
                }
                if (timeCounter >= reverseGravityTime)
                {
                    ResetGravity(gameObjectList);
                    timeCounter = 0f;
                    canReverseGravity = !canReverseGravity;
                }
            }
            
        }

    }

    private void ReverseGravity(List<GameObject> gameObjectList)
    {
        foreach (GameObject item in gameObjectList)
        {
            ReverseGravityOfGameObject(item);
        }
    }

    private void ResetGravity(List<GameObject> gameObjectList)
    {
        foreach (GameObject item in gameObjectList)
        {
            ResetGravityOfGameObject(item);
        }     
    }

    private void ReverseGravityOfGameObject(GameObject gameObject)
    {
        bool isReversed = false;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && !isReversed)
        {
            OnReverseGravity?.Invoke(this,EventArgs.Empty);
            float gravity = rb.gravityScale;
            rb.gravityScale = -Mathf.Abs(gravity);
            gameObject.transform.rotation = Quaternion.Euler(180f, 0f, 0f); 
            isReversed = true;
        }
    }

    private void ResetGravityOfGameObject(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            OnReverseGravity?.Invoke(this,EventArgs.Empty);
            float gravity = rb.gravityScale;
            rb.gravityScale = Mathf.Abs(gravity);
            gameObject.transform.rotation = Quaternion.Euler(180f, 0f, 0f);     
        }
        
    }

    public void HorizontalFlip(Transform transform)
    {
        transform.Rotate(180f, 0f, 0f);
    }
}
