using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloorDetail : MonoBehaviour
{
    // ������ ���� �� ���� �� ���������� Ȱ��ȭ
    public void ViewDetail()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        string floor = clickObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

        UIManager.instance.FloorDataUpdate(floor);
    }
}
