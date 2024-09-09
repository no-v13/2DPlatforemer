using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour //날아갈 것임, 어디에 부딫히면 사라질 거임
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
            gameObject.SetActive(false);//활성화 끄고 옵젝풀에서 다시 쓰길 대기
        }//if tag-Ter
        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit(1);
            gameObject.SetActive(false);//총알썼으니 다시 꺼줌
        }

    }

}
