using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;
using System;
using System.Reflection;
using Unity.VisualScripting;

/*
 * 참고 사항
 * - [C#]문자열 비교 방법 : https://developer-talk.tistory.com/223
 * - [Unity] DataTime을 사용하여 현재 시간 표시하기 :  https://icat2048.tistory.com/m/445
 */

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }
    private static UIManager m_instance;

    private int index;

    [Header("HUD")]
    [SerializeField] GameObject controlHud = null;
    [SerializeField] GameObject popupHud = null;

    [Header("Floor Button")]
    [SerializeField] Transform tabButtons = null;
    [SerializeField] GameObject tabButtonPrefab = null;

    [Header("Floor Data")]
    [SerializeField] TextMeshProUGUI txt_floor = null;
    [SerializeField] TextMeshProUGUI txt_kr = null;
    [SerializeField] TextMeshProUGUI txt_en = null;
    [SerializeField] TextMeshProUGUI txt_desc = null;
    [SerializeField] VideoPlayer player = null;
    private bool playerCheck = false;
    [SerializeField] Image[] image = null;
    [SerializeField] GameObject bigImage = null;

    [SerializeField] SceneMove btn = null;

    [SerializeField] GameObject floorData = null;
    [SerializeField] GameObject f8Data = null;
    [SerializeField] GameObject dataNone = null;

    [Header("Floor Data : Portfolio")]
    [SerializeField] GameObject portfolio = null;
    [SerializeField] GameObject f8Portfolio = null;

    [Header("Now Time")]
    [SerializeField] TextMeshProUGUI txt_date = null;
    [SerializeField] TextMeshProUGUI txt_time = null;

    [Header("Portfolio")]
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject swiper;
    [SerializeField] Transform swiperContent;
    [SerializeField] GameObject info;
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DataEnabled(false);
        f8Data.SetActive(false);
        bigImage.SetActive(false);
        portfolio.SetActive(false);
        f8Portfolio.SetActive(false);
        PopupEnable(false);

        for(int i = 0; i < FloorInfo.instance.data.Length; i++)
        {
            GameObject obj = Instantiate(tabButtonPrefab, tabButtons);
            obj.transform.Find("Text_Floor").GetComponent<TextMeshProUGUI>().text = FloorInfo.instance.data[i].floor;
            obj.transform.Find("Text_Kr").GetComponent<TextMeshProUGUI>().text = FloorInfo.instance.data[i].kr;
            obj.transform.Find("Text_Eng").GetComponent<TextMeshProUGUI>().text = FloorInfo.instance.data[i].en;
            obj.transform.Find("Line").gameObject.SetActive((i != FloorInfo.instance.data.Length - 1) ? true : false);
        }
    }

    private void Update()
    {
        txt_date.text = DateTime.Now.ToString(("yyyy. MM. dd"));
        txt_time.text = DateTime.Now.ToString(("HH : mm : ss"));
    }

    public void FloorDataUpdate(string floor)
    {
        index = CheckFloor(floor);

        if (index < 0)
        {
            DataEnabled(false);
        }
        else
        {
            DataEnabled(true);
            if (FloorInfo.instance.data[index].detailType != Data.type.etc)
            { 
                txt_floor.text = floor;
                txt_kr.text = FloorInfo.instance.data[index].kr;
                txt_en.text = FloorInfo.instance.data[index].en;
                txt_desc.text = "<b>" + FloorInfo.instance.data[index].point + "</b><br>" + FloorInfo.instance.data[index].desc;

                btn.sceneName = floor;
            }

            InfoEnable(true, index);
            MediaEnable(false);
            f8Data.SetActive(false);
            bigImage.SetActive(false);
            portfolio.SetActive(false);
            f8Portfolio.SetActive(false);

            if (FloorInfo.instance.data[index].detailType == Data.type.etc)
            {
                InfoEnable(false);
                f8Data.SetActive(true);
                f8Portfolio.SetActive(true);
            }
            else if (FloorInfo.instance.data[index].detailType == Data.type.video)
            {
                MediaEnable(true, index);
                portfolio.SetActive(true);
            }
            else if (FloorInfo.instance.data[index].detailType == Data.type.img)
            {
                bigImage.SetActive(true);
                for (int i = 0; i< bigImage.transform.childCount; i++)
                {
                    bigImage.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = FloorInfo.instance.data[index].img[i];
                }
            }
        }
    }

    private void InfoEnable(bool active, int index = -1)
    {
        txt_floor.gameObject.SetActive(active);
        txt_kr.gameObject.SetActive(active);
        txt_en.gameObject.SetActive(active);
        txt_desc.gameObject.SetActive(active);
        btn.gameObject.SetActive(active);
    }
    private void MediaEnable(bool active, int index = -1)
    {
        player.gameObject.SetActive(active);

        for (int i = 0; i < image.Length; i++)
        {
            image[i].gameObject.SetActive(active);
        }

        if (active)
        {
            player.clip = FloorInfo.instance.data[index].clip;

            for (int i = 0; i < image.Length; i++)
            {
                image[i].sprite = FloorInfo.instance.data[index].img[i];
            }
        }

    }

    private void DataEnabled(bool active)
    {
        floorData.SetActive(active);
        dataNone.SetActive(!active);
    }

    private int CheckFloor(string floor)
    {
        for(int i = 0; i < FloorInfo.instance.data.Length ; i++)
        {
            if(string.Equals(floor, FloorInfo.instance.data[i].floor))
            {
                return i;
            }
        }
        return -1;
    }

    public void PopupEnable(bool active)
    {
        controlHud.SetActive(!active);
        popupHud.SetActive(active);
        if (active)
        {
            if (player.isPlaying)
            {
                player.Pause();
                playerCheck = true;
            }
        }
        else
        {
            if (playerCheck)
            {
                player.Play();
            }
        }
        
    }

    public void PopupPortfolio(bool active)
    {
        if (active)
        {

            int length = FloorInfo.instance.data[index].portfolio.Length;
            int cnt = 0;
            if (0 < length)
            {
                SwiperEnable(true);
                for (int i = 0; cnt < 6; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        GameObject obj = Instantiate(prefab, swiperContent);
                        VideoPlayer video = obj.transform.Find("Video").GetComponent<VideoPlayer>();
                        video.clip = FloorInfo.instance.data[index].portfolio[j];
                    }

                    cnt += length;
                }

                FindObjectOfType<Swiper>().Initialization();
            }
            else
            {
                SwiperEnable(false);
            }
        }
        else
        {
            for (int i = 0; i < swiperContent.childCount; i++)
            {
                Destroy(swiperContent.GetChild(i).gameObject);
            }
        }
    }

    private void SwiperEnable(bool active)
    {
        swiper.SetActive(active);
        info.SetActive(!active);
    }
}
