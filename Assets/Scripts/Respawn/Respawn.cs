using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private GameObject player;
    private RespawnPlayer respawnplayer;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        respawnplayer = player.GetComponent<RespawnPlayer>();
        //respawnplayer.Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;
        player.GetComponent<RespawnPlayer>().respawnpoint = transform.position;
        player.GetComponent<RespawnPlayer>().indexrespawnpoint = index;
    }
}
