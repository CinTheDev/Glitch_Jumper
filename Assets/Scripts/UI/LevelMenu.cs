using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public int level;
    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }
    public void Level1()
    {
        SceneManager.LoadScene(3);
    }
    public void Level2()
    {
        SceneManager.LoadScene(4);
    }
    public void Level3()
    {
        SceneManager.LoadScene(5);
    }
}
