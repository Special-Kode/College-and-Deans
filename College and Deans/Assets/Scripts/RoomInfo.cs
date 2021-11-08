using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : ScriptableObject
{
    public enum RoomType //All possible room types
    {
        Spawn, Enemies, Boss, Cafe, Loot
    };

    public enum AdjacentRooms
    {
        None = 0, North = 1, East = 2, South = 4, West = 8 //Every value allows a flag behaviour
    };

    public RoomType roomType;
    public AdjacentRooms adjacentRooms;

    public Vector2 position;

    //Adds a flag for every door that needs to be generated
    public void CheckAdjacentRooms(List<Vector2> positions, Vector2 moveAmount)
    {
        if (positions.Contains(position + Vector2.up * moveAmount.y))
        {
            adjacentRooms |= AdjacentRooms.North;
        }
        if (positions.Contains(position + Vector2.down * moveAmount.y))
        {
            adjacentRooms |= AdjacentRooms.South;
        }

        if (positions.Contains(position + Vector2.left * moveAmount.x))
        {
            adjacentRooms |= AdjacentRooms.West;
        }
        if (positions.Contains(position + Vector2.right * moveAmount.x))
        {
            adjacentRooms |= AdjacentRooms.East;
        }
    }
}