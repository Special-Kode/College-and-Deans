using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private Grid grid;
    private int x;
    public int X {get {return x;} set{x = value;} }
    private int y;
    public int Y {get {return y;} set{y = value;} }

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public PathNode cameFromNode;
    public PathNode(Grid grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
