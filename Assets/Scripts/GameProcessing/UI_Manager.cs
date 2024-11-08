using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public TextMeshProUGUI RoundText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI NutrientText;
    public TextMeshProUGUI NutrientRateText;
    public GameObject Pause_UI;
    public GameObject Victory;
    public GameObject Loss;
    public GameObject Intro_UI;
    public GameObject Popup;
    public GameObject Key;
    public GameObject Info_UI;
    bool popup_active;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void UpdateRoundText(int round)
    {
        RoundText.text = "Round " + round.ToString();
    }
    public void UpdateHealthText (int health)
    {
        HealthText.text = health.ToString();
    }
    public void UpdateNutrientText(int nutrients)
    {
        NutrientText.text = nutrients.ToString();
    }
    public void UpdateNutrientRateText(int rate)
    {
        NutrientRateText.text = rate.ToString() + " Per Second";
    }

    public void Pause()
    {
        if (Game_Manager.instance.paused)
        {
            Pause_UI.SetActive(true);
            Menu_Manager.instance.ChangeLayer(0);
            Controller_Manager.instance.UIFocus();
        }
        else
        {
            Pause_UI.SetActive(false);
            if (Game_Manager.instance.started)
            {
                Controller_Manager.instance.GameplayFocus();
            }
        }
    }

    public void Begin()
    {
        Intro_UI.SetActive(false);
        Controller_Manager.instance.GameplayFocus();
    }

    public void Victory_UI()
    {
        Victory.SetActive(true);
        Menu_Manager.instance.ChangeLayer(0);
        Controller_Manager.instance.UIFocus();
    }

    public void Loss_UI()
    {
        Loss.SetActive(true);
        Menu_Manager.instance.ChangeLayer(0);
        Controller_Manager.instance.UIFocus();
    }

    public IEnumerator Popup_Routine()
    {
        popup_active = true;
        Popup.SetActive(true);
        yield return new WaitForSeconds(.75f);
        Popup.SetActive(false);
        popup_active = false;
    }

    public void Popup_UI()
    {
        if (!popup_active)
        {
            StartCoroutine(Popup_Routine());
        }
    }

    public void Toggle_Key()
    {
        Key.SetActive(!Key.activeSelf);
        if (Key.activeSelf)
        {
            Menu_Manager.instance.ChangeLayer(1);
            Controller_Manager.instance.UIFocus();
        }
        else
        {
            Menu_Manager.instance.ChangeLayer(0);
            if (Controller_Manager.instance.KeyButton)
            {
                Controller_Manager.instance.GameplayFocus();
                Controller_Manager.instance.KeyButton = false;
            }
        }
    }

    public void Toggle_Info()
    {
        Info_UI.SetActive(!Info_UI.activeSelf);
        if (Info_UI.activeSelf)
        {
            Menu_Manager.instance.ChangeLayer(1);
        }
        else
        {
            Menu_Manager.instance.ChangeLayer(0);
        }
    }
}
