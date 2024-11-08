using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit_UI : MonoBehaviour
{
    public GameObject Info;
    public Player_Control.Placeable_Object unit_object;

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            Info.SetActive(true);
        }
        else
        {
            Info.SetActive(false);
        }
    }

    public void Select()
    {
        if (!Game_Manager.instance.paused)
        {
            Player_Control.instance.SelectObject(unit_object);
            UI_Manager.instance.Toggle_Key();
        }
    }
}
