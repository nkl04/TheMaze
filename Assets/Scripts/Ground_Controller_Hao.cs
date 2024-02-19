using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Controller_Hao : MonoBehaviour
{
    [SerializeField] private GameObject groundMove;
    [SerializeField] private float targetXPosition;
    private float firstPosition;
    // Start is called before the first frame update
    void Start()
    {
        firstPosition = groundMove.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu player tạo trigger với nút bấm
        if (collision.CompareTag("Player"))
        {
            // Di chuyển game object đến vị trí mới
            Vector3 newPosition = new Vector3(targetXPosition, groundMove.transform.position.y, groundMove.transform.position.z);
            groundMove.transform.position = newPosition;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Di chuyển game object đến vị trí mới
            Vector3 newPosition = new Vector3(firstPosition, groundMove.transform.position.y, groundMove.transform.position.z);
            groundMove.transform.position = newPosition;
        }
    }
}
