using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private float x;
    private float y;
    private float height;
    private float width;

    public float Tam
    {
        get{ return width/2;}
        private set {width = value;}
    }
    public Vector2 center;

    public Cell(float x1, float y1)
    {
        x = x1;
        y = y1;
        height = width = 1.5f;
        Tam = width/2;
        center = new Vector2(x, y);
    }
}
