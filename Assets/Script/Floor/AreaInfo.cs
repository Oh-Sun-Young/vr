using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
class AreaData
{
    public string areaName;
    public string areaNameButton;
    public VideoClip videoClip;
    public float x;
    public float y;
}
public class AreaInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textFloor;
    [SerializeField] TextMeshProUGUI textArea;
    [SerializeField] string textFloorInfo;
    [SerializeField] Sprite mapFloor;

    [SerializeField] GameObject prefab;
    [SerializeField] Transform map;
    [SerializeField] AreaData[] areaData;

    private void Awake()
    {
        textFloor.text = textFloorInfo;

        if (mapFloor != null)
        {
            map.gameObject.GetComponent<Image>().sprite = mapFloor;
        }
    }
    private void Start()
    {
        for (int i = 0; i < areaData.Length; i++)
        {
            GameObject obj = Instantiate(prefab, map);
            obj.transform.localPosition = new Vector2(areaData[i].x, areaData[i].y);

            ButtonData buttonData = obj.GetComponent<ButtonData>();
            buttonData.areaName = areaData[i].areaName;
            if (areaData[i].videoClip != null) 
            {
                buttonData.videoClip = areaData[i].videoClip;
            }

            if(areaData[i].areaNameButton == string.Empty)
            {
                obj.GetComponentInChildren<TextMeshProUGUI>().text = areaData[i].areaName;
            }
            else
            {
                obj.GetComponentInChildren<TextMeshProUGUI>().text = areaData[i].areaNameButton;
            }

            // 첫 진입시
            if (i == 0)
            {
                ChangeAreaName(areaData[i].areaName);
            }
        }
    }

    public void ChangeAreaName(string name)
    {
        textArea.text = name;
    }
}
