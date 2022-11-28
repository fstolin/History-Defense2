using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] TowerMain tower;

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            // Start placing tower
            bool isPlaced = tower.TowerPlacement(transform);
            // If tower is placed, make the til implacable
            isPlaceable = !isPlaced;            
        }
    }
}
