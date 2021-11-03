using UnityEngine;

public class Door : ActivationClass
{
    public Color debugActiveColor;
    public Color debugUnactiveColor;
    protected override void Act()
    {
        Debug.Log("Door opened");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = debugUnactiveColor;
    }

    protected override void Deact()
    {
        Debug.Log("Door closed");
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = debugActiveColor;
    }
}
