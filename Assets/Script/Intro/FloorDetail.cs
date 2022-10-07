using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloorDetail : MonoBehaviour
{
    // 선택한 층의 상세 정보 및 포토폴리오 활성화
    public void ViewDetail()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        string floor = clickObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

        UIManager.instance.FloorDataUpdate(floor);
    }
}
