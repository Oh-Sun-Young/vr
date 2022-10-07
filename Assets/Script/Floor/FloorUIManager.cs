using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
