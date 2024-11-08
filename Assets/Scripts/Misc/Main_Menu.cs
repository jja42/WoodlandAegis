using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    public GameObject Tutorial;
    public GameObject Key;
    public void OpenTutorial()
    {
        Tutorial.SetActive(true);
        Menu_Manager.instance.ChangeLayer(1);
    }

    public void CloseTutorial()
    {
        Tutorial.SetActive(false);
        Menu_Manager.instance.ChangeLayer(0);
    }

    public void OpenKey()
    {
        Key.SetActive(true);
        Menu_Manager.instance.ChangeLayer(1);
    }

    public void CloseKey()
    {
        Key.SetActive(false);
        Menu_Manager.instance.ChangeLayer(0);
    }
}
