using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    /// <summary>
    /// Reference to the UI Text component.
    /// </summary>
    public TextMeshProUGUI eventText;

    /// <summary>
    /// Duration of the fade-out effect.
    /// </summary>
    public float fadeDuration = 2f;

    /// <summary>
    /// How long to display the text before fading.
    /// </summary>
    public float displayDuration = 2f;

    private Color originalColor;

    void Start()
    {
        eventText = GetComponent<TextMeshProUGUI>();

        // Store the original color of the text (to reset it later)
        originalColor = eventText.color;
        // Make it invisible at start
        eventText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }

    /// <summary>
    /// Display a sentence and trigger the fade-out effect
    /// </summary>
    /// <param name="message"></param>
    public void TriggerText(string message)
    {
        // In case the text is already fading, stop the current coroutine
        StopAllCoroutines();
        eventText.text = message;
        // Reset the color to fully visible
        eventText.color = originalColor;
        StartCoroutine(FadeText());
    }

    /// <summary>
    /// Coroutine that handles fading out the text after a short delay
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeText()
    {
        // Wait for the displayDuration before starting to fade out
        yield return new WaitForSeconds(displayDuration);

        float elapsedTime = 0f;

        // Gradually reduce the alpha value of the text color to create a fading effect
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Fade from 1 to 0 alpha
            eventText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Optionally hide the text completely after fading out
        eventText.text = "";
    }
}
