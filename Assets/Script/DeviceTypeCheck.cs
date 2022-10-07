using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 참고 사항
 * - 유니티 게임이 설치된 기기의 정보 알아내는 방법 [유니티|Unity] : https://bluemeta.tistory.com/m/30
 */
public class DeviceTypeCheck : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Start()
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive((SystemInfo.deviceType == DeviceType.Console) ? false : true);
        }
    }
}
