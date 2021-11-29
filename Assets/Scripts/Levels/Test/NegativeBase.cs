using UnityEngine;

public class NegativeBase : MonoBehaviour
{
    [Range(0, 9)]
    public int hundred;
    [Range(0, 9)]
    public int ten;
    [Range(0, 9)]
    public int one;

    public int num;

    private void OnValidate()
    {
        num = hundred * (int)Mathf.Pow(-10, 2) + ten * (int)Mathf.Pow(-10, 1) + one;
    }
}
