using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Node currentSearchNode;
    // Direction of the BFS algorithm
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null) grid = gridManager.Grid;
    }

    // Start is called before the first frame update
    void Start()    
    {
        ExploreNeighbors();
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = direction + currentSearchNode.coordinates;
            if (grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(grid[neighborCoordinates]);
                // TODO remove after test
                grid[neighborCoordinates].isExplored = true;
                grid[currentSearchNode.coordinates].isPath = true;
            }            
        }        
    }

    
}
