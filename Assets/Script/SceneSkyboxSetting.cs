using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ����
 * - ����Ƽ ��ī�̹ڽ� ( Skybox ) : https://notyu.tistory.com/40
 */
public class SceneSkyboxSetting : MonoBehaviour
{
    // ��ī�̹ڽ� ��Ƽ����
    public Material skyboxMaterial;

    private void Start()
    {
        // ��Ƽ���� ���
        RenderSettings.skybox = skyboxMaterial;
    }
}
