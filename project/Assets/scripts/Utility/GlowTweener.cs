using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class GlowTweener : BaseTweener
{
    Material material;
    //Material testMat;
    SpriteRenderer spriteRenderer;
    Image image;
    float targetIntensity = 1f;
    Color originalColor;
    void Start()
    {
        InitGlowComponents();
    }

    void InitGlowComponents()
    {
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRendererFound))
        {
            spriteRenderer = spriteRendererFound;
            material = spriteRenderer.material = new Material(spriteRenderer.material);
            originalColor = material.color;
        }
        if (TryGetComponent<Image>(out Image imageFound))
        {
            image = imageFound;
            material = image.material = new Material(image.material);
            originalColor = material.color;
        }
        
    }

    public void TurnOffGlow()
    {
        material.SetFloat("_Fade", 0);
    }

    public void TurnOnGlow()
    {
        material.SetFloat("_Fade", targetIntensity);
    }
    public void LogGlowAmount()
    {
        print(material.GetFloat("_Fade"));
    }
    public void SetGlowColor(Color targetColor, float targetIntensity = 7f)
    {
        material.SetColor("_Color", targetColor * Mathf.Pow(2, targetIntensity));
    }

    public void ResetGlowColor()
    {
        material.SetColor("_Color", originalColor);
    }
    IEnumerator SetGlow()
    {
        while (progress < 1)
        {
            float startIntensity = material.GetFloat("_Fade");
            float currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, progress);
            material.SetFloat("_Fade", currentIntensity);
            progress += (Time.deltaTime * speed);
            if (progress >= 1)
            {
                progress = 1f;
                material.SetFloat("_Fade", targetIntensity);
            }
            yield return null;
        }
    }

    IEnumerator SetGlowByDuration(float targetGlow, float duration)
    {
        float elapsed_time = Mathf.Clamp(0, 0, duration); //Elapsed time

        float startGlow = material.GetFloat("_Fade");
        while (elapsed_time < duration)
        {
            float currentIntensity = Mathf.Lerp(startGlow, targetGlow, EaseInOutQuad(elapsed_time / duration));
            material.SetFloat("_Fade", currentIntensity);
            yield return null;
            elapsed_time += Time.deltaTime;
        }
    }
    public void TriggerGlowTween([Optional] float targetGlowIntensity, [Optional] float targetSpeed)
    {
        if (targetSpeed != 0)
        {
            speed = targetSpeed;
        }
        targetIntensity = targetGlowIntensity;
        progress = 0;

        StartCoroutine(SetGlow());
    }

    public void TriggerGlowByDuration([Optional] float targetGlow, [Optional] float duration)
    {
        StartCoroutine(SetGlowByDuration(targetGlow, duration));
    }

    private void OnDestroy()
    {
        Destroy(material);
    }
}
