using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleTransferUI : MonoBehaviour
{
    public Texture2D[] contentTextures;
    public Texture2D[] styleTextures;

    public int uiIndex = 0;
    public UnityEngine.Events.UnityEvent<int, Texture2D> onEvaluate;

    public StyleTransferNetwork network;

    public Dropdown contentDropdown;
    public Dropdown styleDropdown;
    public Slider alphaSlider;

    public RawImage resultImage;

    public float cooldownSeconds = 1f;
    private float nextCooldown = 0f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (!network)
        {
            network = FindObjectOfType<StyleTransferNetwork>();
        }

        yield return new WaitForEndOfFrame();

        UpdateDropdown(contentDropdown, contentTextures);
        UpdateDropdown(styleDropdown, styleTextures);

        contentDropdown.value = Random.Range(0, contentDropdown.options.Count);
        styleDropdown.value = Random.Range(0, styleDropdown.options.Count);
        alphaSlider.value = Random.value;

        Evaluate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public void Evaluate()
    {
        if (Time.time < nextCooldown)
        {
            return;
        }

        int contentIndex = contentDropdown.value;
        int styleIndex = styleDropdown.value;

        Texture2D contentTexture = contentTextures[contentIndex];
        Texture2D styleTexture = styleTextures[styleIndex];

        float alphaValue = alphaSlider.value;

        Texture2D resultTexture = ToTexture2D(network.Evaluate(contentTexture, styleTexture, alphaValue));
        resultImage.texture = resultTexture;

        onEvaluate.Invoke(uiIndex, resultTexture);

        nextCooldown = Time.time + cooldownSeconds;
    }

    void UpdateDropdown(Dropdown dropdown, Texture2D[] textures)
    {
        dropdown.options.Clear();

        for (int index = 0; index < textures.Length; index++)
        {
            Texture2D texture = textures[index];
            Sprite optionSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.one * 0.5f);
            dropdown.options.Add(new Dropdown.OptionData(index.ToString(), optionSprite));
        }
    }
}
