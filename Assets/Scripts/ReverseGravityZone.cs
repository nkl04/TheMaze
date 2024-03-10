using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityZone : MonoBehaviour
{

    [SerializeField] private float reverseGravityTime = 3f;
    private bool canReverseGravity = false;
    private List<GameObject> gameObjectList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("BlockPlatform") || other.CompareTag("OneWayPlatform"))
        {
            gameObjectList.Add(other.gameObject);
        }
    }

    private void LateUpdate() {
        if (gameObjectList!=null && !canReverseGravity)
        {
            StartCoroutine(GravityReverse(gameObjectList));
        }
    }

    IEnumerator GravityReverse(List<GameObject> gameObjectList)
    {
        if (!canReverseGravity)
        {
            yield return new WaitForSeconds(reverseGravityTime);
            canReverseGravity = true;

            ReverseGravity(gameObjectList);

            yield return new WaitForSeconds(reverseGravityTime);
            canReverseGravity = false;

            ResetGravity(gameObjectList);
        }
    }

    private void ReverseGravity(List<GameObject> gameObjectList)
    {
        foreach (GameObject item in gameObjectList)
        {
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float gravity = rb.gravityScale;
                rb.gravityScale = -gravity;
                HorizontalFlip(item.transform);
            }
        }
        
    }

    private void ResetGravity(List<GameObject> gameObjectList)
    {
        foreach (GameObject item in gameObjectList)
        {
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float gravity = rb.gravityScale;
                rb.gravityScale = Mathf.Abs(gravity);
                item.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        
    }

    public void HorizontalFlip(Transform transform)
    {
        transform.Rotate(180f, 0f, 0f);
    }
}
