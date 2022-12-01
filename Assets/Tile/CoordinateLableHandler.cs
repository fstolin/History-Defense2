using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLableHandler : MonoBehaviour
{

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color occupiedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    

    // Coordinates to display in 2D space
    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;
    bool areCoordsVisible;
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        // Display the coordinates on awake -> once during gameplay.
        DisplayCoordinates();
    }

    private void Update()
    {
        ToggleLabels();
        // Update coordinates in editor
        if(!Application.isPlaying)
        {
            areCoordsVisible = true;
            DisplayCoordinates();
            RenameTheObject();            
        }
        SetLabelColor();
    }

    // Displays the coordinates on each game object.
    private void DisplayCoordinates()
    {
        if (!areCoordsVisible)
        {
            label.text = "";
        } else
        {
            // Get the coordinates from the PARENT (tile) position. Save them to our Vector2Int variable.
            coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
            coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
            // Display the coordinates
            label.text = (coordinates.x + "," + coordinates.y);
        }
    }

    // Renames the gameobjects parent to current coordinates.
    private void RenameTheObject()
    {
        this.transform.parent.name = coordinates.ToString();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            areCoordsVisible = !areCoordsVisible;
            DisplayCoordinates();
        }
    }

    // Sets the label color according to node parameters
    private void SetLabelColor()
    {
        if (gridManager == null)
        {
            label.color = Color.black;
            return;
        }
        // Find the node in grid with calculated coordinates from DisplayCoordinates method.
        Node node = gridManager.GetNode(coordinates);

        if (node == null)
        {
            label.color = Color.black;
            return;
        }
        // Check different states & assign colors
        if (!node.isWalkable)
        {
            label.color = occupiedColor;
        }
        else if (node.isPath) {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        } else
        {
            label.color = defaultColor;
        }
    }
}
