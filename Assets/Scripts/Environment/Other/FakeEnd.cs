using UnityEngine;

public class FakeEnd : ActivationClass
{
    public GameObject tutorialend;
    public GameObject box;
    //public Transform box;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != box) return;
        tutorialend.SetActive(true);
        gameObject.SetActive(false);
    }
}
