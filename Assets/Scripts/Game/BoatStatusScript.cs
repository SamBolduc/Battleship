using Assets.Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatStatusScript : MonoBehaviour
{

    public Text[] myBoatLifeTexts;

    public Text[] ennemyBoatLifeTexts;

    public void UpdateStatus(List<Boat> myBoats, List<Boat> enemyBoats)
    {
        for (var i = 0; i < 5; i++)
        {
            myBoatLifeTexts[i].text = Math.Max(myBoats[i].currentHealth, 0) + " PV";
            ennemyBoatLifeTexts[i].text = Math.Max(enemyBoats[i].currentHealth, 0) + " PV";
        }
    }

}
