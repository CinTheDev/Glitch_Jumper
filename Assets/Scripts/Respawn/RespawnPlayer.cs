using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnPlayer : MonoBehaviour
{
    public Vector2 respawnpoint;
    public int indexrespawnpoint;
    public static Vector2 r =new Vector2(-14f, -1.5f);
    public static int i = 0;
    private GameObject player;
    private PlayerMovement playermovement;
    // Start is called before the first frame update
    private void Awake()
    {
        respawnpoint = r;
        indexrespawnpoint = i;
    }
    void Start()
    {
        player = GameObject.Find("Player");
        playermovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialCamera>().index = i;
    }

    // Update is called once per frame
    void Update()
    {
        if (playermovement.respawn == true)
        {
            r = respawnpoint;
            i = indexrespawnpoint -1 ;
            StartCoroutine(Reload());
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            r = respawnpoint;
            i = indexrespawnpoint - 1;
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    public void Reset()
    {
        r = new Vector2(90f, -1.5f);
        i = 0;
    }
}
    
