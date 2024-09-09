using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float timeAdd = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") //�����̶� �÷��̾� �浹�� �׸޴������� �˷���
        {
            GameManager.Instance.AddTime(timeAdd); //timeAdd��ŭ �÷���
            GetComponent<Animator>().SetTrigger("Eaten"); //�ִϸ��̼� ���
            Invoke("DestroyThis", 0.3f);
        }
    }//OnTriggerEnter

    void DestroyThis() //6������ 20�� ��� -> 0.3 ���� �ִϸ��̼� ����� �� �Լ� �ҷ��� ���� ����
    {
        Destroy(gameObject);//Destroy(gameObject); //���� ������ ������� �� �� ����
    }
}
