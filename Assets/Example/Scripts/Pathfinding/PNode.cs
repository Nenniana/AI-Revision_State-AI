using GridSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PNode
{
    public int x, y;
    private float width, height;
    private Vector3 position;
    private GridCore<PNode> gridCore;

    private SpriteRenderer spriteRenderer;
    private GameObject spriteObject;
    private string blackColor = "#424242";
    private Color black;

    public int gCost;
    public int hCost;
    public int fCost;
    public bool visited;
    public bool visitedByEnd;

    private bool isWalkable = true;
    public PNode cameFromNode;

    public bool IsWalkable { get => isWalkable; set { isWalkable = value; if (!value) SetColor(black); } }

    public Vector3 Position { get { return position; } }

    public PNode(GridCore<PNode> gridCore, int x, int y, Vector3 position, float height, float width)
    {
        ColorUtility.TryParseHtmlString(blackColor, out black);
        this.gridCore = gridCore;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.position = position + new Vector3(width * 0.5f, height * 0.5f, 0);
    }

    public void SetUpRenderer(Sprite sprite, Color color)
    {
        spriteObject = new GameObject(sprite.name, typeof(SpriteRenderer));
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        spriteObject.transform.localPosition = position + new Vector3(width * 0.5f, height * 0.5f);
            
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;

        if (!isWalkable)
            spriteRenderer.color = black;
    }

    public void ResetNode()
    {
        visited = false;
        visitedByEnd = false;
        gCost = int.MaxValue;
        hCost = 0;
        fCost = 0;
        cameFromNode = null;
        CalculateFCost();
    }

    public void SetColor (Color color)
    {
        spriteRenderer.color = color;
    }

    public override string ToString()
    {
        int tempG = gCost;
        int tempF = fCost;

        if (tempG > 1000) {
            tempG = 0;
            tempF = 0;
        }

        return "G: " + tempG + "\nF: " + tempF + "\nH: " + hCost; 
    }

    internal void CalculateFCost()
    {
        fCost = gCost + hCost;
        gridCore.TriggerOnGridValueChanged(x, y);
    }
}