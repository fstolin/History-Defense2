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
    

    // Coordinates to display in 2D space
    Vector2Int coordinates = new Vector2Int();
    MouseInputHandler mouseHandler;
    TextMeshPro label;
    bool areCoordsVisible;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        mouseHandler = GetComponentInParent<MouseInputHandler>();
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

    private void SetLabelColor()
    {
        if (mouseHandler.IsPlaceable)
        {
            label.color = defaultColor;
        } else
        {
            label.color = occupiedColor;
        }
    }
}
