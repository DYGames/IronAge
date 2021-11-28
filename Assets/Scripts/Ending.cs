using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public UnityEngine.UI.Text EndingText;
    public void OpenEnding()
    {
        EndingText.text = string.Format("입힌 피해량 : {0}\n받은 피해량: {1}\n처치한 적: {2}\n파괴된 타워: {3}\n버틴 시간: {4}", Stat.DmgGiven, Stat.DmgTaken, Stat.EnemyKilled, Stat.TowerDetroyed, Stat.TimeElapsed);
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
