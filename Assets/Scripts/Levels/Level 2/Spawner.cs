using UnityEngine;
using System.Collections;

public class Spawner : ActivationClass
{
    public GameObject[] spawns;
    public Vector2 offset;
    public float time;

    private int spawnIndex = 0;
    private int spawnNumber = 0;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    protected override void Act()
    {
        spawnNumber++;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (spawnNumber > 0)
            {
                Vector2 pos = transform.position;

                spawns[spawnIndex].transform.position = pos + offset;
                spawns[spawnIndex].transform.rotation = transform.rotation;
                spawns[spawnIndex].SetActive(true);

                spawnIndex = (spawnIndex + 1) % spawns.Length;

                spawnNumber--;
            }

            yield return new WaitForSeconds(time);
        }
        
    }
}
