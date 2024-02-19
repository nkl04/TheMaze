using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainSO terrainSO;
    public RevivePoint revivePoint;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != terrainSO.player.ToString())
        {
            //if not match with player
            //player die
            //revive player

            revivePoint.RevivePlayer(other.gameObject);
        }
    }
}
