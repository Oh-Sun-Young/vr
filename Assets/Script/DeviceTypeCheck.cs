using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ���� ����
 * - ����Ƽ ������ ��ġ�� ����� ���� �˾Ƴ��� ��� [����Ƽ|Unity] : https://bluemeta.tistory.com/m/30
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
