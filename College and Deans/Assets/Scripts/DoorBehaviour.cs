using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorBehaviour : MonoBehaviour
{
    public enum DoorDirection
    {
        Top = 0, Right = 1, Bottom = 2, Left = 3
    };

    public DoorDirection doorDirection;

    public RoomBehaviour RoomParent;
    public GameObject AdjacentRoom;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableCollider()
    {
        //GetComponent<TilemapCollider2D>().enabled = false;
        GetComponent<TilemapCollider2D>().isTrigger = true;
    }

    public void SetAdjacentRoom()
    {
        if(RoomParent == null)
            RoomParent = GetComponentInParent<RoomBehaviour>();

        switch (doorDirection)
        {
            case DoorDirection.Top:
                AdjacentRoom = RoomParent.topRoom.gameObject;
                dir = Vector3.up;
                break;
            case DoorDirection.Right:
                AdjacentRoom = RoomParent.rightRoom.gameObject;
                dir = Vector3.right;
                break;
            case DoorDirection.Bottom:
                AdjacentRoom = RoomParent.bottomRoom.gameObject;
                dir = Vector3.down;
                break;
            case DoorDirection.Left:
                AdjacentRoom = RoomParent.leftRoom.gameObject;
                dir = Vector3.left;
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && GetComponentInParent<RoomBehaviour>() != null)
        {
            other.gameObject.GetComponent<Movement>().agent.Warp(other.transform.position + dir * 6); //TODO not hardcode this number
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            Camera.main.GetComponent<CameraBetweenRooms>().CurrentRoom = AdjacentRoom;
            var room = AdjacentRoom.GetComponent<RoomBehaviour>();
            FindObjectOfType<GameManager>().EnterRoom(room);
        }
    }
}
