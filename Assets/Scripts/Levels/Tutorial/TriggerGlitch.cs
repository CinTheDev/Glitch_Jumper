using UnityEngine;

public class TriggerGlitch : MonoBehaviour
{
    public GlitchTexture glTex;

    public enum Mode
    {
        OnTouch,
        Constant,
    }
    public Mode mode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mode == Mode.Constant)
            glTex.Glitch(Mathf.RoundToInt(GetComponent<BoxCollider2D>().size.x), Mathf.RoundToInt(GetComponent<BoxCollider2D>().size.y));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (mode == Mode.OnTouch)
            glTex.Glitch(Mathf.RoundToInt(GetComponent<BoxCollider2D>().size.x), Mathf.RoundToInt(GetComponent<BoxCollider2D>().size.y));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (mode == Mode.OnTouch) glTex.GetSpriteRenderer().sprite = null;
    }
}
