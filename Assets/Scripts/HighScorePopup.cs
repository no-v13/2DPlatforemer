using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScorePopup : MonoBehaviour
{
    public TextMeshProUGUI scoresLable;

    //�״� ���� �� �� ����-> OnEnable ���(Start �ƴ�)
    private void OnEnable()//���� �����ͼ� �Ѹ�
    {
        string[] scores = PlayerPrefs.GetString("HighScores", "").Split(",");
        string result = "";
        for (int i = 0; i < scores.Length; i++)
        {
            result += (i + 1) + ". " + scores[i] + "\n"; //�ٹٲ�:\n
                                                         //(i + 1)ȭ�鿡 n��°�� ������ �迭0���� �����ϴϱ�
        }
        scoresLable.text = result;
    }
    public void ClosePressed()
    {//���� �˾� ��Ȱ
        gameObject.SetActive(false);//���̽���� �˾��� ���� ���� ��Ȱ
    }

}