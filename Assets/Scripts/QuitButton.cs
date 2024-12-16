using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuitButton : MonoBehaviour
{
    public void ProszeWyjdz ()
    {
        Debug.Log("Quit");
        Application.Quit ();
    }
}
