using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float timeAdd = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") //프룻이랑 플레이어 충돌시 겜메니저에게 알려줌
        {
            GameManager.Instance.AddTime(timeAdd); //timeAdd만큼 올려줘
            GetComponent<Animator>().SetTrigger("Eaten"); //애니메이션 재생
            Invoke("DestroyThis", 0.3f);
        }
    }//OnTriggerEnter

    void DestroyThis() //6프레임 20초 재생 -> 0.3 동안 애니메이션 재생후 이 함수 불러서 옵젝 없앰
    {
        Destroy(gameObject);//Destroy(gameObject); //먹은 아이템 사라지게 할 수 있음
    }
}
