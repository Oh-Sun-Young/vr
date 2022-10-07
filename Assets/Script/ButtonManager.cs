using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
/* 참고
* 방금 클릭한 UI 이름, 정보 가져오기 https://yoonstone-games.tistory.com/70
* 유니티 현재씬 확인 : https://chameleonstudio.tistory.com/59
*/

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<ButtonManager>();
            }
            return m_instance;
        }
    }
    private static ButtonManager m_instance;
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 포트폴리오 팝업창 활성화
    public void PortfolioPopupEnable(bool active)
    {
            UIManager.instance.PopupEnable(active);
            UIManager.instance.PopupPortfolio(active);
    }

    // 지도 팝업창 활성화
    public void MapPopupEnable(bool active)
    {
        FloorUIManager.instance.PopupEnable(active);
    }

    // Scene 이동
    public void SceneMove(string name)
    {
        SceneManager.LoadScene(name);
    }

    // URL 이동
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    // 앱 종료
    public void AppQuit()
    {
        Application.Quit();
    }

}
