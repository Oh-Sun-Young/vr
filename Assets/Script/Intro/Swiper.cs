using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;
using UnityEngine.XR.ARSubsystems;
/*
 * 참고
 * - How to make swipe snap menu in Unity3d : https://www.youtube.com/watch?v=xBxvqxsxdR8
 */

public class Swiper : MonoBehaviour
{
    public int prev_; // 이전의 이전
    public int prev; // 이전
    public int now = 0; // 현재
    public int next; // 다음
    public int next_; // 다음의 다음
    public int all; // 전체

    // 영상
    [Header("영상 Texture")]
    [SerializeField] RenderTexture nowTexture;
    [SerializeField] RenderTexture prevTexture;
    [SerializeField] RenderTexture prevTexture_;
    [SerializeField] RenderTexture nextTexture;
    [SerializeField] RenderTexture nextTexture_;

    [Header("영상 Material")]
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

            transform.GetChild(i).gameObject.SetActive((i == now) ? true : false);
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
