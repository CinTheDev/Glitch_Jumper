using System.Collections;
using TMPro;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    [Header("Please don't spam the trigger. This causes bugs.")]
    public float cursorTime;

    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        StartCoroutine(Cursor());
    }

    private void Update()
    {
        text.rectTransform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1);
        text.rectTransform.sizeDelta = transform.localScale;
    }

    public IEnumerator Type(string text, float timeBetweenSteps)
    {
        StartCoroutine(Type(text.ToCharArray(), timeBetweenSteps));
        yield return null;
    }

    private bool textLock = false;
    public IEnumerator Type(char[] letters, float timeBetweenSteps)
    {
        // If another thread is writing to the text
        while (textLock) yield return new WaitForEndOfFrame();

        // Mark the text as locked
        textLock = true;

        text.text = "> ";
        for (int i = 0; i < letters.Length; i++)
        {
            // Add letters piece by piece
            text.text += letters[i];
            yield return new WaitForSeconds(timeBetweenSteps);
        }
        

        // Free the text from locked state
        textLock = false;
    }
    private IEnumerator Cursor()
    {
        bool a = true;
        while (true)
        {
            // Wait for animation time
            yield return new WaitForSeconds(cursorTime);

            while (textLock)
            {
                // If the text is still writing
                a = true;
                yield return new WaitForEndOfFrame();
            }

            if (a)
            {
                // Add the cursor
                text.text += "_";
                a = false;
            }
            else
            {
                // Remove the last two chars (the cursor)
                text.text = text.text.Remove(text.text.Length - 1);
                a = true;
            }
        }
    }
}