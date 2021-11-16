using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMng : MonoBehaviour
{
    static public void On(GameObject target)
    {
        target.SetActive(true);
    }

    static public void Off(GameObject target)
    {
        target.SetActive(false);
    }

    static public void Toggle(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }

    static public void OnClick(GameObject target)
    {
        target.SendMessage("OnClick");
    }
}
