using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{//총알 발사 관리

    public static ObjectPool Instance; //-1)
    public GameObject bulletPrefab;
    public int bulletLimit = 30; //총알이 화면에 몇개까지 쏠 지 저장

    List<GameObject> bullets;//총알을 리스트로 관리할 거고, 밖으로 드러날 필요 없음

    private void Awake()//-1): 다른 곳에서 ObjectPool을 쉽게 쓸 수 있음
    {
        Instance = this;
    }

    void Start()
    {
        bullets = new List<GameObject>();
        for(int i = 0; i < bulletLimit; i++)
        {
            GameObject go = Instantiate(bulletPrefab,transform);
                                                    //ObjectPool을 자식Object로 만들고 싶음-> 정렬위함
                        //transform: 자기 자신의 위치-> bulletPrefab이 inst되며 transform 위치 자식 옵젝으로!
            go.SetActive(false);//꺼둠
            bullets.Add(go);// 생성한 총알을 리스트에 추가
        }
    }//Start

    public GameObject GetBullet() //밖에서 총알을 부르기 위함: 미리 만든 것중에 하나만 줘!
    {
        foreach (GameObject go in bullets)//bullets에 있는 겜옵 중에서 현재 액티브X인걸 줘야 함
        {   
            if (!go.activeSelf)
            {//activeSelf: 현재 active된 상태 = 살아있는 상태, !go.activeSelf: 현재 액티브X 옵젝이 if 안으로!
                go.SetActive(true); //플레이어에서 활성화 시킬거니까 켜서 줌
                return go; //액티브X 옵젝 나타나면 리턴함
            }
                
        }
        return null; //활성화된 옵젝 못 찾거나, 총알30개 전부 썼다-> 더 이상 안나가게 하는 방식

        /* 총알30개 전부 썼다-> 새로운 옵젝 만들어서 총알 리스트를 더 만들어-> bulletPool을 넓힘!
          GameObject obj = Instantiate(bulletPrefab,transform);
          bullets.Add(obj);
          return obj;
        */
    }
}
