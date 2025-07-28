using UnityEngine;
using System.IO;

//¬ дальнейшем можно сделать, чтобы была кнопка в инспекторе или как Tool
//+ можно сделать чтобы сохран€л сразу спрайтом
public class TransparentSceenshot : MonoBehaviour
{
    public Camera screenshotCamera;
    public RenderTexture renderTexture;

    [ContextMenu("Take Screenshot")]
    public void TakeScreenshot()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        screenshotCamera.Render();

        Texture2D image = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        image.Apply();

        byte[] bytes = image.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Stone.png", bytes);

        RenderTexture.active = currentRT;

        Debug.Log("Saved screenshot with alpha at " + Application.dataPath + "/Stone.png");
    }
}
