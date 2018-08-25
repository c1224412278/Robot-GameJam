using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Fn_LoadSwitchScene(2.5f));
    }
    private IEnumerator Fn_LoadSwitchScene(float time)            //等待轉場
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Scenes/Game_1");
    }
}
