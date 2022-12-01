using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    // Direction of the BFS algorithm
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    // Nodes that we reached
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    // BFS Queue
    Queue<Node> frontier = new Queue<Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null) grid = gridManager.Grid;

        startNode = new Node(startCoordinates, true);
        destinationNode = new Node(destinationCoordinates, true);
    }

    // Start is called before the first frame update
    void Start()    
    {
        StartCoroutine(BreadthFirstSearch());
    }

    private IEnumerator BreadthFirstSearch()
    {
        // Is Running is required to stop the algorithm if we find the target
        bool isRunning = true;

        // Add the start node to the queue -> we will explore it's neighbors first
        frontier.Enqueue(startNode);
        // We have reached the startNode -> add it to reached + it is part of our path
        reached.Add(startNode.coordinates, startNode);
        startNode.isPath = true;

        // While we have tiles queued and haven't reached the end, continue running
        while (frontier.Count > 0 && isRunning)
        {
            // Take a node from the queue & mark it as explored
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;

            // Explore current nodes neighbors & adds them to frontier,
            ExploreNeighbors();

            // If we found the target, explore each left frontier node & exit
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
            yield return new WaitForSeconds(1);
        }
    }

    // Explores current search nodes neighbors, adds then to Reached & Frontier arrays.
    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = direction + currentSearchNode.coordinates;
            if (grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(grid[neighborCoordinates]);
            }            
        }
        
        // Add neighbors to the reached list. Do not add duplicates. Only add walkable tiles.
        // If all conditions are meant, the neighbor is added to the frontier queue.
        foreach (Node neighbor in neighbors)
        {
            if(neighbor.isWalkable && !reached.ContainsKey(neighbor.coordinates))
            {
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    
}
