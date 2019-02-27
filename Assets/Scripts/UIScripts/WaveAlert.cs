using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAlert : MonoBehaviour
{
    [SerializeField]
    private float fadeAmount, timeOnScreen;

    private Image image;
    private Text text;
    private int waveCounter;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
        waveCounter = 1;
    }

    public IEnumerator StartAlert()
    {
        text.text = "Level " + waveCounter + ":\nA new wave of enemies is approaching!";
        // lfade from transparent to opaque
        for (float i = 0; i <= fadeAmount; i += Time.deltaTime)//fade in
        {
            yield return new WaitForSeconds(.08f);//make the fade slower

            image.color = new Color(0, 0, 0, i);
            text.color = new Color(255, 255, 255, i);
        }

        yield return new WaitForSeconds(fadeAmount);//wait with the alert on screen

        // fade from opaque to transparent
        for (float i = fadeAmount; i >= 0; i -= Time.deltaTime)//fade out
        {
            yield return new WaitForSeconds(.01f);//make the fade slower

            image.color = new Color(0, 0, 0, i);
            text.color = new Color(255, 255, 255, i);
            yield return null;
        }

        waveCounter++;
    }
}

