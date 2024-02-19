using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RevivePoint : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    
    public void RevivePlayer(GameObject player)
    {
        player.transform.position = transform.position;
    }
}
