using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AstarPathFinder : MonoBehaviour
{
    private List<Vector2> PathToTarget;
    private List<Node> CheckNodes;
    private List<Node> FreeNodes = new List<Node>();
    private List<Node> WaitNodes;
    //[SerializeField]
   // private GameObject player;
    [SerializeField]
    private LayerMask layer;

    private void Start()
    {
        PathToTarget = new List<Vector2>();
        CheckNodes = new List<Node>();
        WaitNodes = new List<Node>();
        // player = GameObject.Find("hero");
    }
    void Update()
    {
        //PathToTarget = GetPath(player.transform.position);
    }

    public List<Vector2> GetPath(Vector2 target)
    {
        PathToTarget = new List<Vector2>();
        CheckNodes = new List<Node>();
        WaitNodes = new List<Node>();
        Vector2 StartPositon = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        Vector2 TatgetPosion = new Vector2(Mathf.Round(target.x), Mathf.Round(target.y));

        if(StartPositon == TatgetPosion)
        {
            return PathToTarget;
        }
        Node startNode = new Node(0, StartPositon, TatgetPosion, null);
        CheckNodes.Add(startNode);
        WaitNodes.AddRange(GetNeighborsNode(startNode));
        while(WaitNodes.Count > 0)
        {
            Node nodeToCheck = WaitNodes.Where(x => x.F == WaitNodes.Min(y => y.F)).FirstOrDefault();

            if(nodeToCheck.postion == TatgetPosion)
            {
                return CalculatePathFromNode(nodeToCheck);
            }
            bool walkable = !Physics2D.OverlapCircle(nodeToCheck.postion, 0.1f, layer);
            if (!walkable)
            {
                WaitNodes.Remove(nodeToCheck);
                CheckNodes.Add(nodeToCheck);
            }
            else
            {
                WaitNodes.Remove(nodeToCheck);
                if(!CheckNodes.Where(x => x.postion == nodeToCheck.postion).Any())
                {
                    CheckNodes.Add(nodeToCheck);
                    WaitNodes.AddRange(GetNeighborsNode(nodeToCheck));
                }
            }
        }
        FreeNodes = CheckNodes;

        return PathToTarget;
    }

    private List<Vector2> CalculatePathFromNode(Node node)
    {
        List<Vector2> path = new List<Vector2>();
        Node current = node;
        while(current.prevNode != null)
        {
            path.Add(current.postion);
            current = current.prevNode;
        }
        return path;
    }

     private List<Node> GetNeighborsNode(Node node)
    {
        List<Node> Neighbors = new List<Node>();

        Neighbors.Add(new Node(node.G+1, new Vector2(node.postion.x-1, node.postion.y),
            node.targetPosition, node));
        Neighbors.Add(new Node(node.G+1, new Vector2(node.postion.x+1, node.postion.y),
            node.targetPosition, node));
        Neighbors.Add(new Node(node.G+1, new Vector2(node.postion.x, node.postion.y-1),
            node.targetPosition, node));
        Neighbors.Add(new Node(node.G+1, new Vector2(node.postion.x, node.postion.y+1),
            node.targetPosition, node));
        return Neighbors;
    }

    private class Node
    {
        public Vector2 postion;
        public Vector2 targetPosition;
        public Node prevNode;
        public int F; //f = g + h
        public int G;//расстояние от старта до нода
        private int H;//расстояние от нода до цели
        public Node(int g, Vector2 nodePosition, Vector2 targetPosition, Node previousNode)
        {
            this.postion = nodePosition;
            this.targetPosition = targetPosition;
            this.prevNode = previousNode;
            this.G = g;
            this.H = (int)Mathf.Abs(targetPosition.x - postion.x) + (int)Mathf.Abs(targetPosition.y - postion.y);
            this.F = G + H;
        }
    }

    private void OnDrawGizmos()
    {
        if(CheckNodes != null)
        foreach (var item in CheckNodes)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(item.postion, 0.1f);
        }
        if (PathToTarget != null)
        foreach (var item in PathToTarget)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector2(item.x, item.y), 0.2f);
        }  
 
    }
}
