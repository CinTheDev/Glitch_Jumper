using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnSystem : MonoBehaviour
{
    public int indexrespawnpoint = 1;
    public static RespawnSystem instance;
    public GameObject respawnsystem;
    private GameObject player;
    private PlayerMovement playermovement;
    private TutorialCamera cam;
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector2(-9, -8.5f);
        player = GameObject.FindGameObjectWithTag("Player");
        playermovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TutorialCamera>();
        cam.index = indexrespawnpoint;
        cam.SetPosition();
    }

    public void Reset()
    {
        transform.position = new Vector2(-9, -8.5f);
        indexrespawnpoint = 0;
    }
}
    
