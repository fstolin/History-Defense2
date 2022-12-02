using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Grid dimensions
    [SerializeField] Vector2Int gridSize;
    [Tooltip("World grid size - should match UnityEditor snap settings.")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    // Dictionary getter - returns node
    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates)) return grid[coordinates];
        // Node doesn't exist
        return null;
    }


    // Sets the passed in node as not walkable
    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates)) grid[coordinates].isWalkable = false;
    }

    // Returns vector2Int coordinates from transform unity position
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);
        return coordinates;
    }

    // Returns unity transform position from coordiantes
    public Vector3 GetPositionFromCoordiantes(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;
        position.y = 0f;
        return position;
    }

    // Fill our dictionary with nodes = create the grid
    private void CreateGrid()
    {
        for (int x = 0; x <= gridSize.x; x++)
        {
            for (int y = 0; y <= gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
        Debug.Log("GridCreated");
    }


}
