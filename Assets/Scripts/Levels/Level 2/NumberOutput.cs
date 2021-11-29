using UnityEngine;
using TMPro;

public class NumberOutput : MonoBehaviour
{
    // Index 0 is base^0, index 1 base^1, ...
    public NumberInput[] inputs;

    [Header("X is Base, Y is required number.")]
    public Vector2Int[] values;

    public TerminalTrigger terminal;
    public ActivationClass door;

    private bool active = true;
    private int iteration = 0;
    private int numBase;
    private int req;
    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        SetIteration();
    }

    private void Start()
    {
        terminal.Trigger();
        GetNumber();
    }

    private int GetNumber()
    {
        int result = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            result += inputs[i].number * (int)Mathf.Pow(numBase, i);
        }
        return result;
    }

    public void UpdateNumber()
    {
        if (!active) return;

        int cur = GetNumber();
        Check(cur);

        text.text = "Required: " + req.ToString("00000") + "\n";
        text.text += "Current: " + cur.ToString("00000");
    }

    private void Check(int cur)
    {
        if (cur == req)
        {
            terminal.Trigger();
            iteration++;
            if (iteration >= values.Length)
            {
                active = false;
                door.Activate();
            }
            else
            {
                SetIteration();
            }
        }
    }

    private void SetIteration()
    {
        numBase = values[iteration].x;
        req = values[iteration].y;
        foreach (NumberInput inp in inputs)
        {
            inp.numBase = values[iteration].x;
        }
    }
}
