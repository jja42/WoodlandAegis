using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    public GameObject Tutorial;
    public void OpenTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        Tutorial.SetActive(false);
    }
}
