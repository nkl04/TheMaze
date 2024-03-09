using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovements : MonoBehaviour
{
    public PlayerController rb2D;
    public bool isSawOn = false;
    private const string TURN_ON_SAW = ("isSawOn");
    [SerializeField] private Animator animator;
    
    void Start()
    {
        rb2D = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       animator.SetBool(TURN_ON_SAW, isSawOn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isSawOn = true; 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isSawOn = false;
    }

}
