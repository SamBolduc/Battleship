using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{

    public void DisplayText(string title, string message, int time)
    {
        Text[] texts = GetComponentsInChildren<Text>();
        if (texts.Length == 2)
        {
            texts[0].text = title;
            texts[1].text = message;
        }
        StartCoroutine(FadeOutText(time));
    }

    public void DisplayLoader()
    {

    }

    IEnumerator FadeOutText(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Text[] texts = GetComponentsInChildren<Text>();
        if (texts.Length == 2)
        {
            texts[0].text = "";
            texts[1].text = "";
        }
    }

}
