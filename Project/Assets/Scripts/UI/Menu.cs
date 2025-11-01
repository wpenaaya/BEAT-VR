using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Valve.VR;

[Serializable]
public class UIUpdate : UnityEvent<string> { }

public class Menu : MonoBehaviour
{
    public GameObject menuCanvas, mainMenu, songsListMenu, gameScreen;

    public GameObject mainMenuFirstSelected, songsListFirstSelected;

    private bool isMenuOpen = true;

    public UnityEvent fogClose, fogFar;

    public void OpenMenu()
    {
        mainMenu.SetActive(true);
        songsListMenu.SetActive(false);
        gameScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
        fogClose.Invoke();
        isMenuOpen = true;
    }

    public void CloseMenu()
    {
        mainMenu.SetActive(false);
        songsListMenu.SetActive(false);
        gameScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        fogFar.Invoke();
        isMenuOpen = false;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
