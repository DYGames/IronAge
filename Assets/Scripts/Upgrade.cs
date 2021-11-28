using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public static void AttackUpgrade()
    {
        foreach (var item in FindObjectsOfType<Building>())
        {
            item.Power++;
            item.Upgrade();
        }
    }

    public static void DefenseUpgrade()
    {
        foreach (var item in FindObjectsOfType<Building>())
        {
            item.Defense++;
            item.Upgrade();
        }
    }

    public static void SpeedUpgrade()
    {
        foreach (var item in FindObjectsOfType<Building>())
        {
            item.Speed++;
            item.Upgrade();
        }
    }
}
