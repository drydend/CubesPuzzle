using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField]
    private Image _screen;

    [SerializeField]
    private float _fadeTime;
    [SerializeField]
    private float _unFadeTime;

    public IEnumerator Fade()
    {
        var timeElapsedNormalized = 0f;

        while (_screen.color.a != 1)
        {
            var alpha = Mathf.Lerp(0, 1, timeElapsedNormalized);
            var newColor = new Color(_screen.color.r, _screen.color.g, _screen.color.b, alpha);

            _screen.color = newColor;

            timeElapsedNormalized += Time.deltaTime / _fadeTime;
            timeElapsedNormalized = Mathf.Clamp(timeElapsedNormalized, 0, 1);

            yield return null;
        }
    }

    public IEnumerator UnFade()
    {
        var timeElapsedNormalized = 0f;

        while (_screen.color.a != 1)
        {
            var alpha = Mathf.Lerp(1, 0, timeElapsedNormalized);
            var newColor = new Color(_screen.color.r, _screen.color.g, _screen.color.b, alpha);

            _screen.color = newColor;
            
            timeElapsedNormalized += Time.deltaTime / _unFadeTime;
            timeElapsedNormalized = Mathf.Clamp(timeElapsedNormalized, 0, 1);

            yield return null;
        }
    }
}