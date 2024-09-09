using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)//EndPoint->isTrigger라서 OnTriggerEnter2D 사용
    {//트리거 세팅된 콜라이더에 다른 옵젝 들어오면 이벤트 발생, (Collider2D collision)는 부딫힌 상대방 뜻함
        if(collision.tag == "Player") //부딫힌 콜라이더의 태그가 플레이어? 플레이어가 도착지점까지 왔다는 뜻
        {
            GameManager.Instance.StageClear();
        }
    }
}
