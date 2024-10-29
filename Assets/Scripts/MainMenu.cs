using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject nextButtons;

    private void Start()
    {
        Invoke(nameof(OnPlay), 1f);
    }

    private void OnPlay()
    {
        nextButtons.SetActive(true);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("LVL 1");
    }
    
    public void LoadSecoundLevel()
    {
        SceneManager.LoadScene("LVL 2");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
