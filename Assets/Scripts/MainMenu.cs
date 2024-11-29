using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject gameMode;

    private void Start()
    {
        Invoke(nameof(OnPlay), 1f);
    }

    private void OnPlay() => gameMode.SetActive(true);

    public void OnQuit() => Application.Quit();
}
