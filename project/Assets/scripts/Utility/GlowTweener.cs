using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class GlowTweener : BaseTweener
{
    Material material;
    Material testMat;
    SpriteRenderer spriteRenderer;
    Image image;
    float targetIntensity = 1f;
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
        }
        if (TryGetComponent<Image>(out Image imageFound))
        {
            image = imageFound;
            material = image.material = new Material(image.material);
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
    public void SetGlowColor(Color targetColor)
    {
        material.SetColor("_Color", targetColor);
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

    private void OnDestroy()
    {
        Destroy(material);
    }
}
