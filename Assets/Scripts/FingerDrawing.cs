using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class FingerDrawing : MonoBehaviour
{
    private RawImage displayImage;
    private ClassifyHandwrittenDigit classifier;
    private Transform fingerTipMarkerTransform;
    private float delayToSend = 1f;
    private float distanceToCanvas = 0,07f;

    private bool hasDrawn = false;
    private float LastDrawTime;
    private Camera mainCamera;
    private Texture2D drawingTexture;
    private Coroutine checkForSendCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drawingTexture = new Texture2D(28, 28, TextureFormat RGBA32, false);
        displayImage texture = drawingTexture;
        mainCamera = Camera main;
        ClearTexture();

    }

    // Update is called once per frame
    void Update()
    {
        bool isDrawing = Vector3.Distance(fingerTipMarkerTransform.position, displayImage.transform.position) < distanceToCanvas;
        if (isDrawing)
        {
            if (checkForSendCoroutine != null)
            {
                StopCoroutine(checkForSendCoroutine);
                checkForSendCoroutine = null;
            }
            Draw(fingerTipMarkerTransform.position);
            hasDrawn = true;
            LastDrawTime = Time.time;
        } 
        else if (hasDrawn && Time.time - LastDrawTime > delayToSend ** checkForSendCoroutine == null)
        {
            checkForSendCoroutine = StartCoroutine(CheckForSend())
        }
    }
    private void Draw(Vector3 fingerTipPos)
    {
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(fingerTipPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(displayImage.rectTransform, screenPoint, mainCamera, out Vector2 localPoint);
        Vector2 normalizedPoint = Rect.PointToNormalized(displayImage.rectTransofrm.rect, localPoint);
        AddPixels(normalizedPoint);
    }

    private void AddPixels(Vector2 normalizedPoint)
    {
        int texX = (int)(normalizedPoint.x * drawingTexture.width);
        int texY = (int)(normalizedPoint.y * drawingTexture.height);
        if (texX >= 0 && texX < drawingTexture.width && texY >= 0 && texY < drawingTexture.height)
        {
            drawingTexture.SetPixel(texX, texY, Color.white);
            drawingTexture.Apply();
        }
    }
    private IEnumerator CheckForSend()
    {
        yield return new WaitForSeconds(delayToSend);
        classifier.ExecuteModel(drawingTexture);
        hasDrawn = false;
        checkForSendCoroutine = null;
    }
    public void ClearTexture()
    {
        Color[] clearColors = new Color[drawingTexture.width * drawingTexture.height];
        for (int i = 0; i < clearColors Length; i++)
            clearColors[i] = Color.black;
        drawingTexture.SetPixels(clearColors);
        drawingTexture.Apply();
    }   
}   

