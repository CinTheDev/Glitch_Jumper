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

    private bool triggerStay = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mode == Mode.Constant)
            glTex.Glitch(Mathf.RoundToInt(GetComponent<BoxCollider2D>().size.x), Mathf.RoundToInt(GetComponent<BoxCollider2D>().size.y));

        triggerStay = true;
        FindObjectOfType<AudioManager>().Play("Glitch");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (mode == Mode.OnTouch) glTex.GetSpriteRenderer().sprite = null;

        triggerStay = false;
    }

    private void Update()
    {
        if (mode == Mode.OnTouch && triggerStay)
            glTex.Glitch(Mathf.RoundToInt(GetComponent<BoxCollider2D>().size.x), Mathf.RoundToInt(GetComponent<BoxCollider2D>().size.y));
    }
}
