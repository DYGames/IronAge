using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleTransferUI : MonoBehaviour
{
    private Texture2D[] contentTextures;
    private Texture2D[] styleTextures;

    public int uiIndex = 0;
    // public UnityEngine.Events.UnityEvent<int, Texture2D> onEvaluate;

    public Dropdown contentDropdown;
    public Dropdown styleDropdown;
    public Slider alphaSlider;

    public RawImage resultImage;

    public float cooldownSeconds = 1f;
    private float nextCooldown = 0f;
    private bool shouldEvaluate = false;

    public enum TextureType
    {
        Content, Style
    }

    public void SetTextures(TextureType type, Texture2D[] textures)
    {
        switch (type)
        {
            case TextureType.Content:
                contentTextures = textures;
                UpdateDropdown(contentDropdown, textures);
                break;

            case TextureType.Style:
                styleTextures = textures;
                UpdateDropdown(styleDropdown, textures);
                break;
        }
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
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
        if (shouldEvaluate)
        {
            Evaluate();
        }
    }

    public void Evaluate()
    {
        if (Time.time < nextCooldown)
        {
            shouldEvaluate = true;
            return;
        }

        shouldEvaluate = false;

        int contentIndex = contentDropdown.value;
        int styleIndex = styleDropdown.value;

        Texture2D contentTexture = contentTextures[contentIndex];
        Texture2D styleTexture = styleTextures[styleIndex];

        float alphaValue = alphaSlider.value;

        Texture2D resultTexture = StyleTransferManager.instance.Evaluate(uiIndex, contentTexture, styleTexture, alphaValue);
        resultImage.texture = resultTexture;

        // onEvaluate.Invoke(uiIndex, resultTexture);

        nextCooldown = Time.time + cooldownSeconds;
    }

    void UpdateDropdown(Dropdown dropdown, Texture2D[] textures)
    {
        dropdown.ClearOptions();

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        for (int index = 0; index < textures.Length; index++)
        {
            Texture2D texture = textures[index];
            Sprite optionSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.one * 0.5f);
            options.Add(new Dropdown.OptionData(index.ToString(), optionSprite));
        }

        dropdown.AddOptions(options);
    }
}
