using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{//Enemy_Script ����: �� ������ HP�𿩾� ��, �����̴� �׷��� ����

    public int hp = 3;
    public float speed = 3;//�ν����Ϳ��� �ӵ������� �� �ֵ���
    Vector2 vx; //���� �̵��ӵ� �� �� �ְ�!

    public Collider2D frontBottomCollider; //fronrBottomCollider�� ������ �ִ��� Ȯ��
    public CompositeCollider2D terrainCollider;// ���� Ȯ��
    public Collider2D frontCollider;//���� ���� ���� �ִ��� Ȯ��

   void Start()
    {
        vx = Vector2.right*speed;//���۽ÿ� ���������� �̵��ϰԲ�!
    }

    void Update()
    { 
        if (frontCollider.IsTouching(terrainCollider) || !frontBottomCollider.IsTouching(terrainCollider))
        {//frontBottomCollider�� ���� ��� ������(=����)
            vx = -vx; //�����̴� ���� ����
            //+)ǥ�õǴ� ���� �ٲٴ� �� - 1) spriteRenderer�� flipx�ٲ�:�ٵ� frntBttCol-�ڽ� �ݶ��̴�-> �θ���� �ٲܰ���
            transform.localScale = new Vector2(-transform.localScale.x, 1);//���� Ʈ������x�� �ݴ�� �ٲ�
        }
    }

    private void FixedUpdate()
    {//��ġ �̵��ϴϱ� �Ƚ����������
        transform.Translate(vx*Time.fixedDeltaTime);
    }

    public void Hit(int damage)
    {// ���� �� �¾��� �� -> Bullet Script���� �ҷ��� ���
        hp -= damage;
        if (hp == 0) 
        { //�� ���� ����-> ĳ���� �׾��� ���� ����� �׷��� �����
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //������ ȸ�� ��
            GetComponent<Rigidbody2D>().angularVelocity = 720;//���� ���� ���ӵ� ��
            GetComponent<Collider2D>().enabled = false;//�ݶ��̴� ���� (Collider2D���� �ڽ�,��Ŭ..���� ���ԵǴϱ� )
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,10), ForceMode2D.Impulse);//���� �� ����!

            Invoke("DestroyThis", 1.0f);
        }
    }//hit

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
