using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    private int _MaxHP;
    public int MaxHP
    {
        get
        {
            return _MaxHP;
        }
        set
        {
            _MaxHP = value;
            HP = value;
        }
    }
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
            if (_HP <= 0)
            {
                _HP = 0;
                gameObject.SendMessage("Destroy");
            }
            HPBar.localScale = new Vector3(1, 1, (float)_HP / MaxHP);
        }
    }
    public Transform HPBar;

    public IEnumerator Start()
    {
        while(true)
        {
            HPBar.transform.parent.rotation = Quaternion.Euler(0, 135, 30);
            yield return null;
        }
    }
}
