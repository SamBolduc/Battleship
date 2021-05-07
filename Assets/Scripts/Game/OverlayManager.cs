using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class OverlayManager : MonoBehaviour
{

    private int _id;
    public void DisplayText(string title, string message, int time)
    {
        Text[] texts = GetComponentsInChildren<Text>();
        if (texts.Length == 2)
        {
            texts[0].text = title;
            texts[1].text = message;
        }

        int id = new Random().Next(100);
        if (id == _id) id++;

        StartCoroutine(FadeOutText(time, id));
    }

    public void DisplayLoader()
    {

    }

    IEnumerator FadeOutText(int seconds, int id)
    {
        yield return new WaitForSeconds(seconds);
        Text[] texts = GetComponentsInChildren<Text>();
        if (texts.Length == 2 && _id == id)
        {
            texts[0].text = "";
            texts[1].text = "";
        }
    }

}
