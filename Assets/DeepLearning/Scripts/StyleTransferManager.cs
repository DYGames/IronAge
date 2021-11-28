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

    public List<Texture2D> generatedTextures;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        generatedTextures = new List<Texture2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGeneratedTexture(int index, Texture2D generatedTexture)
    {
        while (generatedTextures.Count <= index)
        {
            generatedTextures.Add(null);
        }

        generatedTextures[index] = generatedTexture;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
