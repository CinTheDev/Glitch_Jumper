using UnityEngine;
using TMPro;


public class NumberInput : MonoBehaviour
{
    public NumberOutput output;
    public int numBase;
    public int number;

    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        number = (number + 1) % numBase;
        text.text = number.ToString();
        output.UpdateNumber();
    }
}
