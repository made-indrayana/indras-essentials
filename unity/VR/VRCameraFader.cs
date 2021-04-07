/*
        ▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷
        ◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁
                                                                                           
             ((((                                                                          
            ((((     _______       _______.     ___                                        
             ))))   |       \     /       |    /   \                                       
          _ .---.   |  .--.  |   |   (----`   /  ^  \                                      
         ( |`---'|  |  |  |  |    \   \      /  /_\  \    _                                
          \|     |  |  '--'  |.----)   |    /  _____  \  /   _   _|  _        _  ._ |   _  
          : .___, : |_______/ |_______/    /__/     \__\ \_ (_) (_| (/_ \/\/ (_) |  |< _>  
           `-----'                                                                         
                                                                                           
        ▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷▷
        ◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁◁
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DoubleShot.Utils;

public class VRCameraFader : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private Image fadeImage;
    public float fadeTime = 2f;
    public float pauseBetweenFade = 1f;
    private bool isFading = false;
    public bool IsFading { get { return isFading; } }
    public bool startWithFadeIn = false;
    [Header("Runtime Debug")]
    public bool debugMode = false;

    private void Awake()
    {
        canvas.SetActive(false);
    }

    private void Start()
    {
        if (startWithFadeIn)
        {
            ScreenFadeIn();
        }
    }

    private IEnumerator StartFade(Direction direction)
    {
        float elapsedTime = 0f;
        var tempColor = fadeImage.color;
        float startAlpha = 0f, targetAlpha = 0f;

        switch (direction)
        {
            case Direction.In:
                startAlpha = 1f;
                targetAlpha = 0f;
                break;
            case Direction.Out:
                startAlpha = 0f;
                targetAlpha = 1f;
                break;
        }

        isFading = true;
        canvas.SetActive(true);

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Lerp(startAlpha, targetAlpha, Mathf.Clamp01(elapsedTime / fadeTime));
            fadeImage.color = tempColor;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(pauseBetweenFade);

        isFading = false;

        if (debugMode)
            canvas.SetActive(false);

    }

    private IEnumerator WaitAndStartFade(Direction direction)
    {
        while (isFading) { yield return new WaitForEndOfFrame(); }
        StartCoroutine(StartFade(direction));
    }

    public void ScreenFadeOut()
    {
        if (isFading)
        {
            StartCoroutine(WaitAndStartFade(Direction.Out));
        }
        else { StartCoroutine(StartFade(Direction.Out)); }
    }
    public void ScreenFadeIn()
    {
        if (isFading)
        {
            StartCoroutine(WaitAndStartFade(Direction.In));
        }
        else { StartCoroutine(StartFade(Direction.In)); }
    }

}