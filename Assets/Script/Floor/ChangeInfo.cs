using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeInfo : MonoBehaviour
{
    private ButtonData buttonData;

    private void Start()
    {
        buttonData = transform.GetComponent<ButtonData>();
    }

    public void ChangeData()
    {
        for (int i=0; i<AreaInfo.instance.buttons.Count; i++)
        {
            IconEnable(AreaInfo.instance.buttons[i], ((i == buttonData.index) ? true : false));
        }
        FloorUIManager.instance.PopupEnable(false);
        FloorUIManager.instance.TextArea(buttonData.areaName);
        FloorUIManager.instance.ChangeBg(buttonData.areaMaterial);
    }

    private void IconEnable(GameObject obj, bool action)
    {
        obj.transform.GetChild(0).gameObject.SetActive(!action);
        obj.transform.GetChild(1).gameObject.SetActive(action);
    }

    
}
