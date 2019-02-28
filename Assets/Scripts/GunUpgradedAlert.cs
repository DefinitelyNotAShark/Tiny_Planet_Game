using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUpgradedAlert : MonoBehaviour
{
    [SerializeField]
    private float fadeAmount, timeOnScreen;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public IEnumerator StartAlert()
    {
        // lfade from transparent to opaque
        for (float i = 0; i <= fadeAmount; i += Time.deltaTime)//fade in
        {
            text.color = new Color(0, 250, 227, i);
        }

        yield return new WaitForSeconds(timeOnScreen);//wait with the alert on screen

        // fade from opaque to transparent
        for (float i = fadeAmount; i >= 0; i -= Time.deltaTime)//fade out
        {
            text.color = new Color(0, 250, 227, i);
            yield return null;
        }
    }
}
