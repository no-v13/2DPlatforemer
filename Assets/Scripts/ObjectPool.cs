using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{//�Ѿ� �߻� ����

    public static ObjectPool Instance; //-1)
    public GameObject bulletPrefab;
    public int bulletLimit = 30; //�Ѿ��� ȭ�鿡 ����� �� �� ����

    List<GameObject> bullets;//�Ѿ��� ����Ʈ�� ������ �Ű�, ������ �巯�� �ʿ� ����

    private void Awake()//-1): �ٸ� ������ ObjectPool�� ���� �� �� ����
    {
        Instance = this;
    }

    void Start()
    {
        bullets = new List<GameObject>();
        for(int i = 0; i < bulletLimit; i++)
        {
            GameObject go = Instantiate(bulletPrefab,transform);
                                                    //ObjectPool�� �ڽ�Object�� ����� ����-> ��������
                        //transform: �ڱ� �ڽ��� ��ġ-> bulletPrefab�� inst�Ǹ� transform ��ġ �ڽ� ��������!
            go.SetActive(false);//����
            bullets.Add(go);// ������ �Ѿ��� ����Ʈ�� �߰�
        }
    }//Start

    public GameObject GetBullet() //�ۿ��� �Ѿ��� �θ��� ����: �̸� ���� ���߿� �ϳ��� ��!
    {
        foreach (GameObject go in bullets)//bullets�� �ִ� �׿� �߿��� ���� ��Ƽ��X�ΰ� ��� ��
        {   
            if (!go.activeSelf)
            {//activeSelf: ���� active�� ���� = ����ִ� ����, !go.activeSelf: ���� ��Ƽ��X ������ if ������!
                go.SetActive(true); //�÷��̾�� Ȱ��ȭ ��ų�Ŵϱ� �Ѽ� ��
                return go; //��Ƽ��X ���� ��Ÿ���� ������
            }
                
        }
        return null; //Ȱ��ȭ�� ���� �� ã�ų�, �Ѿ�30�� ���� ���-> �� �̻� �ȳ����� �ϴ� ���

        /* �Ѿ�30�� ���� ���-> ���ο� ���� ���� �Ѿ� ����Ʈ�� �� �����-> bulletPool�� ����!
          GameObject obj = Instantiate(bulletPrefab,transform);
          bullets.Add(obj);
          return obj;
        */
    }
}
