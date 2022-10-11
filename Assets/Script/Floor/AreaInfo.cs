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
    public Material areaMaterial;
    public float x;
    public float y;
}
public class AreaInfo : MonoBehaviour
{
    public static AreaInfo instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<AreaInfo>();
            }
            return m_instance;
        }
    }
    private static AreaInfo m_instance;

    [SerializeField] TextMeshProUGUI textFloor;
    [SerializeField] TextMeshProUGUI textArea;
    [SerializeField] string textFloorInfo;
    [SerializeField] Sprite mapFloor;

    [SerializeField] GameObject prefab;
    [SerializeField] Transform map;
    [SerializeField] AreaData[] areaData;

    public List<GameObject> buttons;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

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
            buttons.Add(obj);

            ButtonData buttonData = obj.GetComponent<ButtonData>();
            buttonData.index = i;
            buttonData.areaName = areaData[i].areaName;
            if (areaData[i].areaMaterial != null) 
            {
                buttonData.areaMaterial = areaData[i].areaMaterial;
            }

            if (areaData[i].areaNameButton == string.Empty)
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
                obj.transform.GetChild(0).gameObject.SetActive(false);
                obj.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void ChangeAreaName(string name)
    {
        textArea.text = name;
    }
}
