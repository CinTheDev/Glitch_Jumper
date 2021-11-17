using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private GameObject player;
    public GameObject respawn;
    public int index;

    void Start()
    {
        player = GameObject.Find("Player");
        respawn = GameObject.Find("RespawnSystem");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;
        respawn.transform.position = transform.position;
        respawn.GetComponent<RespawnSystem>().indexrespawnpoint = index;
    }
}
