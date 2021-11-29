using UnityEngine;

public class EasterEgg_Commands : MonoBehaviour
{
    public enum Function
    {
        SetBackground,
        DEV_RESET,
        Spawn,
        Kill,
        Speed,
    }

    public Function function;

    public GameObject MP;

    public GameObject enemy;
    public Vector2 enemySpawn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (function)
        {
            case Function.SetBackground:
                // Random background color
                float r, b, g;
                r = Random.Range(0f, 1f);
                b = Random.Range(0f, 1f);
                g = Random.Range(0f, 1f);

                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor = new Color(r, g, b);
                break;

            case Function.DEV_RESET:
                // Make MP solid
                MP.GetComponent<BoxCollider2D>().enabled = true;
                break;

            case Function.Spawn:
                // Spawn enemy
                GameObject o = Instantiate(enemy, enemySpawn, new Quaternion());
                o.SetActive(true);
                break;

            case Function.Kill:
                // Kill player
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Die(Entity.DieCause.Player);
                break;

            case Function.Speed:
                // Set MP speed to a very high value
                MP.GetComponent<MovingPlatform>().timeScale = 10f;
                break;
        }
    }
}
