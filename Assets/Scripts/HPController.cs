using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    public int MaxHP;
    private int _HP;
    public int HP
    {
        get
        {
            return _HP;
        }
        set
        {
            _HP = value;
            HPBar.localScale = new Vector3(_HP / MaxHP, 1, 1);
        }
    }
    public Transform HPBar;
}
