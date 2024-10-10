using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject nextButtons;
    [SerializeField] private GameObject playButton;
    [SerializeField] private List<GameObject> buttons;
    private static readonly int ShowButtons = Animator.StringToHash("ShowButtons");

    public void OnPlay()
    {
        playButton.SetActive(false);
        anim.SetBool(ShowButtons,true);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
