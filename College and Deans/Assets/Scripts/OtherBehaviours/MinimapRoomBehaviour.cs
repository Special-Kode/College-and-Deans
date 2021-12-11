using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRoomBehaviour : MonoBehaviour
{
    public RoomInfo roomInfo;
    public RoomBehaviour room;
    public SpriteRenderer minimapIconRenderer;

    private float adjacentRoomAlpha = 0.55f;
    private StatsManager statsManager;
    
    // Start is called before the first frame update
    void Start()
    {
        ColorMinimapRoom();
        SetMinimapIcon();
        NotRenderRoom();

        statsManager = FindObjectOfType<StatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RenderSelection();
    }

    void ColorMinimapRoom()
    {
        foreach (var wall in GetComponentsInChildren<SpriteRenderer>())
        {
            //TODO make this color comparison more properly
            if ((wall.color == Color.white || wall.color == new Color(1, 1, 1, adjacentRoomAlpha)) && wall.name != "MinimapIcon")
            {
                switch (roomInfo.roomType)
                {
                    case RoomInfo.RoomType.Spawn:
                        wall.color = Color.cyan;
                        break;
                    case RoomInfo.RoomType.Cafe:
                        wall.color = Color.green;
                        break;
                    case RoomInfo.RoomType.ModLoot:
                    case RoomInfo.RoomType.EnhLoot:
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
            wall.color = new Color(wall.color.r, wall.color.g, wall.color.b, 1.0f);
        }
    }

    void SetMinimapIcon()
    {
        Sprite minimapIcon;
        minimapIcon = Resources.Load<Sprite>("Minimap/MinimapIcons/Minimap" + roomInfo.roomType.ToString());
        minimapIconRenderer.sprite = minimapIcon;
    }

    private void RenderSelection()
    {
        if (statsManager.SeeFullMinimap >= 1.0f)
            RenderAll();
        else if (statsManager.SeeFullMinimap <= 0.0f)
            RenderOnlyVisited();
        else
            RenderIfVisited();
    }

    private void NotRenderRoom()
    {
        foreach (var elem in GetComponentsInChildren<SpriteRenderer>())
        {
            elem.enabled = false;
        }
    }

    private void RenderAdjacent(float alpha)
    {
        if (room.topRoom != null)
            foreach (var elem in room.topRoom.minimapRoom.GetComponentsInChildren<SpriteRenderer>())
            {
                elem.enabled = true;
                if (!room.topRoom.hasBeenVisited)
                    elem.color = new Color(elem.color.r, elem.color.g, elem.color.b, alpha);
            }
        if (room.rightRoom != null)
            foreach (var elem in room.rightRoom.minimapRoom.GetComponentsInChildren<SpriteRenderer>())
            {
                elem.enabled = true;
                if (!room.rightRoom.hasBeenVisited)
                    elem.color = new Color(elem.color.r, elem.color.g, elem.color.b, alpha);
            }
        if (room.bottomRoom != null)
            foreach (var elem in room.bottomRoom.minimapRoom.GetComponentsInChildren<SpriteRenderer>())
            {
                elem.enabled = true;
                if (!room.bottomRoom.hasBeenVisited)
                    elem.color = new Color(elem.color.r, elem.color.g, elem.color.b, alpha);
            }
        if (room.leftRoom != null)
            foreach (var elem in room.leftRoom.minimapRoom.GetComponentsInChildren<SpriteRenderer>())
            {
                elem.enabled = true;
                if (!room.leftRoom.hasBeenVisited)
                    elem.color = new Color(elem.color.r, elem.color.g, elem.color.b, alpha);
            }
    }

    void RenderAll()
    {
        foreach (var elem in GetComponentsInChildren<SpriteRenderer>())
        {
            elem.enabled = true;
            elem.color = new Color(elem.color.r, elem.color.g, elem.color.b, 1.0f);
        }
    }

    void RenderOnlyVisited()
    {
        foreach (var elem in GetComponentsInChildren<SpriteRenderer>())
        {
            elem.enabled = room.hasBeenVisited;
            elem.color = new Color(elem.color.r, elem.color.g, elem.color.b, 1.0f);
        }
    }

    void RenderIfVisited()
    {
        if (!room.hasBeenVisited) return;

        foreach (var elem in GetComponentsInChildren<SpriteRenderer>())
        {
            elem.enabled = room.hasBeenVisited;
            elem.color = new Color(elem.color.r, elem.color.g, elem.color.b, 1.0f);
        }

        RenderAdjacent(adjacentRoomAlpha);
    }
}
