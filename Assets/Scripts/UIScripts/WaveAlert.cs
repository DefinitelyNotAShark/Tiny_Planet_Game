using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAlert : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public IEnumerator FadeImage()
    {

        // lfade from transparent to opaque
        for (float i = 0; i <= fadeTime; i += Time.deltaTime)
        {
            image.color = new Color(0, 0, 0, i);
        }

        // fade from opaque to transparent
        for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
        {
            image.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}

