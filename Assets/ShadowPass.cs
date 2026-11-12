using UnityEngine;
using UnityEngine.UI;

public class LineShadow : MonoBehaviour
{
    public Camera lineCam;
    public Camera overCam;
    public RawImage shadowImage;
    public RawImage lineImage;        // canvas units
    public RawImage overlayImage;
    public int xO,yO;
    public Color shadowColor = new Color(0, 0, 0, 0.5f); // RGB multiplies the line colors, so use black or near-black
    RenderTexture ot;
    RenderTexture rt;
    int w, h;

    void Start()
    {

        Build();
    }
    

    void Update()
    {
        if (Screen.width != w || Screen.height != h) Build();   // handle window resize
    }

    void Build()
    {
        Vector2 offset = new Vector2(xO, yO); 
        if (rt != null)
        {
            lineCam.targetTexture = null;
            rt.Release();
            Destroy(rt);
        }
        if (ot != null)
        {
            overCam.targetTexture = null;
            ot.Release();
            Destroy(ot);
        }

        w = Screen.width;
        h = Screen.height;

        rt = new RenderTexture(w, h, 24, RenderTextureFormat.ARGB32); //gpu image reference

        lineCam.targetTexture = rt;

        lineImage.texture = rt;
        shadowImage.texture = rt;
        shadowImage.color = shadowColor;
        shadowImage.rectTransform.anchoredPosition = offset;



        ot = new RenderTexture(w, h, 24, RenderTextureFormat.ARGB32); //gpu image reference

        overCam.targetTexture = ot;

        overlayImage.texture = ot;

    }

    void OnDestroy()
    {
        if (rt != null) rt.Release();
    }
}