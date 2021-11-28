using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
public class StyleTransferNetwork : MonoBehaviour
{
    public NNModel modelSource;

    private Model model;
    private IWorker worker;

    // Start is called before the first frame update
    void Start()
    {
        model = ModelLoader.Load(modelSource);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputeRef, model);
    }

    void OnDestroy()
    {
        worker.Dispose();
    }

    public RenderTexture Evaluate(Texture2D contentTexture, Texture2D styleTexture, float alphaValue)
    {
        var content = new Tensor(contentTexture);
        var style = new Tensor(styleTexture);
        var alpha = new Tensor(1, 1, new float[] { alphaValue });

        worker.Execute(new Dictionary<string, Tensor>()
        {
            ["content"] = content,
            ["style"] = style,
            ["alpha"] = alpha
        });

        var output = worker.PeekOutput("output");

        RenderTexture texture = output.ToRenderTexture();

        output.Dispose();

        content.Dispose();
        style.Dispose();
        alpha.Dispose();

        return texture;
    }
}
