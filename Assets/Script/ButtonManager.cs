using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
/* ����
* ��� Ŭ���� UI �̸�, ���� �������� https://yoonstone-games.tistory.com/70
* ����Ƽ ����� Ȯ�� : https://chameleonstudio.tistory.com/59
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

    // ��Ʈ������ �˾�â Ȱ��ȭ
    public void PortfolioPopupEnable(bool active)
    {
            UIManager.instance.PopupEnable(active);
            UIManager.instance.PopupPortfolio(active);
    }

    // ���� �˾�â Ȱ��ȭ
    public void MapPopupEnable(bool active)
    {
        FloorUIManager.instance.PopupEnable(active);
    }

    // Scene �̵�
    public void SceneMove(string name)
    {
        SceneManager.LoadScene(name);
    }

    // URL �̵�
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    // �� ����
    public void AppQuit()
    {
        Application.Quit();
    }

}
