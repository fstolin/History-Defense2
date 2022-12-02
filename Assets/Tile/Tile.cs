    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] TowerMain tower;

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            // Assign coordinates to this tile
            coordinates = gridManager.GetCoordinatesFromPosition(this.transform.position);
            // Block the path, if it isn't placeable
            if (!isPlaceable) gridManager.BlockNode(coordinates);
        }
    }

    // Handle behaviour on mouse down - build a tower
    private void OnMouseDown()
    {
        // Build a tower, if tile isWalkable and placing wouldn't block path
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            // Start placing tower
            bool isPlaced = tower.TowerPlacement(transform);
            // If tower is placed, make the til implacable
            isPlaceable = !isPlaced;
            // If the tower is placed, block the node
            if (isPlaced)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }   
    }
}
