using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public UnityEngine.UI.Text EndingText;
    public UnityEngine.UI.Text RankText;
    public void OpenEnding()
    {
        EndingText.text = string.Format("���� ���ط� : {0}\n���� ���ط�: {1}\nóġ�� ��: {2}\n�ı��� Ÿ��: {3}\n��ƾ �ð�: {4}", Stat.DmgGiven, Stat.DmgTaken, Stat.EnemyKilled, Stat.TowerDetroyed, Stat.TimeElapsed);
        EndingText.transform.parent.parent.gameObject.SetActive(true);
        int wave = FindObjectOfType<WaveMng>().Wave;
        if (wave < 3) RankText.text = "C";
        else if (wave < 5) RankText.text = "B";
        else if (wave < 7) RankText.text = "A";
        else RankText.text = "S";
    }

    public static void Share()
    {

    }

    public static void Exit()
    {
        Stat.DmgGiven = 0;
        Stat.DmgTaken = 0;
        Stat.EnemyKilled = 0;
        Stat.TowerDetroyed = 0;
        Stat.TimeElapsed = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
