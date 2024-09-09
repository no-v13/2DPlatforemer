using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject virtualCamera; //ķ �˷���
    public Player player;

    public Lives lifeDisplayer;
    public GameObject resultPopup; //�˾�â �˷���

    public TextMeshProUGUI scoreLabel;

    public float timeLimit = 30; //30s �ȿ� ���� �ϴ� �ɷ� ����

    public int lives = 3; //��� 3��

    public bool isCleared; //���� Ŭ����/�׾ ��� â�� ������ �˷��ִ� ����



    private void Awake()
    {
        Instance = this;  //�̱������� ����̶� ���-> �ѹ� �����غ���
    }

    void Start()
    {
        lifeDisplayer.SetLives(lives);
        isCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime; //timeLimit�� �ð��� �帧�� ���� �ٿ���
        scoreLabel.text = "Time Left " + timeLimit.ToString("#.#"); //ȭ�鿡 �ð� ǥ��, ("#.#");-> �Ҽ��� �Ʒ� ���ڸ��� ǥ���ϰڴ�!
        //scoreLabel.text = ((int)timeLimit).ToString(); �� �׳� ������ ǥ���� �� O


    }//update

    public void AddTime(float time)
    {
        timeLimit += time; //���ϸ��� �� ��ŭ Ÿ�Ӹ��� �þ
    }

    public void Die()
    {
        virtualCamera.SetActive(false);//�׾��� �� ����� ī�޶� ������

        lives--;
        lifeDisplayer.SetLives(lives); //���������� ��������� ���������÷��̿� ����
        Invoke("Restart", 1); //2�ʵڿ� �״� �ִϸ��̼� �����ְ� �ٽ� Restart
    }//die

    public void Restart()
    {
        if (lives == 0)
        {
            GameOver();
        }
        else
        {
            virtualCamera.SetActive(true);//�÷��̾� ������ ī�޶� �ٽ� �����̰�
            player.Restart();//����ŸƮ!
        }

    }

    public void StageClear()
    {
        isCleared = true;
        resultPopup.SetActive(true);//�˾� ����
    }

    public void GameOver()
    {
        isCleared = false;
        resultPopup.SetActive(true);// ���ӿ��� �˾�
    }
}//gameManager