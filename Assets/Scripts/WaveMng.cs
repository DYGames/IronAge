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
            EnemyText.text = "남은 적 : " + value.ToString();
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

    private float[] heightMins;

    public IEnumerator Start()
    {
        Money = 10000;
        Wave = 1;
        Enemy = 0;

        yield return new WaitUntil(() => GameStart.isGameStart);

        heightMins = new float[enemys.Count];

        RandomizeEnemies(false, false, false, true);

        foreach (var item in waveInfos)
        {
            for (int i = 0; i <= 5; i++)
            {
                EnemyText.text = "준비 시간 : " + (5 - i).ToString();
                yield return new WaitForSeconds(1);
            }
            Enemy = item.EnemyCount;
            for (int i = 0; i < item.EnemyCount; i++)
            {
                GameObject o = Instantiate(enemys[Random.Range(0, enemys.Count - 1)]);
                o.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].transform.position;
                o.GetComponent<HPController>().MaxHP = Random.Range(1, 3) * item.Difficulty;
                o.GetComponentInChildren<HeightMapMesh>().heightMin = heightMins[o.GetComponent<Enemy>().Type];
            }
            yield return new WaitUntil(() => { return Enemy == 0; });
            Wave++;
            RandomizeEnemies(false, true, true, true);
            foreach (Building building in FindObjectsOfType<Building>())
            {
                HPController hP = building.gameObject.GetComponent<HPController>();
                hP.MaxHP = hP.MaxHP;
            }
        }
    }

    private void RandomizeEnemies(bool content, bool style, bool alpha, bool heightMin)
    {
        Texture2D[] contentTextures = StyleTransferManager.instance.contentTextures;
        Texture2D[] styleTextures = StyleTransferManager.instance.styleTextures;

        for (int index = 0; index < enemys.Count; index++)
        {
            var evalutaion = StyleTransferManager.instance.GetEvaluation(index);

            if (content)
            {
                evalutaion.contentTexture = contentTextures[Random.Range(0, contentTextures.Length)];
            }

            if (style)
            {
                evalutaion.styleTexture = styleTextures[Random.Range(0, styleTextures.Length)];
            }

            if (alpha)
            {
                evalutaion.alphaValue = Random.value;
            }

            if (content || style || alpha)
            {
                StyleTransferManager.instance.Evaluate(index, evalutaion.contentTexture, evalutaion.styleTexture, evalutaion.alphaValue);
            }

            if (heightMin)
            {
                heightMins[index] = Random.Range(0.1f, 0.8f);
            }
        }

        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.ApplyStyleTransfer();
            enemy.GetComponentInChildren<HeightMapMesh>().heightMin = heightMins[enemy.Type];
        }
    }
}
