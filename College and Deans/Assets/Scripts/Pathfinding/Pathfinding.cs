using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding{

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance {get; private set;}

    private Grid grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private Vector2 originPosition;

    public Pathfinding(int width, int height, float cellSize, Vector3 originPosition)
    {
        Instance = this;
        grid = new Grid(width, height, cellSize, originPosition);
        this.originPosition = originPosition;
    }

    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if(path == null)
        {
            return null;
        }else{
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.X, pathNode.Y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;   
        }
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetValue(startX, startY);
        PathNode endNode = grid.GetValue(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++){
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetValue(x,y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0) 
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode){return CalculatePath(endNode);}

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighboursList(currentNode))
            {
                if(closedList.Contains(neighbourNode)) continue;
                if(!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    private List<PathNode> GetNeighboursList(PathNode currentNode)
    {
        List<PathNode> neighboursList = new List<PathNode>();

        if(currentNode.X - 1 >= 0)
        {
            //Left
            neighboursList.Add(GetNode(currentNode.X - 1, currentNode.Y));
            //Left Up
            if(currentNode.Y + 1 < grid.GetHeight())neighboursList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
            //Left Down
            if(currentNode.Y - 1 >= 0)neighboursList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
        }
        if(currentNode.X + 1 < grid.GetWidth())
        {
            //Right
            neighboursList.Add(GetNode(currentNode.X + 1, currentNode.Y));
            //Right Up
            if(currentNode.Y + 1 < grid.GetHeight())neighboursList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
            //Right Down
            if(currentNode.Y - 1 >= 0)neighboursList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
        }
        //Down
        if(currentNode.Y - 1 >= 0) neighboursList.Add(GetNode(currentNode.X, currentNode.Y - 1));
        //Up
        if(currentNode.Y - 1 < grid.GetHeight()) neighboursList.Add(GetNode(currentNode.X, currentNode.Y + 1));

        return neighboursList;
    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetValue(x,y);
    }

    public Grid GetGrid()
    {
        return grid;
    }

    public Vector2 GetOriginPosition()
    {
        return originPosition;
    }
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost){lowestFCostNode = pathNodeList[i];}
        }
        return lowestFCostNode;
    }
}
