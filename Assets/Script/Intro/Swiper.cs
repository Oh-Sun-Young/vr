using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;
using UnityEngine.XR.ARSubsystems;
/*
 * ����
 * - How to make swipe snap menu in Unity3d : https://www.youtube.com/watch?v=xBxvqxsxdR8
 */

public class Swiper : MonoBehaviour
{
    public int prev_; // ������ ����
    public int prev; // ����
    public int now = 0; // ����
    public int next; // ����
    public int next_; // ������ ����
    public int all; // ��ü

    // ����
    [Header("���� Texture")]
    [SerializeField] RenderTexture nowTexture;
    [SerializeField] RenderTexture prevTexture;
    [SerializeField] RenderTexture prevTexture_;
    [SerializeField] RenderTexture nextTexture;
    [SerializeField] RenderTexture nextTexture_;

    [Header("���� Material")]
    [SerializeField] Material nowMaterial;
    [SerializeField] Material prevMaterial;
    [SerializeField] Material prevMaterial_;
    [SerializeField] Material nextMaterial;
    [SerializeField] Material nextMaterial_;

    public void Initialization()
    {
        now = 0;
        all = transform.childCount;

        if(0 < all)
        {
            ItemEnable();
        }
    }

    private void ItemEnable()
    {
        prev = (1 <= now ? now - 1 : all - 1);
        prev_ = (1 <= prev ? prev - 1 : all - 1);
        next = (now < all - 1 ? now + 1 : 0);
        next_ = (next < all - 1 ? next + 1 : 0);

        for (int i = 0; i < all; i++)
        {

            GameObject video = transform.GetChild(i).Find("Video").gameObject;

            if (i == now) VideoInfo(video, nowMaterial, nowTexture);
            else if (i == prev) VideoInfo(video, prevMaterial, prevTexture);
            else if (i == prev_) VideoInfo(video, prevMaterial_, prevTexture_);
            else if (i == next) VideoInfo(video, nextMaterial, nextTexture);
            else if (i == next_) VideoInfo(video, nextMaterial_, nextTexture_);

            video.GetComponent<VideoPlayer>().SetDirectAudioMute(0, (i == now) ? false : true);

            transform.GetChild(i).gameObject.SetActive((i == now || i == prev || i == next || i == prev_ || i == next_) ? true : false);
        }
    }

    private void VideoInfo(GameObject video, Material material, RenderTexture texture)
    {
        RawImage theRawImage = video.GetComponent<RawImage>();
        VideoPlayer theVideoPlayer = video.GetComponent<VideoPlayer>();

        theRawImage.material = material;
        theVideoPlayer.targetTexture = texture;
    }

    public void PrevButton()
    {
        now = (--now < 0) ? all - 1 : now;
        ItemEnable();
    }
    public void NextButton()
    {
        now = (all <= ++now) ? 0 : now;
        ItemEnable();
    }
}
