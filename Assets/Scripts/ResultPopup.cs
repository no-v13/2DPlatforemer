using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultPopup : MonoBehaviour
{
    public TextMeshProUGUI titleLabel;
    public TextMeshProUGUI scoreLabel;
    public GameObject highScorePopup;
    public TextMeshProUGUI highScoreLabel;
    public TextMeshProUGUI highScoreListLabel;  // ���� 10���� ����� ǥ���� �ؽ�Ʈ

    private void OnEnable()
    {
        Time.timeScale = 0; // �˾��� �߸� �ð� ����

        highScoreLabel.gameObject.SetActive(false);  // �⺻������ highScoreLabel ��Ȱ��ȭ

        if (GameManager.Instance.isCleared) // ������ Ŭ����Ǹ�
        {
            if (IsNewHighScore())  // �ű�� ���� ���� Ȯ��
            {
                highScoreLabel.text = "Break the record!";
                highScoreLabel.gameObject.SetActive(true);  // �ű���̸� �ؽ�Ʈ Ȱ��ȭ
            }

            titleLabel.text = "Mission Complete!";
            scoreLabel.text = GameManager.Instance.timeLimit.ToString("#.#");  // �Ҽ��� ���ڸ����� ǥ��
        }
        else
        {
            titleLabel.text = "Game Over...";
            scoreLabel.text = "";  // ���� ���� �� ���� ǥ�� �� ��
        }
    }

    bool IsNewHighScore()  // �ű���� Ȯ���ϴ� �Լ�
    {
        float currentScore = GameManager.Instance.timeLimit;
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (currentScore > highScore)  // �ű���� ���
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);  // ��� ����
            PlayerPrefs.Save();
            SaveHighScore();  // �ű���� ���� 10���� ��Ͽ��� �ݿ�
            return true;  // �ű������ ��ȯ
        }

        return false;  // �ű���� �ƴ�
    }

    void SaveHighScore()
    {
        float score = GameManager.Instance.timeLimit;
        string currentScoreString = score.ToString("#.##");

        string savedScoreString = PlayerPrefs.GetString("HighScores", "");

        if (savedScoreString == "")
        {
            PlayerPrefs.SetString("HighScores", currentScoreString);
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(",");
            List<string> scoreList = new List<string>(scoreArray);

            for (int i = 0; i < scoreList.Count; i++)
            {
                float savedScore = float.Parse(scoreList[i]);
                if (savedScore < score)
                {
                    scoreList.Insert(i, currentScoreString); // �� �������� ū ��� ����
                    break;
                }
            }

            if (scoreArray.Length == scoreList.Count)
            {
                scoreList.Add(currentScoreString);  // ������ ��� ��Ϻ��� ������ �������� �߰�
            }

            if (scoreList.Count > 10)
            {
                scoreList.RemoveAt(10);  // ����Ʈ�� 10���� �ʰ��ϸ� ����
            }

            string result = string.Join(",", scoreList);
            PlayerPrefs.SetString("HighScores", result);
        }

        PlayerPrefs.Save();
    }

    // HighScorePopup���� ���� 10�� ����� ǥ���ϴ� �Լ�
    public void HighScorePressed()
    {
        highScorePopup.SetActive(true);

        string savedScoreString = PlayerPrefs.GetString("HighScores", "");
        if (string.IsNullOrEmpty(savedScoreString))
        {
            highScoreListLabel.text = "";
        }
        else
        {
            string[] scoreArray = savedScoreString.Split(",");
            string highScoreList = "";

            for (int i = 0; i < scoreArray.Length; i++)
            {
                highScoreList += (i + 1) + ". " + scoreArray[i] + "\n";  // 1���� �����ؼ� ���� ǥ��
            }

            highScoreListLabel.text = highScoreList;  // ���� 10�� ����� ǥ��
        }
    }

    public void PlayAgainPressed()
    {
        Time.timeScale = 1;  // �ð� �ٽ� �簳
        SceneManager.LoadScene("GameScene");
    }
}
