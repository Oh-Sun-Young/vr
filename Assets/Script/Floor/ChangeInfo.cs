using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeInfo : MonoBehaviour
{
    private ButtonData buttonData;
    private void Awake()
    {
        buttonData = GetComponent<ButtonData>();
    }

    public void ChangeData()
    {
        int index = buttonData.index;
        Material areaMaterial = buttonData.areaMaterial;

        for (int i = 0; i < AreaInfo.instance.areaList.Count; i++)
        {
            IconEnable(AreaInfo.instance.areaList[i], ((i == index) ? true : false));
        }

        FloorUIManager.instance.PopupEnable(false);

        FloorUIManager.instance.ChangeAreaName(buttonData.areaName);

        if (areaMaterial == null)
        {
            FloorUIManager.instance.BgDataEnable(false);
        }
        else
        {
            FloorUIManager.instance.BgDataEnable(true);
            RenderSettings.skybox = buttonData.areaMaterial;
        }
    }

    private void IconEnable(GameObject obj, bool active)
    {
        obj.transform.GetChild(0).gameObject.SetActive(!active);
        obj.transform.GetChild(1).gameObject.SetActive(active);
    }
}
