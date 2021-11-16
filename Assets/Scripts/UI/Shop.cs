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
            o.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                UIMng.Build(item.code);
            });
        }
        foreach (var item in upgradeItems)
        {
            GameObject o = Instantiate(ShopButton);
            o.transform.parent = Content;
            o.name = "Upgrade";
            o.GetComponentInChildren<UnityEngine.UI.Text>().text = item.name + System.Environment.NewLine + item.price.ToString();
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
