using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = Color.green;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;
    
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetColorCoordinates();
        ToggleLabels();
    }



    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }



    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.y);

        label.text = $"{coordinates.x},{coordinates.y}";
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }   

    void SetColorCoordinates()
    {
        if (gridManager != null)
        {
            Node node = gridManager.GetNode(coordinates);

            if (node != null)
            {
                if (!node.isWalkable)
                {
                    label.color = blockedColor;
                }
                else if (node.isPath) 
                { 
                    label.color = pathColor;
                }
                else if(node.isExplored)
                {
                    label.color = exploredColor;
                }
                else
                {
                    label.color = defaultColor;
                }
            }

        }
    }
}
