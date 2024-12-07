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
    Color color;
  
    void Start()
    {
        InitImageComponents();
    }

    private void OnEnable()
    {
        InitImageComponents();
    }

    void InitImageComponents()
    {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRendererFound))
        {
            spriteRenderer = spriteRendererFound;
            color = spriteRenderer.color;
        }
        if (TryGetComponent<Image>(out Image imageFound))
        {
            image = imageFound;
            color = image.color;
        }
    }

    // REFACTOR: create reference to the "color" and set it in the init func. then just modify the color in the tween

    public void SetImageAlpha(float targetAlpha = 0)
    {
        image.color = new Color(endRed, endGreen, endBlue, targetAlpha);
    }

    public void SetSpriteAlpha(float targetAlpha = 0)
    {
        spriteRenderer.color = new Color(endRed, endGreen, endBlue, targetAlpha);
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

    IEnumerator SetColor()
    {
        Color targetColor = new Color(endRed, endGreen, endBlue, endAlpha);
        Color startColor = color;
        while (progress < 1)
        {
            color = Color.Lerp(startColor, targetColor, progress);
            progress += (Time.deltaTime * speed);
            if (progress >= 1)
            {
                progress = 1f;
                image.color = targetColor;
            }
            yield return null;
        }
    }

    IEnumerator SetSpriteColorByDuration(float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration); //Elapsed time
        Color targetColor = new Color(endRed, endGreen, endBlue, endAlpha);
        Color startColor = spriteRenderer.color;

        while (elapsed_time < duration)
        {
            spriteRenderer.color = Color.Lerp(startColor, targetColor, EaseInOutQuad(elapsed_time / duration));
            yield return null;
            elapsed_time += Time.deltaTime;
        }

        spriteRenderer.color = targetColor;
    }

    IEnumerator SetTargetColorByDuration(Color targetColor, float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration); //Elapsed time
        //Color targetColor = new Color(endRed, endGreen, endBlue, endAlpha);
        Color startColor = image.color;

        while (elapsed_time < duration)
        {
            image.color = Color.Lerp(startColor, targetColor, EaseInOutQuad(elapsed_time / duration));
            yield return null;
            elapsed_time += Time.deltaTime;
        }
        image.color = targetColor;
    }

    IEnumerator SetImageColorByDuration(float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration); //Elapsed time
        Color targetColor = new Color(endRed, endGreen, endBlue, endAlpha);
        Color startColor = image.color;

        while (elapsed_time < duration)
        {
            image.color = Color.Lerp(startColor, targetColor, EaseInOutQuad(elapsed_time / duration));
            yield return null;
            elapsed_time += Time.deltaTime;
        }
        image.color = targetColor;
    }

    public void TriggerAlphaSpriteTween([Optional] float targetAlpha, [Optional] float targetSpeed)
    {
        if (targetSpeed != 0)
        {
            speed = targetSpeed;
        }
        endAlpha = targetAlpha;
        progress = 0;
        InitImageComponents();

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

        StartCoroutine(SetImageColor());
    }

    public void TriggerSpriteAlphaByDuration([Optional] float targetAlpha, [Optional] float duration)
    {
        endAlpha = targetAlpha;
        StartCoroutine(SetSpriteColorByDuration(duration));
    }
    public void TriggerImageAlphaByDuration([Optional] float targetAlpha, [Optional] float duration)
    {
        endAlpha = targetAlpha;
        StartCoroutine(SetImageColorByDuration(duration));
    }

    public void TriggerImageColorByDuration(Color targetColor, float duration)
    {
        StartCoroutine(SetTargetColorByDuration(targetColor, duration));
    }
}
