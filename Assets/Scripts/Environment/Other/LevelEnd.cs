using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Destroy(GameObject.FindGameObjectWithTag("Respawn"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
