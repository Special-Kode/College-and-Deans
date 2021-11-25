using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRoomBehaviour : MonoBehaviour
{
    public RoomInfo roomInfo;
    public RoomBehaviour room;
    
    // Start is called before the first frame update
    void Start()
    {
        ColorMinimapRoom();
        NotRenderRoom();
    }

    // Update is called once per frame
    void Update()
    {
        RenderIfVisited();
    }

    void ColorMinimapRoom()
    {
        foreach (var wall in GetComponentsInChildren<SpriteRenderer>())
        {
            if (wall.color == Color.white)
            {
                switch (roomInfo.roomType)
                {
                    case RoomInfo.RoomType.Spawn:
                        wall.color = Color.cyan;
                        break;
                    case RoomInfo.RoomType.Cafe:
                        wall.color = Color.green;
                        break;
                    case RoomInfo.RoomType.Loot:
                        wall.color = Color.yellow;
                        break;
                    case RoomInfo.RoomType.Stairs:
                        wall.color = Color.magenta;
                        break;
                    case RoomInfo.RoomType.Boss:
                        wall.color = Color.red;
                        break;
                }
            }
        }
    }

    private void NotRenderRoom()
    {
        foreach (var elem in GetComponentsInChildren<SpriteRenderer>())
        {
            elem.enabled = false;
        }
    }

    private void RenderAdjacent()
    {
        if (room.topRoom != null)
            foreach (var elem in room.topRoom.minimapRoom.GetComponentsInChildren<SpriteRenderer>())
            {
                elem.enabled = true;
            }
        if (room.rightRoom != null)
            foreach (var elem in room.rightRoom.minimapRoom.GetComponentsInChildren<SpriteRenderer>())
            {
                elem.enabled = true;
            }
        if (room.bottomRoom != null)
            foreach (var elem in room.bottomRoom.minimapRoom.GetComponentsInChildren<SpriteRenderer>())
            {
                elem.enabled = true;
            }
        if (room.leftRoom != null)
            foreach (var elem in room.leftRoom.minimapRoom.GetComponentsInChildren<SpriteRenderer>())
            {
                elem.enabled = true;
            }
    }

    void RenderIfVisited()
    {
        if (!room.hasBeenVisited) return;

        foreach (var elem in GetComponentsInChildren<SpriteRenderer>())
        {
            elem.enabled = room.hasBeenVisited;
        }

        RenderAdjacent();
    }
}
