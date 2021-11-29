using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StyleTransferManager : MonoBehaviour
{
    public static StyleTransferManager _instance;
    public static StyleTransferManager instance
    {
        get
        {
            return _instance;
        }
    }

    public Texture2D[] contentTextures;
    public Texture2D[] styleTextures;

    public StyleTransferNetwork network;

    public StyleTransferUI[] uis;

    public struct Evaluation
    {
        public Texture2D contentTexture;
        public Texture2D styleTexture;
        public float alphaValue;
        
        public Texture2D resultTexture;
    }

    private Evaluation[] evaluations;

    public Evaluation GetEvaluation(int index)
    {
        return evaluations[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (StyleTransferUI ui in uis)
        {
            ui.SetTextures(StyleTransferUI.TextureType.Content, contentTextures);
            ui.SetTextures(StyleTransferUI.TextureType.Style, styleTextures);
        }

        evaluations = new Evaluation[uis.Length];

        for (int index = 0; index < uis.Length; index++)
        {
            evaluations[index] = new Evaluation();
        }
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

    public Texture2D Evaluate(int index, Texture2D contentTexture, Texture2D styleTexture, float alphaValue)
    {
        Texture2D resultTexture = ToTexture2D(network.Evaluate(contentTexture, styleTexture, alphaValue));
        
        evaluations[index].contentTexture = contentTexture;
        evaluations[index].styleTexture = styleTexture;
        evaluations[index].alphaValue = alphaValue;
        evaluations[index].resultTexture = resultTexture;

        return resultTexture;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
