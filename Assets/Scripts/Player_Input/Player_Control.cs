using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public static Player_Control instance;

    public Selected_Object selected;
    public Sell_Object sell;
    public GameObject Roots;
    public GameObject Units;
    public enum Placeable_Object
    {
        Root,
        Lemon,
        Pineapple,
        Apple,
        Corn,
        Watermelon,
        Potato,
        None
    }

    public List<int> placement_costs;

    public SpriteRenderer selected_object_img;
    bool placement_mode;
    public bool sell_mode;
    Placeable_Object selected_object;

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

    // Start is called before the first frame update
    void Start()
    {
        selected_object = Placeable_Object.None;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectObject(Placeable_Object obj)
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

    public void Place()
    {
        if (placement_mode && selected.valid_pos)
        {
            PlaceObject(selected_object);
        }
    }

    public void Sell()
    {
        if (sell.valid_pos)
        {
            SellObject(sell.transform.position);
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
        placement_mode = true;
        GameObject obj = Resources.Load<GameObject>("Prefabs/Placeable/" + selected_object.ToString());
        SpriteRenderer render = obj.GetComponent<SpriteRenderer>();
        selected_object_img.sprite = render.sprite;
        if (selected_object == Placeable_Object.Root)
        {
            selected.Root = true;
        }
        else
        {
            selected.Root = false;
        }
        selected.gameObject.SetActive(true);
    }

    void PlacementOff()
    {
        selected.gameObject.SetActive(false);
        placement_mode = false;
        selected_object = Placeable_Object.None;
    }

    public void SellOn()
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
