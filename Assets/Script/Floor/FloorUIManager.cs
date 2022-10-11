using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorUIManager : MonoBehaviour
{
    public static FloorUIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<FloorUIManager>();
            }
            return m_instance;
        }
    }
    private static FloorUIManager m_instance;

    [SerializeField] GameObject controlHud;
    [SerializeField] GameObject popupHud;
    [SerializeField] TextMeshProUGUI text;
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PopupEnable(false);
    }
    public void PopupEnable(bool active)
    {
        controlHud.SetActive(!active);
        popupHud.SetActive(active);
    }

    public void TextArea(string area)
    {
        text.text = area;
    }

    public void ChangeBg(Material bg)
    {
        RenderSettings.skybox = bg;
    }
}
