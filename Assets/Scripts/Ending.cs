using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public UnityEngine.UI.Text EndingText;
    public void OpenEnding()
    {
        EndingText.text = string.Format("���� ���ط� : {0}\n���� ���ط�: {1}\nóġ�� ��: {2}\n�ı��� Ÿ��: {3}\n��ƾ �ð�: {4}", Stat.DmgGiven, Stat.DmgTaken, Stat.EnemyKilled, Stat.TowerDetroyed, Stat.TimeElapsed);
        EndingText.transform.parent.parent.gameObject.SetActive(true);
    }

    public static void Share()
    {

    }

    public static void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
