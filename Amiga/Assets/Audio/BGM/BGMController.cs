using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMController : MonoBehaviour
{

    [SerializeField] private AudioMixer mixer;

    private Coroutine interpolationCoroutine;

    public void SetPauseEffect (bool pauseEffect)
    {
        if (interpolationCoroutine != null) StopCoroutine (interpolationCoroutine);

        if (pauseEffect)
        {
            interpolationCoroutine = StartCoroutine (InterpolateAudioEffect (5000f, 500f, 0.8f, 0.5f));
        }
        else
        {
            interpolationCoroutine = StartCoroutine (InterpolateAudioEffect (22000f, 10f, 1.25f, 0.5f));            
        }
    }

    private IEnumerator InterpolateAudioEffect (float targetLowpass, float targetHighpass, float volumeFactor, float duration)
    {
        // get current values of effects
        float lowpass, highpass, b1Vol, b2Vol, tutVol;
        mixer.GetFloat ("BGM Lowpass", out lowpass);
        mixer.GetFloat ("BGM Highpass", out highpass);
        mixer.GetFloat ("Base Music 1 Volume", out b1Vol);
        mixer.GetFloat ("Base Music 2 Volume", out b2Vol);
        mixer.GetFloat ("Tutorial Music Volume", out tutVol);

        // interpolate over time
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            // get next value for each effect
            float step = Mathf.SmoothStep (0f, 1f, elapsed / duration);
            float nextLowpass = Mathf.Lerp  (lowpass, targetLowpass, step);
            float nextHighpass = Mathf.Lerp (highpass, targetHighpass, step);
            float nextB1Vol = Mathf.Lerp (b1Vol, b1Vol * volumeFactor, step);
            float nextB2Vol = Mathf.Lerp (b2Vol, b2Vol * volumeFactor, step);
            float nextTutVol = Mathf.Lerp (tutVol, tutVol * volumeFactor, step);

            // set each effect to its next value
            mixer.SetFloat ("BGM Lowpass", nextLowpass);
            mixer.SetFloat ("BGM Highpass", nextHighpass);
            mixer.SetFloat ("Base Music 1 Volume", nextB1Vol);
            mixer.SetFloat ("Base Music 2 Volume", nextB2Vol);
            mixer.SetFloat ("Tutorial Music Volume", nextTutVol);

            yield return null;
        }

        // finally set everything to its target value
        mixer.SetFloat ("BGM Lowpass", targetLowpass);
        mixer.SetFloat ("BGM Highpass", targetHighpass);
        mixer.SetFloat ("Base Music 1 Volume", b1Vol * volumeFactor);
        mixer.SetFloat ("Base Music 2 Volume", b2Vol * volumeFactor);
        mixer.SetFloat ("Tutorial Music Volume", tutVol * volumeFactor);
    }
}
