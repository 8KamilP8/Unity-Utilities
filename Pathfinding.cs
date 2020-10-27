using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class Pathfinding {

    //Pathfinding Graph based on rectangles
    private static Graph<Rectangle> graph;
    private PathNode<GraphNode<Rectangle>>[] graphNodes;

    #region Singleton
    private static Pathfinding _instance;
    public static Pathfinding Instance { get {
            if (_instance == null) {
                //TO DO RETRIVE MAIN GRAPH
                graph = Object.FindObjectOfType<MapBaker>().GetGraph();
                _instance = new Pathfinding(graph);
            }
            return _instance;
        } }
    #endregion

    public Pathfinding(Graph<Rectangle> graph) {
        Pathfinding.graph = graph;
    }
    public static void Destroy() {
        _instance = null;
    }
    #region FIND PATH FUNCTIONS
    public Path<Rectangle> FindPath(Vector2 startPosition, Vector2 endPosition) {
        if(graph == null) Object.FindObjectOfType<MapBaker>().GetGraph();
        int startIndex = UtilFunc.GetGraphNodeIndexFromPosition(graph, startPosition);
        int endIndex = UtilFunc.GetGraphNodeIndexFromPosition(graph, endPosition);
        GraphNode<Rectangle> startNode = graph[startIndex];
        GraphNode<Rectangle> endNode = graph[endIndex];
        return FindPath(startNode, endNode);
    }
    public Path<Rectangle> FindPath(GraphNode<Rectangle> startNode, GraphNode<Rectangle> endNode) {

        Path<Rectangle> path = new Path<Rectangle>();
        HashSet<int> closedSet = new HashSet<int>();
        List<PathNode<GraphNode<Rectangle>>> openList = new List<PathNode<GraphNode<Rectangle>>>();
        if (graphNodes == null) {
            
            graphNodes = new PathNode<GraphNode<Rectangle>>[graph.Nodes.Length];
            for (int i = 0; i < graphNodes.Length; i++) {

                graphNodes[i] = new PathNode<GraphNode<Rectangle>>();
                graphNodes[i].element = graph[i];
                
            }
        }
        for (int i = 0; i < graphNodes.Length; i++) {
            //Initialise values

            graphNodes[i].data = new AStarData(float.MaxValue, CalculateHeuristicCost(graph[i], endNode), -1);
            
        }
        var startPathNode = new PathNode<GraphNode<Rectangle>>();
        startPathNode.element = startNode;
        startPathNode.data = new AStarData(0f, CalculateHeuristicCost(startNode, endNode), -1);
        openList.Add(startPathNode);
        
        
        while (openList.Count > 0) {
            var currentNode = GetLowestFcostNode(openList);
            
            closedSet.Add(currentNode.element.index);
            openList.Remove(currentNode);
            if (currentNode.element == endNode) {
                CalculatePath(out path, currentNode);
                return path;
            }
                
            for (int i = 0; i < currentNode.element.Neighbours.Length; i++) {
                PathNode<GraphNode<Rectangle>> neighbour = graphNodes[currentNode.element.Neighbours[i]];
                if (closedSet.Contains(neighbour.element.index)) continue;
                float tentativeGCost = currentNode.data.gCost + Vector2.Distance(currentNode.element.nodeData.position, neighbour.element.nodeData.position);
                if (tentativeGCost < neighbour.data.gCost) {
                    neighbour.data.gCost = tentativeGCost;
                    neighbour.data.cameFromeNode = currentNode.element.index;
                    
                }
                openList.Add(neighbour);
                //AddToSortedList(neighbour,openList);
            }

        }

        return path;
    }
    #endregion
    private PathNode<GraphNode<Rectangle>> GetLowestFcostNode(List<PathNode<GraphNode<Rectangle>>> list) {
        var pathNode = list[0];
        for(int i = 1; i < list.Count; i++) {
            if (pathNode.data.fCost > list[i].data.fCost)
                pathNode = list[i];
        }

        return pathNode;

    }
    private PathNode<GraphNode<Rectangle>> GetLowestFCostNode(List<PathNode<GraphNode<Rectangle>>> pathNodes, bool sorted = false) {
        if (sorted) return pathNodes[0];
        PathNode<GraphNode<Rectangle>> lowestFCostNode = pathNodes[0];
        pathNodes.ForEach((PathNode<GraphNode<Rectangle>> pathNode) => {
            if (pathNode.data.fCost < lowestFCostNode.data.fCost) {
                lowestFCostNode = pathNode;
            }
        });
        return lowestFCostNode;
    }
    private void CalculatePath(out Path<Rectangle> path, PathNode<GraphNode<Rectangle>> endNode) {
        path = new Path<Rectangle>();
        var currentNode = endNode;
        while (currentNode.data.cameFromeNode != -1) {
            path.AddToPath(currentNode.element);
            currentNode = graphNodes[currentNode.data.cameFromeNode];
        }
        path.AddToPath(currentNode.element);
    }
    private void AddToSortedList(PathNode<GraphNode<Rectangle>> pathNode, List<PathNode<GraphNode<Rectangle>>> pathNodes) {
        int leftIndex = 0;
        int rightIndex = pathNodes.Count - 1;
        int mid = leftIndex + (rightIndex / 2);

        while (leftIndex <= rightIndex) {
            mid =  (leftIndex + rightIndex) / 2;

            if (pathNodes[mid].data.fCost < pathNode.data.fCost) {
                leftIndex = mid + 1;
            }
            else if (pathNodes[mid].data.fCost > pathNode.data.fCost) {
                rightIndex = mid - 1;
            }
            else {
                //pathNodes.Insert(mid, pathNode);
                break;
            }
        }
        string message = "";
        foreach(var pathnode in pathNodes) {
            Debug.Log("FOREACH");
            message += pathnode.element.index + ";";
        }
        Debug.Log(message);
        //mid = leftIndex + (rightIndex / 2);
        pathNodes.Insert(mid, pathNode);
    }
    private float CalculateHeuristicCost(GraphNode<Rectangle> A, GraphNode<Rectangle> B) {
        if(B == null) {
            Debug.Log("A index = " + A.index);
            Debug.Log("B index = " + B.index);
        }

        return Vector2.Distance(A.nodeData.position,B.nodeData.position);
    }

}
public struct AStarData {

    public float gCost;
    public float hCost;
    public float fCost;

    public int cameFromeNode;
    public AStarData(float gCost, float hCost, int cameFromeNode) {
        this.gCost = gCost;
        this.hCost = hCost;
        fCost = gCost + hCost;
        this.cameFromeNode = cameFromeNode;
    }

    public float CalculateFCost() {
        return gCost + hCost;
    }
}

public class PathNode<T> {
    public AStarData data;
    public T element;
}
public class Path<T> {
    Stack<GraphNode<T>> path;
    public Path() {
        path = new Stack<GraphNode<T>>();
    }
    public Path(Stack<GraphNode<T>> path) {
        this.path = path;
    }
    public void AddToPath(GraphNode<T> node) {
        path.Push(node);
    }
    public T PopFromPath() {
        return path.Pop().nodeData;
    }
    public GraphNode<T> PopNodeFromPath() {
        return path.Pop();
    }
    public T PeekFromPath() {
        return path.Peek().nodeData;
    }
    public GraphNode<T> PeekNodeFromPath() {
        return path.Peek();
    }
    public bool IsEmpty() {
        return path.Count == 0;
    }
}