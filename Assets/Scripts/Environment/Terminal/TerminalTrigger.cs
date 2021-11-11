using System.Collections;
using UnityEngine;

public class TerminalTrigger : MonoBehaviour
{
    public Terminal terminal;
    public enum Mode
    {
        Type,
        TypeOnce,
        TypeMultiple,
    }
    public Mode mode;

    [HideInInspector]
    [TextArea(0, 10)]
    public string[] textArray;
    public Queue textQueue;
    [HideInInspector]
    [TextArea(0, 10)]
    public string text;
    public float time;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        textQueue = new Queue(textArray);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        switch (mode)
        {
            case Mode.Type:
                Type();
                break;

            case Mode.TypeOnce:
                TypeOnce();
                break;

            case Mode.TypeMultiple:
                TypeMultiple();
                break;
        }
    }

    private void Type()
    {
        StartCoroutine(terminal.Type(text, time));
    }

    private bool typed = false;
    private void TypeOnce()
    {
        if (!typed)
        {
            StartCoroutine(terminal.Type(text, time));
            typed = true;
        }
    }

    private void TypeMultiple()
    {
        if (textQueue.Count > 0)
            StartCoroutine(terminal.Type((string)textQueue.Dequeue(), time));
    }
}
