using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public Selected_Object selected;
    public Sell_Object sell;
    public GameObject Roots;
    public GameObject Units;
    enum Placeable_Object
    {
        Root,
        Lemon,
        Apple,
        Pineapple,
        Watermelon,
        Corn,
        Potato,
        None
    }

    public List<int> placement_costs;

    public SpriteRenderer selected_object_img;
    bool placement_mode;
    bool sell_mode;
    Placeable_Object selected_object;
    // Start is called before the first frame update
    void Start()
    {
        selected_object = Placeable_Object.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Game_Manager.instance.Pause_Unpause();
        }
        if (!Game_Manager.instance.paused)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SelectObject(Placeable_Object.Root);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                SelectObject(Placeable_Object.Lemon);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SelectObject(Placeable_Object.Watermelon);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                SelectObject(Placeable_Object.Potato);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SellOn();
            }
            if (placement_mode)
            {
                if (Input.GetMouseButtonDown(0) && selected.valid_pos)
                {
                    PlaceObject(selected_object);
                }
            }
            if (sell_mode)
            {
                if (Input.GetMouseButtonDown(0) && sell.valid_pos)
                {
                    SellObject(sell.transform.position);
                }
            }
        }
    }

    void SelectObject(Placeable_Object obj)
    {
        if (selected_object == obj)
        {
            PlacementOff();
        }
        else
        {
            selected_object = obj;
            PlacementOn();
        }
    }

    void PlaceObject(Placeable_Object obj)
    {
        bool is_root = false;
        if (obj == Placeable_Object.Root)
        {
            is_root = true;
        }
        int cost = placement_costs[(int)obj];
        if (cost <= Game_Manager.instance.nutrients)
        {
            GameObject gameObject = Resources.Load<GameObject>("Prefabs/Placeable/" + obj.ToString());
            if (is_root)
            {
                Instantiate(gameObject, Map_Manager.instance.NearestNodePos(selected.gameObject.transform.position), Quaternion.identity, Roots.transform);
            }
            else
            {
                Instantiate(gameObject, Map_Manager.instance.NearestNodePos(selected.gameObject.transform.position), Quaternion.identity, Units.transform);
            }
            Map_Manager.instance.UpdateNode(selected.gameObject.transform.position, is_root, false);
            cost = cost * -1;
            Game_Manager.instance.UpdateNutrients(cost);
            PlacementOff();
        }
        else
        {
           UI_Manager.instance.Popup_UI();
        }
    }

    void SellObject(Vector3 pos)
    {
        int val = Map_Manager.instance.GetValue(sell.gameObject.transform.position, sell.Unit);
        Game_Manager.instance.UpdateNutrients(val);
        Map_Manager.instance.UpdateNode(sell.gameObject.transform.position, !sell.Unit, true);
        SellOff();
    }

    void PlacementOn()
    {
        SellOff();
        selected.gameObject.SetActive(true);
        placement_mode = true;
        selected_object_img.sprite = Resources.Load<Sprite>("Placeable/" + selected_object.ToString());
        if (selected_object == Placeable_Object.Root)
        {
            selected.Root = true;
        }
        else
        {
            selected.Root = false;
        }
    }

    void PlacementOff()
    {
        selected.gameObject.SetActive(false);
        placement_mode = false;
        selected_object = Placeable_Object.None;
    }

    void SellOn()
    {
        PlacementOff();
        sell_mode = true;
        sell.gameObject.SetActive(true);
    }

    void SellOff()
    {
        sell_mode = false;
        sell.gameObject.SetActive(false);
    }
}
