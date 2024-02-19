using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X_button : MonoBehaviour
{
    [SerializeField] private GameObject groundDestroy;
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
        // Kiểm tra nếu player tạo trigger với nút bấm
        if (collision.CompareTag("Player"))
        {
            Destroy(groundDestroy);
        }
    }
}
