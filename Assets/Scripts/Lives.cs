using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public List<GameObject> lifeImages;
    public void SetLives(int life)
    {
        foreach (GameObject obj in lifeImages) // 현재 라이프이미지 전부 불러옴
        {
            obj.SetActive(false);//현재 라이프이미지 전부 꺼
        }

        for (int i = 0; i < life; i++) 
        { 
            lifeImages[i].SetActive(true);//목숨개수만큼만 액티브 켜줌
        }  

    }


}
