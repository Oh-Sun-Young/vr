using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 참고
 * - 유니티 스카이박스 ( Skybox ) : https://notyu.tistory.com/40
 */
public class SceneSkyboxSetting : MonoBehaviour
{
    // 스카이박스 머티리얼
    public Material skyboxMaterial;

    private void Start()
    {
        // 머티리얼 등록
        RenderSettings.skybox = skyboxMaterial;
    }
}
