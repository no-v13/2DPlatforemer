using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7;
    public float jumpSpeed = 15;
    public Collider2D bottomCollider; // public����Ƽ���� bottom(ĸ���ݶ��̴� �����) �����ؼ� �˷���(�߹ٴ�)
    public CompositeCollider2D terrainCollider;//(�� �ݶ��̴�)-���̶� ���̶� ����? ���� ���� ������ �� �� ����
                                               // public GameObject bulletPrefab; //�÷��̾ �Ѿ� �� �� �ְ� ���������� �������-> �ٵ� ����Ǯ�� ������ �Ŷ� �� ��


    float vx=0; //����� ����,��� �ӵ��� �����̴� ��
    bool isGrounded; //�����پ����� �ƴ��� ����

    float preVx = 0; //���� �ӵ� �����ߴٰ� �ٱ� ����(�ӵ�0->X) update

    float lastShoot; //�Ѿ� ��Ÿ�� ������ ����- ���������� �Ѿ� �� �ð��� ����

    Vector2 originPositon; // �׾����� (�����ִ� ���)ĳ���� �ٽ� ����ġ�� �÷����� ����

    void Start()
    {
        originPositon = transform.position; //������ġ= ������ ���۵� �� ��ġ!
    }

    public void Restart()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;//-(*)Die()�� ������� ����(�ʱ�ȭ)
        GetComponent<Rigidbody2D>().angularVelocity = 0;                                //-(*)Die() �ʱ�ȭ
       //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);   -(*)Die() �ʱ�ȭ�� �÷�ġ�� �� ����
        GetComponent<BoxCollider2D>().enabled = true;                                   //-(*)Die() �ʱ�ȭ
        transform.eulerAngles = Vector3.zero; //���ư� ������ �����·� ������ ����


        transform.position = originPositon;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; //�������� �ӵ�0���� ������� 
    }

    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal");//GetAxisRaw: ������ȯ�� -1,+1/GetAxis: ������ȯ �ε巴��-���ӵ� ����� ���

        if (vx < 0) //ĳ���� ������ ���� �ִٸ�?->�¿������Ű�� ���� :GetComponent<SpriteRenderer>().flipX ���
        {
            GetComponent<SpriteRenderer>().flipX = true; 
        }
        if (vx > 0) //if-else������ ���ϰ� if�� 2�� ����?  (vx = 0)�϶� ���� ������ �ʱ� ����-> ���� ���� ����
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (bottomCollider.IsTouching(terrainCollider)) //IsTouching �Լ��� �̿��ؼ� ���̶� �� �ݶ��̴� �پ��ִ��� üũ����
        {
            if (!isGrounded) //����
            {
                if (vx == 0)// ����
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else
                {
                    if (vx != 0) // �ӵ�0�ƴ�-> �޸���!
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
            else //��� �ȴ���
            {//���� ������,���ݵ� �پ�����-> �ӵ��� �ٲ�� �ִϸ��̼� �ٲ�
                if (preVx != vx)//�����ӵ� != ����ӵ�
                {
                    if (vx == 0) //�����ӵ� != ����ӵ��ε�, ����ӵ��� 0? => �ٴٰ� ����:Idle
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
            isGrounded = true; //�ٴڿ� �پ������ϱ�
        }
        else
        {
            if (isGrounded)// �������� ���� �پ��־��ٸ�? ����������� �ٲ����
            {
                GetComponent<Animator>().SetTrigger("jump");
            }
               
            isGrounded = false;
        }
            
            if (Input.GetButtonDown("Jump") && isGrounded)//JumpŰ�� ������ ����
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;//Rigidbody2D�� ������������ ���� �ӵ�����
            }
            preVx = vx;

        ////////////////////�Ѿ˹߻�///////////////
        ///
        //- ���������� instantiate �� ���� & �Ѿ� ���⵵ ���� ���� �����̶� ���ƾ� ��
        /* instantiate - ���� ���� �߿� ���� ���� �� �ɸ�
         * -> Object Pooling : Prefab�� �̸� instantiate�� ������ �صΰ�  �����ٰ� �ʿ�� ��
         * :Object Pool�� �̸� instantiate ���� & �����ִ� ��
        */

        if (Input.GetButtonDown("Fire1") && lastShoot+0.5f<Time.time)
        {//��ư�� ������ ���� �Ѿ˹߻�(Fire1:�������� ��ǲ �Ŵ����� left ctrlŰ) && ����ð�(Time.time)�� ���������� �� �ð�+ 0.5�� ���� Ŀ�� ��=> ��Ÿ��0.5s
            Vector2 bulletV = Vector2.zero;

            if (GetComponent<SpriteRenderer>().flipX) //�ٶ󺸴� ���� �� �� ���� ->���̸� ����, flip�̴ϱ�
            {
                bulletV = new Vector2 (-10, 0); //����
            }
            else
            {
                bulletV = new Vector2(10, 0); //������
            }
            
            //GameObject bullet = Instantiate(bulletPrefab);->�� �ɸ���� ����Ǯ�� ���� ����(��ó��)
            GameObject bullet = ObjectPool.Instance.GetBullet();

            bullet.transform.position = transform.position; //�Ѿ��� ��ġ= ����(�÷��̾�) ��ġ
            bullet.GetComponent<Bullet>().velocity = bulletV; //�߻�
            lastShoot = Time.time; //����ð� �Է�
            
            
        }
    }//update

    private void FixedUpdate()//�����̴� ��ü-> FixedUpdate
    {
        transform.Translate(Vector2.right * vx * speed * Time.fixedDeltaTime);
    }

    /////////���̶� ���� ��ȣ�ۿ�////////////
    private void OnCollisionEnter2D(Collision2D collision)
    {//isTriger�� �Ѵ� �� ���������� OnCollisionEnter2D ���
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }//OnColli-

    void Die()
    {
        //[������ �׷��� ����� ����]//
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //.None:freez rotat Ǯ��
        GetComponent<Rigidbody2D>().angularVelocity = 720;//���ۺ��� ���� �Ϸ��� ���ӵ�(angularVelocity) �� & 720->�� �� 2���� ��
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10),ForceMode2D.Impulse); //�������� ������ ����(AddForce:������ ��)
                                                                                      //ForceMode2D.Impulse���� �ߴ� �� ���ϰ� ��

        GetComponent<BoxCollider2D>().enabled = false;//�ݶ��̴��� ���� ���̶� �΋H���� ������ �������� ��
        //+)���̶� �΋H���� ������ �ٽ� �ʱ�ȭ-> ���� restart()�̿� -(*)

        GameManager.Instance.Die();//������ �׸Ŵ������� ���� ��� �˷�����
    }//die

}
