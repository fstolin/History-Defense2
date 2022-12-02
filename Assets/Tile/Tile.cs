    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] TowerMain tower;

    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
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

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            // Start placing tower
            bool isPlaced = tower.TowerPlacement(transform);
            // If tower is placed, make the til implacable
            isPlaceable = !isPlaced;
            // If the tower is placed, block the node
            if (isPlaced) gridManager.BlockNode(coordinates);
        }
    }
}
