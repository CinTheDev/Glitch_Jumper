using System.Collections;
using System.Linq;
using UnityEngine;

public class TerminalTrigger : MonoBehaviour
{
    public Terminal terminal;
    public GameObject[] triggerObjects;
    public enum Mode
    {
        Type,
        TypeOnce,
        TypeMultiple,
    }
    public Mode mode;
    public bool clearConsole;

    [HideInInspector]
    [TextArea(0, 10)]
    public string[] textArray;
    public Queue textQueue;
    [HideInInspector]
    [TextArea(0, 10)]
    public string text;
    public float time;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        textQueue = new Queue(textArray);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggerObjects.Contains(collision.gameObject)) return;

        Trigger();
    }

    public void Trigger()
    {
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
        StartCoroutine(terminal.Type(text, time, clearConsole));
    }

    private bool typed = false;
    private void TypeOnce()
    {
        if (!typed)
        {
            StartCoroutine(terminal.Type(text, time, clearConsole));
            typed = true;
        }
    }

    private void TypeMultiple()
    {
        if (textQueue.Count > 0)
        {
            string text = (string)textQueue.Dequeue();
            bool c = false;
            if (text.Contains("\\c"))
            {
                c = true;
                text = text.Remove(0, 2);
            }
            StartCoroutine(terminal.Type(text, time, c || clearConsole));
        }
    }
}
