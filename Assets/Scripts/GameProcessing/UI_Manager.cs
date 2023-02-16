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
    public GameObject Begin_Button;
    public GameObject Popup;
    public GameObject Key;
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
        }
        else
        {
            Pause_UI.SetActive(false);
        }
    }

    public void Begin()
    {
        Begin_Button.SetActive(false);
    }

    public void Victory_UI()
    {
        Victory.SetActive(true);
    }

    public void Loss_UI()
    {
        Loss.SetActive(true);
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
    }
}
