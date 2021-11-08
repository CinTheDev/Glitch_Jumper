using UnityEngine;

public class TriggerAbility : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        GetComponent<DeactivatePlayerAbility>().DeactAbility();
    }
}
