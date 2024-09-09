using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour //���ư� ����, ��� �΋H���� ����� ����
{
    public Vector2 velocity = new Vector2(10, 0);
    
        
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Terrain") 
        { 
            gameObject.SetActive(false);//Ȱ��ȭ ���� ����Ǯ���� �ٽ� ���� ���
        }//if tag-Ter
        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit(1);
            gameObject.SetActive(false);//�Ѿ˽����� �ٽ� ����
        }

    }

}
