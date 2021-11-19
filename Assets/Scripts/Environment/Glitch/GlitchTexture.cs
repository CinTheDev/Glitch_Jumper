using UnityEngine;

public class GlitchTexture : MonoBehaviour
{
    public ComputeShader computeShader;
    [Range(1, 128)]
    public int resolution;

    private Sprite sprite;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Glitch(int width, int height)
    {
        width *= resolution;
        height *= resolution;
        // Shader
        RenderTexture rTex = new RenderTexture(width, height, 32);
        rTex.enableRandomWrite = true;
        rTex.Create();

        computeShader.SetTexture(0, "Result", rTex);
        computeShader.SetInt("Seed", Random.Range(0, 256));
        computeShader.SetInt("ResolutionY", height);
        computeShader.SetInt("ResolutionX", width);
        computeShader.Dispatch(0, rTex.width, rTex.height, 1);

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        // Render Texture apply
        RenderTexture.active = rTex;
        texture.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        texture.Apply();

        sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), resolution);
        sr.sprite = sprite;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return sr;
    }
}
