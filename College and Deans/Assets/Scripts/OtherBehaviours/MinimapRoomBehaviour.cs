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

    void RenderIfVisited()
    {
        foreach (var elem in GetComponentsInChildren<SpriteRenderer>())
        {
            elem.enabled = room.hasBeenVisited;
        }
    }
}
