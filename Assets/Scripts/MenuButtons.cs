using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour 
{
    public static MenuButtons Instance;

    private bool VrMode;

    void Awake()
    {
        Instance = this;
    }

    public void LoadStage()
    {
        if(VrMode)
        {
            SceneManager.LoadScene(2);
        }
        else if(!VrMode)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void LoadScene(int SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMenu(GameObject Menu)
    {
        Menu.SetActive(true);
    }

    public void CloseMenu(GameObject Menu)
    {
        Menu.SetActive(false);
    }

    public void PlayNavigationSound()
    {
        SoundManager.Instance.NavigationSE();
    }

    public void PlaySelectSound()
    {
        SoundManager.Instance.SelectSE();
    }

    public void ToggleSound()
    {
        if(AudioListener.volume != 0)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void ToggleMode(Text ModeText)
    {
        if (ModeText.text != "VR Mode")
        {
            ModeText.text = "VR Mode";

            VrMode = true;
        }
        else
        {
            ModeText.text = "Third Person Mode";

            VrMode = false;
        }
    }
}
