using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class Data
{
    public enum type
    {
        video,
        img,
        etc,
        none
    }
    public type detailType;

    public string floor;
    public string kr;
    public string en;
    public string point;
    public string desc;
    public VideoClip clip;
    public Sprite[] img;
    public VideoClip[] portfolio;
}
public class FloorInfo : MonoBehaviour
{
    public static FloorInfo instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<FloorInfo>();
            }
            return m_instance;
        }
    }
    private static FloorInfo m_instance;

    public Data[] data;

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}
