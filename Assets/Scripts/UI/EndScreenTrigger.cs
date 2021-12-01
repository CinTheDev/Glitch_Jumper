using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenTrigger : MonoBehaviour
{
    public GameObject Endscreen;
    public void OnTriggerEnter2D(Collider2D collision)
    {
            Endscreen.SetActive(true);
            Time.timeScale = 0f;
    }
}
