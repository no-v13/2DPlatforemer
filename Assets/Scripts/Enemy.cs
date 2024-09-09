using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{//Enemy_Script 할일: 총 맞으면 HP깎여야 함, 움직이는 그래픽 적용

    public int hp = 3;
    public float speed = 3;//인스펙터에서 속도조절할 수 있도록
    Vector2 vx; //현재 이동속도 알 수 있게!

    public Collider2D frontBottomCollider; //fronrBottomCollider가 절벽에 있는지 확인
    public CompositeCollider2D terrainCollider;// 땅을 확인
    public Collider2D frontCollider;//앞을 막는 땅이 있는지 확인

   void Start()
    {
        vx = Vector2.right*speed;//시작시에 오른쪽으로 이동하게끔!
    }

    void Update()
    { 
        if (frontCollider.IsTouching(terrainCollider) || !frontBottomCollider.IsTouching(terrainCollider))
        {//frontBottomCollider가 땅에 닿아 않으면(=절벽)
            vx = -vx; //움직이는 방향 돌려
            //+)표시되는 방향 바꾸는 법 - 1) spriteRenderer의 flipx바꿈:근데 frntBttCol-자식 콜라이더-> 부모방향 바꿀거임
            transform.localScale = new Vector2(-transform.localScale.x, 1);//현재 트랜스폼x가 반대로 바뀜
        }
    }

    private void FixedUpdate()
    {//위치 이동하니까 픽스ㄷ업뎃사용
        transform.Translate(vx*Time.fixedDeltaTime);
    }

    public void Hit(int damage)
    {// 적이 총 맞았을 때 -> Bullet Script에서 불러서 사용
        hp -= damage;
        if (hp == 0) 
        { //적 죽은 상태-> 캐릭터 죽었을 때랑 비슷한 그래픽 만들기
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //프리즈 회전 끔
            GetComponent<Rigidbody2D>().angularVelocity = 720;//빙글 돌게 각속도 줌
            GetComponent<Collider2D>().enabled = false;//콜라이더 꺼줌 (Collider2D쓰면 박스,서클..무관 포함되니까 )
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,10), ForceMode2D.Impulse);//위로 힘 세게!

            Invoke("DestroyThis", 1.0f);
        }
    }//hit

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
