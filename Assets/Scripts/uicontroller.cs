using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using Image = UnityEngine.UIElements.Image;

public class uicontroller : MonoBehaviour
{
    public CanvasGroup mainmenu, blackoverlay, whiteoverlay, endscreen, loredumptext, younuketext;
    
    void Awake()
    {
        mowscript.God.UI = this;
    }
    
    public void titlefade()
    {
        StartCoroutine(Titlestart());
    }
    public void gameover()
    {
        StartCoroutine(endstart());
    }

    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public IEnumerator Titlestart()
    {
        Debug.Log("called");
        while (blackoverlay.alpha < 1)
        {
            blackoverlay.alpha += 0.05f;
            yield return null;
        }
        while (loredumptext.alpha < 1)
        {
            loredumptext.alpha += 0.25f;
            yield return null;
        }
        yield return new WaitForSeconds(5);
        while (loredumptext.alpha > 0)
        {
            loredumptext.alpha -= 0.25f;
            yield return null;
        }
        mowscript.God.MC.gamestart();
        while (younuketext.alpha < 1)
        {
            younuketext.alpha += 0.25f;
            yield return null;
        }
        yield return new WaitForSeconds(5);
        while (younuketext.alpha > 0)
        {
            younuketext.alpha -= 0.25f;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        mainmenu.alpha = 0;
        while (blackoverlay.alpha > 0)
        {
            blackoverlay.alpha -= 0.1f;
            yield return null;
        }
        mowscript.God.PC.gstart();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public IEnumerator endstart()
    {
        while (whiteoverlay.alpha < 1)
        {
            whiteoverlay.alpha += 0.05f;
            yield return null;
        }
        mowscript.God.PC.gend();
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return new WaitForSeconds(3);
        while (endscreen.alpha < 1)
        {
            endscreen.alpha += 0.25f;
            yield return null;
        }
    }
}
