using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopButton;
    public Transform Content;
    [System.Serializable]
    public class ShopItem
    {
        public string name;
        public int code;
        public int price;
        public int HP;
        public int Power;
        public float Speed;
        public int Defense;
        public GameObject obj;
    }
    public List<ShopItem> shopItems;
    [System.Serializable]
    public class UpgradeItem
    {
        public string name;
        public int code;
        public int price;
    }
    public List<UpgradeItem> upgradeItems;
    public int type = 0;

    void Start()
    {
        foreach (var item in shopItems)
        {
            GameObject o = Instantiate(ShopButton);
            o.transform.parent = Content;
            o.name = "Shop";
            o.GetComponentInChildren<UnityEngine.UI.Text>().text = item.name + System.Environment.NewLine + item.price.ToString();
            o.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                WaveMng m = FindObjectOfType<WaveMng>();
                if (m.Money >= item.price)
                {
                    m.Money -= item.price;
                    GameObject o = Instantiate(shopItems[item.code].obj);
                    o.GetComponent<HPController>().MaxHP = shopItems[item.code].HP;
                    o.GetComponent<Building>().Power = shopItems[item.code].Power;
                    o.GetComponent<Building>().Defense = shopItems[item.code].Defense;
                    o.GetComponent<Building>().Speed = shopItems[item.code].Speed;
                }
            });
        }
        foreach (var item in upgradeItems)
        {
            GameObject o = Instantiate(ShopButton);
            o.transform.parent = Content;
            o.name = "Upgrade";
            o.GetComponentInChildren<UnityEngine.UI.Text>().text = item.name + System.Environment.NewLine + item.price.ToString();
            o.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                if (item.code == 0)
                    Upgrade.AttackUpgrade();
                else if (item.code == 1)
                    Upgrade.DefenseUpgrade();
                else if (item.code == 2)
                    Upgrade.SpeedUpgrade();
            });
        }
        transform.localScale = new Vector3(0, 1, 1);
    }

    public void OnClick()
    {
        type = ((type + 1) > 2) ? 0 : type + 1;
        transform.localScale = new Vector3(type != 0 ? 1 : 0, 1, 1);
        foreach (var item in Content)
        {
            if ((item as Transform).gameObject.name.CompareTo("Shop") == 0) (item as Transform).gameObject.SetActive(type == 1);
            if ((item as Transform).gameObject.name.CompareTo("Upgrade") == 0) (item as Transform).gameObject.SetActive(type == 2);
        }
    }
}
