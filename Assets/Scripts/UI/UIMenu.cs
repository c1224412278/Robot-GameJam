using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UIMenu : MonoBehaviour
{
    private delegate void _deleg_Event();
    private _deleg_Event deleg_Event;
    private UIManager UIManagerScript;

    [SerializeField] private Text Text_Title;
    [SerializeField] private Text Text_InputAny;
    [SerializeField] private Canvas Canvas_Background;
    [SerializeField] private RectTransform Rect_MenuButtons;
    private void Start()
    {
        deleg_Event = Fn_StartGame;
        Text_InputAny.enabled = true;
        Canvas_Background.enabled = false;
        Rect_MenuButtons.gameObject.SetActive(false);

        if (UIManagerScript == null)
        {
            UIManagerScript = GameSystem.Instance.gameObject.GetComponent<UIManager>();
        }
    }
    private void Update()
    {
        if (deleg_Event != null)
            deleg_Event();
    }
    private void Fn_StartGame()
    {
        if (Input.anyKeyDown)
        {
            deleg_Event = Fn_MainMenu;
            Text_InputAny.enabled = false;
            Rect_MenuButtons.gameObject.SetActive(true);
        }
    }
    private void Fn_MainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            deleg_Event = Fn_StartGame;
            Text_InputAny.enabled = true;
            Rect_MenuButtons.gameObject.SetActive(false);
        }
    }

    // button event
    public void Fn_PlayGame()
    {
        Canvas_Background.enabled = true;
        Image Img_Blockground = Canvas_Background.transform.GetChild(0).GetComponent<Image>();
        Button[] Btns_Menu = Rect_MenuButtons.GetComponentsInChildren<Button>();
        for (int i = 0; i < Btns_Menu.Length; i ++)
        {
            Btns_Menu[i].interactable = false;
        }
        DOTween.Sequence().Append(UIManagerScript.Fn_SetImageAlpha(Img_Blockground , 1f , 1.25f))
            .OnComplete(() => 
            {
                Debug.Log("Switch Scene.");
                SceneManager.LoadScene("Scenes/Loading");
            });
    }
    public void Fn_ExitGame()
    {
        Canvas_Background.enabled = true;
        Image Img_Blockground = Canvas_Background.transform.GetChild(0).GetComponent<Image>();
        Button[] Btns_Menu = Rect_MenuButtons.GetComponentsInChildren<Button>();
        for (int i = 0; i < Btns_Menu.Length; i++)
        {
            Btns_Menu[i].interactable = false;
        }

        DOTween.Sequence().Append(UIManagerScript.Fn_SetImageAlpha(Img_Blockground, 1f, 1.25f))
            .OnComplete(() =>
            {
                Application.Quit();
            });
    }
}
