using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller_Manager : MonoBehaviour
{
    public static Controller_Manager instance;

    public PlayerInput input;
    public bool KeyButton;
    private void Awake()
    {
        //Standard Singleton Setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnPause()
    {
        if (!Game_Manager.instance.started)
        {
            Game_Manager.instance.Begin();
            return;
        }
        Game_Manager.instance.Pause_Unpause();
    }

    void OnMove(InputValue value)
    {
        if (!Game_Manager.instance.paused)
        {
            Vector2 inputvector = value.Get<Vector2>();
            inputvector = new Vector2(Mathf.RoundToInt(inputvector.x), Mathf.RoundToInt(inputvector.y));

            transform.position += new Vector3(inputvector.x, inputvector.y, 0);
            transform.position = ClampPosition(transform.position);
        }
    }

    Vector3 ClampPosition(Vector3 pos)
    {
        float x = pos.x;
        float y = pos.y;

        x = Mathf.Clamp(x, -12, 11);
        y = Mathf.Clamp(y, -6, 7);

        Vector3 new_pos = new Vector3(x, y, 0);
        return new_pos;
    }

    public void GameplayFocus()
    {
        input.currentActionMap.Disable();
        input.SwitchCurrentActionMap("Gameplay");
        input.currentActionMap.Enable();
    }

    public void UIFocus()
    {
        input.currentActionMap.Disable();
        input.SwitchCurrentActionMap("UI");
        input.currentActionMap.Enable();
    }

    void OnRoots()
    {
        if (!Game_Manager.instance.paused)
        {
            Player_Control.instance.SelectObject(Player_Control.Placeable_Object.Root);
        }
    }

    void OnPlace()
    {
        if (!Game_Manager.instance.paused)
        {
            if (!Player_Control.instance.sell_mode)
            {
                Player_Control.instance.Place();
            }
            else
            {
                Player_Control.instance.Sell();
            }
        }
    }

    void OnSell()
    {
        if (!Game_Manager.instance.paused)
        {
            Player_Control.instance.SellOn();
        }
    }

    void OnUnits()
    {
        if (!Game_Manager.instance.paused)
        {
            UI_Manager.instance.Toggle_Key();
            KeyButton = true;
        }
    }
}
