using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;
using UnityEngine.XR.ARSubsystems;

public class Swiper : MonoBehaviour
{
    public float test;

    public int prev_; // 이전의 이전
    public int prev; // 이전
    public int now = 0; // 현재
    public int next; // 다음
    public int next_; // 다음의 다음
    public int all; // 전체

    private float width;

    private float speed = 1500;

    // 위치
    private Vector3 nowPosition;
    private Vector3 prevPosition;
    private Vector3 prevPosition_;
    private Vector3 nextPosition;
    private Vector3 nextPosition_;

    // 크기
    private Vector3 realSize = new Vector3(1280, 680, 1);
    private Vector3 minSize = new Vector3(0.75f, 0.75f, 0.75f);
    private Vector3 maxSize = new Vector3(1, 1, 1);
    private Vector3 changeSize = new Vector3(0.0016f, 0.0016f, 0.0016f);

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

    private void Awake()
    {
        width = transform.GetComponent<BoxCollider2D>().size.x;

        nowPosition = new Vector3(transform.localPosition.x, 0, 0);
        prevPosition = new Vector3(transform.localPosition.x - width, 0, 0);
        prevPosition_ = new Vector3(transform.localPosition.x - width * 2, 0, 0);
        nextPosition = new Vector3(transform.localPosition.x + width, 0, 0);
        nextPosition_ = new Vector3(transform.localPosition.x + width * 2, 0, 0);
    }

    public void Initialization()
    {
        now = 0;
        all = transform.childCount;

        if(0 < all)
        {
            ItemEnable();

            // 위치
            transform.GetChild(prev_).localPosition = prevPosition_;
            transform.GetChild(prev).localPosition = prevPosition;
            transform.GetChild(now).localPosition = nowPosition;
            transform.GetChild(next).localPosition = nextPosition;
            transform.GetChild(next_).localPosition = nextPosition_;

            for (int i = 0; i < all; i++)
            {
                transform.GetChild(i).Find("Video").transform.localScale = ((i == now) ? maxSize : minSize);
            }
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

        transform.GetChild(prev_).gameObject.SetActive(false);
        transform.GetChild(prev).localPosition = prevPosition_;
        transform.GetChild(now).localPosition = prevPosition;
        transform.GetChild(next).localPosition = nowPosition;
        transform.GetChild(next_).localPosition = nextPosition;

        StartCoroutine("SwiperMove");
        StartCoroutine("SwiperScale");
    }
    public void NextButton()
    {
        now = (all <= ++now) ? 0 : now;
        ItemEnable();

        // 이동 전 위치
        transform.GetChild(prev_).localPosition = prevPosition;
        transform.GetChild(prev).localPosition = nowPosition;
        transform.GetChild(now).localPosition = nextPosition;
        transform.GetChild(next).localPosition = nextPosition_;
        transform.GetChild(next_).gameObject.SetActive(false);

        StartCoroutine("SwiperMove");
        StartCoroutine("SwiperScale");
    }

    private void MoveTowards(Transform transform, Vector3 destination)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, Time.deltaTime * speed);
    }

    IEnumerator SwiperMove()
    {

        while (0 != Vector3.Distance(transform.GetChild(now).localPosition, nowPosition))
        {

            /*if(transform.GetChild(prev_).gameObject.activeInHierarchy) MoveTowards(transform.GetChild(prev_), prevPosition_);

            if (0.01f < Vector3.Distance(nowTransform.localScale, maxSize))
            {
                nowTransform.localScale += changeSize;
                prevTransform.localScale -= changeSize;
                nextTransform.localScale -= changeSize;
            }

            if (transform.GetChild(prev_).gameObject.activeInHierarchy) MoveTowards(transform.GetChild(prev_), prevPosition_);

            MoveTowards(transform.GetChild(prev), prevPosition);
            MoveTowards(transform.GetChild(now), nowPosition);
            MoveTowards(transform.GetChild(next), nextPosition);
            if (transform.GetChild(next_).gameObject.activeInHierarchy) MoveTowards(transform.GetChild(next_), nextPosition_);*/

            yield return null;
        }

        if (transform.GetChild(prev_).gameObject.activeInHierarchy)
        {
            transform.GetChild(prev_).gameObject.SetActive(false);
        }
        if (transform.GetChild(next_).gameObject.activeInHierarchy)
        {
            transform.GetChild(next_).gameObject.SetActive(false);
        }

        VideoPlayer video = transform.GetChild(now).Find("Video").GetComponent<VideoPlayer>();
        video.Stop();
        video.Play();
    }

    IEnumerator SwiperScale()
    {
        Transform prevTransform = transform.GetChild(prev).Find("Video");
        Transform nowTransform = transform.GetChild(now).Find("Video");
        Transform nextTransform = transform.GetChild(next).Find("Video");

        while (0.01f < Vector3.Distance(nowTransform.localScale, maxSize))
        {
            nowTransform.localScale += changeSize;
            prevTransform.localScale -= changeSize;
            nextTransform.localScale -= changeSize;
            yield return null;
        }

        nowTransform.localScale = maxSize;
        prevTransform.localScale = minSize;
        nextTransform.localScale = minSize;

    }
}
