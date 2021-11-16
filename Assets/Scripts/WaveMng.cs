using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMng : MonoBehaviour
{
    private int _Money;
    public int Money
    {
        get
        {
            return _Money;
        }
        set
        {
            MoneyText.text = value.ToString() + "$";
            _Money = value;
        }
    }

    public UnityEngine.UI.Text MoneyText;

    private int _Wave;
    public int Wave
    {
        get
        {
            return _Wave;
        }
        set
        {
            WaveText.text = "Wave " + value.ToString();
            _Wave = value;
        }
    }

    public UnityEngine.UI.Text WaveText;

    private int _Enemy;
    public int Enemy
    {
        get
        {
            return _Enemy;
        }
        set
        {
            EnemyText.text = "���� �� : " + value.ToString();
            _Enemy = value;
        }
    }

    public UnityEngine.UI.Text EnemyText;

    [System.Serializable]
    public class WaveInfo
    {
        public int EnemyCount;
        public int Difficulty;
    }

    public List<WaveInfo> waveInfos;
    public List<GameObject> spawnPoints;
    public List<GameObject> enemys;

    public IEnumerator Start()
    {
        Money = 1000;
        Wave = 1;
        Enemy = 0;
        foreach (var item in waveInfos)
        {
            for (int i = 0; i <= 5; i++)
            {
                EnemyText.text = "�غ� �ð� : " + (5 - i).ToString();
                yield return new WaitForSeconds(1);
            }
            Enemy = item.EnemyCount;
            for (int i = 0; i < item.EnemyCount; i++)
            {
                GameObject o = Instantiate(enemys[Random.Range(0, enemys.Count - 1)]);
                o.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].transform.position;
            }
            yield return new WaitUntil(() => { return Enemy == 0; });
            Wave++;
        }
    }
}