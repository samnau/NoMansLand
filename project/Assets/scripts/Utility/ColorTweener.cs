using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ColorTweener : BaseTweener
{
    SpriteRenderer spriteRenderer;
    Image image;
    float endAlpha = 1f;
    float endRed = 1f;
    float endGreen = 1f;
    float endBlue = 1f;
  
    void Start()
    {
        InitImageComponents();
    }
    void InitImageComponents()
    {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRendererFound))
        {
            spriteRenderer = spriteRendererFound;
        }
        if (TryGetComponent<Image>(out Image imageFound))
        {
            image = imageFound;
        }
    }

    IEnumerator SetSpriteColor()
    {
        Color targetColor = new Color(endRed, endGreen, endBlue, endAlpha);
        Color startColor = spriteRenderer.color;
        while (progress < 1)
        {
            spriteRenderer.color = Color.Lerp(startColor, targetColor, progress);
            progress += (Time.deltaTime * speed);
            if (progress >= 1)
            {
                progress = 1f;
                spriteRenderer.color = targetColor;
            }
            yield return null;
        }
    }
    IEnumerator SetImageColor()
    {
        Color targetColor = new Color(endRed, endGreen, endBlue, endAlpha);
        Color startColor = image.color;
        while (progress < 1)
        {
            image.color = Color.Lerp(startColor, targetColor, progress);
            progress += (Time.deltaTime * speed);
            if (progress >= 1)
            {
                progress = 1f;
                image.color = targetColor;
            }
            yield return null;
        }
    }
    public void TriggerAlphaSpriteTween([Optional] float targetAlpha, [Optional] float targetSpeed)
    {
        if (targetSpeed != 0)
        {
            speed = targetSpeed;
        }
        endAlpha = targetAlpha;
        progress = 0;
        StartCoroutine(SetSpriteColor());
    }

    public void TriggerAlphaImageTween([Optional] float targetAlpha, [Optional] float targetSpeed)
    {
        if (targetSpeed != 0)
        {
            speed = targetSpeed;
        }
        endAlpha = targetAlpha;
        progress = 0;

        //InitImageComponents();
        StartCoroutine(SetImageColor());
    }

}
