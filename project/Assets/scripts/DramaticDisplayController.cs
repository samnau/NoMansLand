using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DramaticDisplayController : MonoBehaviour
{
    public enum DisplayColor
    {
        Red,
        Black,
        White
    }

    [Header("Dramatic Display Settings")]
    public GameObject targetObject;
    public DisplayColor spriteColor = DisplayColor.Red;
    public DisplayColor backgroundColor = DisplayColor.Black;
    public float displayDuration = 2.0f;

    [Header("Background Field Settings")]
    public GameObject backgroundField;
    public float backgroundFadeInTime = 0.5f;
    public float backgroundFadeOutTime = 0.5f;
    public int foregroundSortingOrder = 1000;

    private Dictionary<DisplayColor, Color> colorMap = new Dictionary<DisplayColor, Color>
    {
        { DisplayColor.Red, Color.red },
        { DisplayColor.Black, Color.black },
        { DisplayColor.White, Color.white }
    };

    private List<SpriteRenderer> originalSpriteRenderers = new List<SpriteRenderer>();
    private List<Color> originalColors = new List<Color>();
    private List<int> originalSortingOrders = new List<int>();
    private CanvasGroup backgroundCanvasGroup;
    private bool isDisplaying = false;

    private void Awake()
    {
        if (backgroundField != null)
        {
            backgroundCanvasGroup = backgroundField.GetComponent<CanvasGroup>();
            if (backgroundCanvasGroup == null)
            {
                backgroundCanvasGroup = backgroundField.AddComponent<CanvasGroup>();
            }
            backgroundField.SetActive(false);
        }
    }

    public void TriggerDramaticDisplay()
    {
        if (isDisplaying)
        {
            Debug.LogWarning("Dramatic display already in progress!");
            return;
        }

        // Auto-create background field if needed
        if (backgroundField == null)
        {
            CreateBackgroundField();
        }

        StartCoroutine(DramaticDisplayCoroutine());
    }

    public void TriggerDramaticDisplay(GameObject customTarget, DisplayColor customSpriteColor, 
                                       DisplayColor customBackgroundColor, float customDuration)
    {
        if (isDisplaying)
        {
            Debug.LogWarning("Dramatic display already in progress!");
            return;
        }

        // Auto-create background field if needed
        if (backgroundField == null)
        {
            CreateBackgroundField();
        }

        targetObject = customTarget;
        spriteColor = customSpriteColor;
        backgroundColor = customBackgroundColor;
        displayDuration = customDuration;

        StartCoroutine(DramaticDisplayCoroutine());
    }

    private IEnumerator DramaticDisplayCoroutine()
    {
        isDisplaying = true;

        // Store original sprite colors and apply new colors
        StoreAndChangeSpriteColors();

        // Show background field
        if (backgroundField != null)
        {
            yield return StartCoroutine(ShowBackgroundField());
        }

        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Hide background field
        if (backgroundField != null)
        {
            yield return StartCoroutine(HideBackgroundField());
        }

        // Restore original sprite colors
        RestoreSpriteColors();

        isDisplaying = false;
    }

    private void StoreAndChangeSpriteColors()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object is null!");
            return;
        }

        originalSpriteRenderers.Clear();
        originalColors.Clear();
        originalSortingOrders.Clear();

        // Get all SpriteRenderers in the target object and its children
        SpriteRenderer[] spriteRenderers = targetObject.GetComponentsInChildren<SpriteRenderer>();

        Color targetColor = colorMap[spriteColor];

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            originalSpriteRenderers.Add(renderer);
            originalColors.Add(renderer.color);
            originalSortingOrders.Add(renderer.sortingOrder);
            
            // Change color and bring to front
            renderer.color = targetColor;
            renderer.sortingOrder = foregroundSortingOrder;
        }
    }

    private void RestoreSpriteColors()
    {
        for (int i = 0; i < originalSpriteRenderers.Count && i < originalColors.Count && i < originalSortingOrders.Count; i++)
        {
            if (originalSpriteRenderers[i] != null)
            {
                originalSpriteRenderers[i].color = originalColors[i];
                originalSpriteRenderers[i].sortingOrder = originalSortingOrders[i];
            }
        }

        originalSpriteRenderers.Clear();
        originalColors.Clear();
        originalSortingOrders.Clear();
    }

    private IEnumerator ShowBackgroundField()
    {
        if (backgroundField == null) yield break;

        Color bgColor = colorMap[backgroundColor];
        
        // Set background color
        SpriteRenderer bgRenderer = backgroundField.GetComponent<SpriteRenderer>();
        if (bgRenderer != null)
        {
            bgRenderer.color = bgColor;
            bgRenderer.sortingOrder = foregroundSortingOrder - 1; // Background just behind target
        }

        backgroundField.SetActive(true);

        // Instant show
        if (backgroundCanvasGroup != null)
        {
            backgroundCanvasGroup.alpha = 1f;
        }
    }

    private IEnumerator HideBackgroundField()
    {
        if (backgroundField == null) yield break;

        // Instant hide
        if (backgroundCanvasGroup != null)
        {
            backgroundCanvasGroup.alpha = 0f;
        }

        backgroundField.SetActive(false);
    }

    // Helper method to create a background field if none is assigned
    [ContextMenu("Create Background Field")]
    public void CreateBackgroundField()
    {
        if (backgroundField == null)
        {
            // Create a new GameObject for the background (don't parent it to avoid scale inheritance)
            backgroundField = new GameObject("DramaticBackground");
            // backgroundField.transform.SetParent(transform); // Commented out to keep world space

            // Add SpriteRenderer
            SpriteRenderer renderer = backgroundField.AddComponent<SpriteRenderer>();
            renderer.sprite = CreateBackgroundSprite();
            renderer.color = colorMap[backgroundColor];

            // Add CanvasGroup for fading
            backgroundCanvasGroup = backgroundField.AddComponent<CanvasGroup>();
            backgroundCanvasGroup.alpha = 0f;

            // Position and scale it to cover the screen
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                // Position the background at the camera's center, but behind game objects
                // In 2D, negative Z is typically "behind" the camera
                backgroundField.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -1f); // Set Z to -1 for background layer
                
                // For 2D games with an orthographic camera, calculate the visible world height and width
                float worldScreenHeight = mainCamera.orthographicSize * 2f;
                float worldScreenWidth = worldScreenHeight * mainCamera.aspect;
                
                // Apply precise scale (no multiplier needed since we're in world space)
                backgroundField.transform.localScale = new Vector3(worldScreenWidth, worldScreenHeight, 1);
                
                // Debug logging to check values
                Debug.Log($"Camera orthographicSize: {mainCamera.orthographicSize}, Aspect: {mainCamera.aspect}");
                Debug.Log($"World Screen Size: {worldScreenWidth} x {worldScreenHeight}");
                Debug.Log($"Background Scale: {backgroundField.transform.localScale}");
                Debug.Log($"Background Position: {backgroundField.transform.position}");
                backgroundField.transform.rotation = mainCamera.transform.rotation;
            }

            backgroundField.SetActive(false);

            Debug.Log("Background field created!");
        }
        else
        {
            Debug.LogWarning("Background field already exists!");
        }
    }

    private Sprite CreateBackgroundSprite()
    {
        // Create a simple white square sprite
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
    }

    private void OnValidate()
    {
        // Ensure display duration is positive
        if (displayDuration <= 0)
        {
            displayDuration = 0.1f;
        }

        // Ensure fade times are positive
        if (backgroundFadeInTime <= 0)
        {
            backgroundFadeInTime = 0.1f;
        }

        if (backgroundFadeOutTime <= 0)
        {
            backgroundFadeOutTime = 0.1f;
        }
    }
}
