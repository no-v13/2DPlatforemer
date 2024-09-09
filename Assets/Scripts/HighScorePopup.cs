using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScorePopup : MonoBehaviour
{
    public TextMeshProUGUI scoresLable;

    //켰다 껏다 할 수 있음-> OnEnable 사용(Start 아님)
    private void OnEnable()//점수 가져와서 뿌림
    {
        string[] scores = PlayerPrefs.GetString("HighScores", "").Split(",");
        string result = "";
        for (int i = 0; i < scores.Length; i++)
        {
            result += (i + 1) + ". " + scores[i] + "\n"; //줄바꿈:\n
                                                         //(i + 1)화면에 n번째로 나오게 배열0부터 시작하니까
        }
        scoresLable.text = result;
    }
    public void ClosePressed()
    {//현재 팝업 비활
        gameObject.SetActive(false);//하이스토어 팝업에 붙은 옵젝 비활
    }

}