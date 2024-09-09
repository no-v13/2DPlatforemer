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
    public TextMeshProUGUI highScoreListLabel;  // 상위 10개의 기록을 표시할 텍스트

    private void OnEnable()
    {
        Time.timeScale = 0; // 팝업이 뜨면 시간 멈춤

        highScoreLabel.gameObject.SetActive(false);  // 기본적으로 highScoreLabel 비활성화

        if (GameManager.Instance.isCleared) // 게임이 클리어되면
        {
            if (IsNewHighScore())  // 신기록 갱신 여부 확인
            {
                highScoreLabel.text = "Break the record!";
                highScoreLabel.gameObject.SetActive(true);  // 신기록이면 텍스트 활성화
            }

            titleLabel.text = "Mission Complete!";
            scoreLabel.text = GameManager.Instance.timeLimit.ToString("#.#");  // 소수점 한자리까지 표시
        }
        else
        {
            titleLabel.text = "Game Over...";
            scoreLabel.text = "";  // 게임 오버 시 점수 표시 안 함
        }
    }

    bool IsNewHighScore()  // 신기록을 확인하는 함수
    {
        float currentScore = GameManager.Instance.timeLimit;
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (currentScore > highScore)  // 신기록일 경우
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);  // 기록 갱신
            PlayerPrefs.Save();
            SaveHighScore();  // 신기록을 상위 10개의 기록에도 반영
            return true;  // 신기록임을 반환
        }

        return false;  // 신기록이 아님
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
                    scoreList.Insert(i, currentScoreString); // 전 점수보다 큰 경우 삽입
                    break;
                }
            }

            if (scoreArray.Length == scoreList.Count)
            {
                scoreList.Add(currentScoreString);  // 점수가 모두 기록보다 낮으면 마지막에 추가
            }

            if (scoreList.Count > 10)
            {
                scoreList.RemoveAt(10);  // 리스트가 10개를 초과하면 제거
            }

            string result = string.Join(",", scoreList);
            PlayerPrefs.SetString("HighScores", result);
        }

        PlayerPrefs.Save();
    }

    // HighScorePopup에서 상위 10개 기록을 표시하는 함수
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
                highScoreList += (i + 1) + ". " + scoreArray[i] + "\n";  // 1부터 시작해서 순위 표시
            }

            highScoreListLabel.text = highScoreList;  // 상위 10개 기록을 표시
        }
    }

    public void PlayAgainPressed()
    {
        Time.timeScale = 1;  // 시간 다시 재개
        SceneManager.LoadScene("GameScene");
    }
}
