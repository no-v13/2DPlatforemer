using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject virtualCamera; //캠 알려줌
    public Player player;

    public Lives lifeDisplayer;
    public GameObject resultPopup; //팝업창 알려줌

    public TextMeshProUGUI scoreLabel;

    public float timeLimit = 30; //30s 안에 깨야 하는 걸로 설정

    public int lives = 3; //목숨 3개

    public bool isCleared; //게임 클리어/죽어서 결과 창이 떴는지 알려주는 변수



    private void Awake()
    {
        Instance = this;  //싱글턴패턴 기술이랑 비슷-> 한번 공부해보자
    }

    void Start()
    {
        lifeDisplayer.SetLives(lives);
        isCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime; //timeLimit을 시간이 흐름에 따라 줄여줌
        scoreLabel.text = "Time Left " + timeLimit.ToString("#.#"); //화면에 시간 표시, ("#.#");-> 소수점 아래 한자리만 표시하겠다!
        //scoreLabel.text = ((int)timeLimit).ToString(); 로 그냥 정수만 표시할 수 O


    }//update

    public void AddTime(float time)
    {
        timeLimit += time; //과일먹은 초 만큼 타임리밋 늘어남
    }

    public void Die()
    {
        virtualCamera.SetActive(false);//죽었을 때 버츄얼 카메라를 꺼버림

        lives--;
        lifeDisplayer.SetLives(lives); //죽을때마다 목숨개수를 라이프디스플레이에 업뎃
        Invoke("Restart", 1); //2초뒤에 죽는 애니메이션 보여주고 다시 Restart
    }//die

    public void Restart()
    {
        if (lives == 0)
        {
            GameOver();
        }
        else
        {
            virtualCamera.SetActive(true);//플레이어 죽으면 카메라 다시 움직이고
            player.Restart();//리스타트!
        }

    }

    public void StageClear()
    {
        isCleared = true;
        resultPopup.SetActive(true);//팝업 켜짐
    }

    public void GameOver()
    {
        isCleared = false;
        resultPopup.SetActive(true);// 게임오버 팝업
    }
}//gameManager