using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)//EndPoint->isTrigger�� OnTriggerEnter2D ���
    {//Ʈ���� ���õ� �ݶ��̴��� �ٸ� ���� ������ �̺�Ʈ �߻�, (Collider2D collision)�� �΋H�� ���� ����
        if(collision.tag == "Player") //�΋H�� �ݶ��̴��� �±װ� �÷��̾�? �÷��̾ ������������ �Դٴ� ��
        {
            GameManager.Instance.StageClear();
        }
    }
}
