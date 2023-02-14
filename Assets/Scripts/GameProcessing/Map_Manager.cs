using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour
{
    public static Map_Manager instance;
    int path_layerMask;
    int root_layerMask;
    int unit_layerMask;
    int nutrients_layerMask;
    int max_x = 24;
    int max_y = 14;
    public Node[,] grid;
    public Vector3 start_pos;
    public Vector3 end_pos;
    List<Vector3> path;
    public int nutrients_to_spawn;
    public bool random_nutrient_spawn;

    private void Awake()
    {
        instance = this;
        path_layerMask = LayerMask.GetMask("Path");
        nutrients_layerMask = LayerMask.GetMask("Nutrients");
        root_layerMask = LayerMask.GetMask("Roots");
        unit_layerMask = LayerMask.GetMask("Units");
        grid = new Node[max_x, max_y];
        for (int i = 0; i < max_x; i++)
        {
            for (int j = 0; j < max_y; j++)
            {
                grid[i, j] = new Node(new Vector3(i - (max_x / 2), (j + 1) - (max_y / 2)), i, j);
            }
        }
    }

    private void Start()
    {
        path = GetPath(start_pos, end_pos);
        if (random_nutrient_spawn)
        {
            SpawnNutrients();
        }
    }
    public class Node
    {
        public bool isPath;
        public bool isRoot;
        public bool isNutrient;
        public bool hasUnit;
        public float h_cost;
        public float g_cost;
        public float f_cost;
        public Vector3 pos;
        public Node parent;
        public int x;
        public int y;
        public int value;
        public List<Node> Neighbors { get; set; }
        public Node(Vector3 position, int x_index, int y_index)
        {
            isPath = Physics2D.OverlapCircle(position, .25f, instance.path_layerMask);
            isRoot = false;
            isNutrient = Physics2D.OverlapCircle(position, .25f, instance.nutrients_layerMask);
            hasUnit = false;
            value = 0;
            h_cost = int.MaxValue;
            g_cost = 0;
            f_cost = 0;
            pos = position;
            x = x_index;
            y = y_index;
        }
    }


    void SpawnNutrients()
    {
        while (nutrients_to_spawn > 0)
        {
            int x = Random.Range(0, max_x);
            int y = Random.Range(0, max_y);
            Node n = grid[x, y];
            if (n.isPath)
            {
                continue;
            }
            else
            {
                n.isNutrient = true;
                GameObject gameObject = Resources.Load<GameObject>("Prefabs/Nutrients");
                Instantiate(gameObject, n.pos, Quaternion.identity);
                nutrients_to_spawn--;
            }
        }
    }

    public List<Node> CalculatePath(Node startNode, Node destNode)
    {
        List<Node> to_search = new List<Node>();
        List<Node> searched = new List<Node>();
        List<Node> node_path = new List<Node>();
        to_search.Add(startNode);

        while (to_search.Count > 0)
        {
            if (to_search.Count > 1000)
            {
                print("FUG");
                break;
            }

            Node current = to_search[0];
            foreach (Node n in to_search)
            {
                if (n.f_cost < current.f_cost || (n.f_cost == current.f_cost && n.h_cost < current.h_cost))
                {
                    current = n;
                }
            }
            searched.Add(current);
            to_search.Remove(current);

            if (current == destNode)
            {
                while (current.parent != null)
                {
                    node_path.Add(current);
                    current = current.parent;
                }
                return node_path;
            }

            current.Neighbors = GetNeighbors(current);

            foreach (Node neighbor in current.Neighbors)
            {
                if (neighbor.isPath && !searched.Contains(neighbor))
                {
                    bool searching = to_search.Contains(neighbor);
                    float neighbor_cost = current.g_cost + Vector3.Distance(current.pos, neighbor.pos);

                    if (!searching || neighbor_cost < neighbor.g_cost)
                    {
                        neighbor.g_cost = neighbor_cost;
                        neighbor.f_cost = neighbor.g_cost + neighbor.h_cost;
                        neighbor.parent = current;

                        if (!searching)
                        {
                            neighbor.h_cost = Vector3.Distance(neighbor.pos, destNode.pos);
                            neighbor.f_cost = neighbor.g_cost + neighbor.h_cost;
                            to_search.Add(neighbor);
                        }
                    }
                }
            }
        }
        print("No Path");
        return null;
    }

    List<Node> GetNeighbors(Node node)
    {
        List<Node> Neighbors = new List<Node>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                int new_x = node.x + i;
                int new_y = node.y + j;
                if (new_x > -1 && new_x < max_x)
                {
                    if (new_y > -1 && new_y < max_y)
                    {
                        Neighbors.Add(grid[new_x, new_y]);
                    }
                }
            }
        }

        foreach(Node n in Neighbors)
        {
            if (!n.isPath)
            {
                Neighbors.Remove(n);
            }
        }
        return Neighbors;
    }

    public List<Node> GetUnitPathNeighbors(Vector3 pos)
    {
        Node node = GetNode(pos);
        List<Node> Neighbors = new List<Node>();
        if (node.x > 0)
        {
            Neighbors.Add(grid[node.x - 1, node.y]);
        }
        if (node.x + 1 < max_x)
        {
            Neighbors.Add(grid[node.x + 1, node.y]);
        }
        if (node.y > 0)
        {
            Neighbors.Add(grid[node.x, node.y - 1]);
        }
        if (node.y + 1 < max_y)
        {
            Neighbors.Add(grid[node.x, node.y + 1]);
        }
        return Neighbors;
    }

    public List<Vector3> GetPath(Vector3 startpos, Vector3 destpos)
    {
        Node startNode = GetNode(startpos);
        Node destNode = GetNode(destpos);
        List<Node> nodes = CalculatePath(startNode, destNode);
        List<Vector3> path = new List<Vector3>();
        foreach (Node n in nodes)
        {
            path.Add(n.pos);
        }
        path.Reverse();
        foreach (Node n in grid)
        {
            n.parent = null;
        }
        return path;
    }

    Node GetNode(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        if (x > 11 || x < -12 || y < -6 || y > 7)
        {
            return null;
        }
        return grid[x + (max_x / 2), (y - 1) + (max_y / 2)];
    }

    public bool IsPath(Vector3 pos)
    {
        Node n = GetNode(pos);
        if(n == null)
        {
            return false;
        }
        return n.isPath;
    }

    public int NearbyRoots(Node n)
    {
        int count = 0;
        if(n == null)
        {
            return 0;
        }

        List<Node> neighbors = GetNeighbors(n);
        for(int i = 0; i< neighbors.Count; i++)
        {
            if (neighbors[i].isRoot)
            {
                count++;
            }
        }
        return count;
    }

    public void UpdateNode(Vector3 pos, bool isRoot, bool sell)
    {
        Node n = GetNode(pos);
        if (!sell)
        {
            if (isRoot)
            {
                n.isRoot = true;
                if (n.isNutrient)
                {
                    Game_Manager.instance.UpdateNutrientRate(1);
                }
            }
            else
            {
                n.hasUnit = true;
            }
        }
        else
        {
            if (isRoot)
            {
                n.isRoot = false;
                if (n.isNutrient)
                {
                    Game_Manager.instance.UpdateNutrientRate(-1);
                }
                DestroyRoot(n.pos);
            }
            else
            {
                n.hasUnit = false;
                DestroyUnit(n.pos);
            }
        }
    }

    public bool CheckNode(Vector3 pos, bool isRoot, bool sell)
    {
        Node n = GetNode(pos);
        if(n == null)
        {
            return false;
        }
        if (!sell)
        {
            if (isRoot)
            {
                if (!n.isRoot && !n.hasUnit && !n.isPath)
                {
                    if (IsEdge(n))
                    {
                        return true;
                    }
                    if (NearbyRoots(n) > 0)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (!n.hasUnit && n.isRoot && !n.isPath)
                {
                    return true;
                }
            }
        }
        else
        {
            if (isRoot)
            {
                if (n.isRoot)
                {
                    if (IsEdge(n))
                    {
                        List<Node> nodes = GetNeighbors(n);
                        for(int i = 0; i<nodes.Count; i++)
                        {
                            if (!nodes[i].isRoot)
                            {
                                continue;
                            }
                            if (!IsEdge(nodes[i]))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        List<Node> nodes = GetNeighbors(n);
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            if (!nodes[i].isRoot)
                            {
                                continue;
                            }
                            if (IsEdge(nodes[i]))
                            {
                                continue;
                            }
                            if(NearbyRoots(nodes[i]) < 2)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            else
            {
                if (n.hasUnit)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsUnit(Vector3 pos)
    {
        Node n = GetNode(pos);
        if (n == null)
        {
            return false;
        }
        return n.hasUnit;
    }

    public int GetValue(Vector3 pos, bool isUnit)
    {
        if (isUnit) 
        { 
            Collider2D col = Physics2D.OverlapCircle(pos, .25f, instance.unit_layerMask);
            Unit unit = col.gameObject.GetComponent<Unit>();
            return unit.sell_value;
        }
        else
        {
            return 10;
        }
    }

    bool IsEdge(Node n)
    {
        int x = (int)n.pos.x;
        int y = (int)n.pos.y;
        if (x == 11 || x == -12 || y == -6 || y == 7)
        {
            return true;
        }
        return false;
    }

    public Vector3 NearestNodePos(Vector3 pos)
    {
        Node n = GetNode(pos);
        return n.pos;
    }

    void DestroyRoot(Vector3 position)
    {
        Collider2D col = Physics2D.OverlapCircle(position, .25f, instance.root_layerMask);
        Destroy(col.gameObject);
    }

    void DestroyUnit(Vector3 position)
    {
        Collider2D col = Physics2D.OverlapCircle(position, .25f, instance.unit_layerMask);
        Destroy(col.gameObject);
    }

    public List<Vector3> GetPath()
    {
        List<Vector3> list = new List<Vector3>();
        foreach(Vector3 vector in path)
        {
            list.Add(vector);
        }
        return list;
    }
}
