using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnSystem : MonoBehaviour
{
    public int indexrespawnpoint = 1;
    public GameObject prefab;
    public static RespawnSystem instance;
    public GameObject respawnsystem;
    public Vector2 reset = new Vector2(-9, -8.5f);
    public bool test = false;
    private GameObject player;
    private PositionCamera cam;
    // Start is called before the first frame update
    void Awake()
    {
        if (!test)
        {
            indexrespawnpoint = 0;
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    Destroy(gameObject);
                    break;
                case 2:
                    reset = new Vector2(-9, -8.5f);
                    break;
                case 3:
                    reset = new Vector2(-7, -2.5f);
                    break;
                case 4:
                    reset = new Vector2(-5.5f, -3.5f);
                    break;
                case 5:
                    reset = new Vector2(-10f, -2.5f);
                    break;
            }
        }
        transform.position = reset;
        player = GameObject.FindGameObjectWithTag("Player");
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

    void Update()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                Destroy(gameObject);
                break;
            case 2:
                reset = new Vector2(-9, -8.5f);
                break;
            case 3:
                reset = new Vector2(-7, -2.5f);
                break;
        }
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
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PositionCamera>();
        cam.index = indexrespawnpoint;
        cam.SetPosition();
    }

}
    
