using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7;
    public float jumpSpeed = 15;
    public Collider2D bottomCollider; // public유니티에서 bottom(캡슐콜라이더 만든거) 지정해서 알려줌(발바닥)
    public CompositeCollider2D terrainCollider;//(땅 콜라이더)-발이랑 땅이랑 붙음? 발이 땅에 있음을 알 수 있음
                                               // public GameObject bulletPrefab; //플레이어가 총알 쏠 수 있게 프리팹으로 만들거임-> 근데 옵젝풀로 관리할 거라 안 씀


    float vx=0; //어느쪽 방향,어느 속도로 움직이는 지
    bool isGrounded; //땅에붙었는지 아닌지 저장

    float preVx = 0; //이전 속도 저장했다가 뛰기 시작(속도0->X) update

    float lastShoot; //총알 쿨타임 설정할 거임- 마지막으로 총알 쏜 시간을 저장

    Vector2 originPositon; // 죽었을때 (생명있는 경우)캐릭을 다시 원위치에 올려놓기 위함

    void Start()
    {
        originPositon = transform.position; //현재위치= 게임이 시작될 때 위치!
    }

    public void Restart()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;//-(*)Die()를 원래대로 돌림(초기화)
        GetComponent<Rigidbody2D>().angularVelocity = 0;                                //-(*)Die() 초기화
       //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);   -(*)Die() 초기화로 올려치는 힘 삭제
        GetComponent<BoxCollider2D>().enabled = true;                                   //-(*)Die() 초기화
        transform.eulerAngles = Vector3.zero; //돌아간 각도를 원상태로 돌리기 위함


        transform.position = originPositon;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //리스폰시 속도0으로 만들려고 
    }

    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal");//GetAxisRaw: 오왼전환시 -1,+1/GetAxis: 오왼전환 부드럽게-가속도 고려시 사용

        if (vx < 0) //캐릭이 왼쪽을 보고 있다면?->좌우반전시키고 싶음 :GetComponent<SpriteRenderer>().flipX 사용
        {
            GetComponent<SpriteRenderer>().flipX = true; 
        }
        if (vx > 0) //if-else문으로 안하고 if문 2개 이유?  (vx = 0)일때 방향 변하지 않기 위해-> 이전 방향 유지
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (bottomCollider.IsTouching(terrainCollider)) //IsTouching 함수를 이용해서 발이랑 땅 콜라이더 붙어있는지 체크가능
        {
            if (!isGrounded) //착지
            {
                if (vx == 0)// 정지
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else
                {
                    if (vx != 0) // 속도0아님-> 달린다!
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
            else //계속 걷는중
            {//땅에 전에도,지금도 붙어있음-> 속도가 바뀌면 애니메이션 바꿈
                if (preVx != vx)//이전속도 != 현재속도
                {
                    if (vx == 0) //이전속도 != 현재속도인데, 현재속도가 0? => 뛰다가 멈춤:Idle
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
            isGrounded = true; //바닥에 붙어있으니까
        }
        else
        {
            if (isGrounded)// 좀전까지 땅에 붙어있었다면? 점프모션으로 바꿔야함
            {
                GetComponent<Animator>().SetTrigger("jump");
            }
               
            isGrounded = false;
        }
            
            if (Input.GetButtonDown("Jump") && isGrounded)//Jump키가 눌리는 순간
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;//Rigidbody2D의 물리엔진으로 위로 속도제공
            }
            preVx = vx;

        ////////////////////총알발사///////////////
        ///
        //- 프리팹으로 instantiate 할 것임 & 총알 방향도 내가 보는 방향이랑 같아야 함
        /* instantiate - 게임 루프 중에 자주 쓰면 렉 걸림
         * -> Object Pooling : Prefab을 미리 instantiate을 여러개 해두고  꺼놨다가 필요시 켬
         * :Object Pool에 미리 instantiate 저장 & 껐다켯다 함
        */

        if (Input.GetButtonDown("Fire1") && lastShoot+0.5f<Time.time)
        {//버튼을 누르는 순간 총알발사(Fire1:플젝세팅 인풋 매니저에 left ctrl키) && 현재시간(Time.time)은 마지막으로 쏜 시간+ 0.5초 보다 커야 함=> 쿨타임0.5s
            Vector2 bulletV = Vector2.zero;

            if (GetComponent<SpriteRenderer>().flipX) //바라보는 방향 알 수 잇음 ->참이면 왼쪽, flip이니까
            {
                bulletV = new Vector2 (-10, 0); //완쪽
            }
            else
            {
                bulletV = new Vector2(10, 0); //오른쪽
            }
            
            //GameObject bullet = Instantiate(bulletPrefab);->렉 걸릴까봐 옵젝풀을 만들어서 관리(밑처럼)
            GameObject bullet = ObjectPool.Instance.GetBullet();

            bullet.transform.position = transform.position; //총알의 위치= 현재(플레이어) 위치
            bullet.GetComponent<Bullet>().velocity = bulletV; //발사
            lastShoot = Time.time; //현재시간 입력
            
            
        }
    }//update

    private void FixedUpdate()//움직이는 물체-> FixedUpdate
    {
        transform.Translate(Vector2.right * vx * speed * Time.fixedDeltaTime);
    }

    /////////적이랑 나랑 상호작용////////////
    private void OnCollisionEnter2D(Collision2D collision)
    {//isTriger가 둘다 안 켜져있을때 OnCollisionEnter2D 사용
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }//OnColli-

    void Die()
    {
        //[죽을때 그래픽 만들고 싶음]//
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //.None:freez rotat 풀음
        GetComponent<Rigidbody2D>().angularVelocity = 720;//빙글빙글 돌게 하려고 각속도(angularVelocity) 줌 & 720->초 당 2바퀴 돔
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10),ForceMode2D.Impulse); //위쪽으로 날리고 싶음(AddForce:물리력 줌)
                                                                                      //ForceMode2D.Impulse위로 뜨는 힘 강하게 함

        GetComponent<BoxCollider2D>().enabled = false;//콜라이더를 꺼서 적이랑 부딫히면 밑으로 떨어지게 함
        //+)적이랑 부딫혀서 죽으면 다시 초기화-> 위에 restart()이용 -(*)

        GameManager.Instance.Die();//죽으면 겜매니저한테 죽은 사실 알려야함
    }//die

}
