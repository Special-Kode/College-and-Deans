using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class DoorBehaviour : MonoBehaviour
{
    public enum DoorDirection
    {
        Top = 0, Right = 1, Bottom = 2, Left = 3
    };

    public DoorDirection doorDirection;

    public RoomBehaviour RoomParent;
    public GameObject AdjacentRoom;

    public GameObject lockSprite;
    private Vector3 dir;

    void Awake()
    {
        lockSprite = Resources.Load("DoorLock") as GameObject;
        lockSprite = Instantiate(lockSprite, transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        CenterLockIntoDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CenterLockIntoDoor()
    {
        switch (doorDirection)
        {
            case DoorDirection.Top:
                lockSprite.transform.localPosition += Vector3.up * 7;
                break;
            case DoorDirection.Right:
                lockSprite.transform.localPosition += Vector3.right * 10;
                break;
            case DoorDirection.Bottom:
                lockSprite.transform.localPosition += Vector3.down * 7;
                break;
            case DoorDirection.Left:
                lockSprite.transform.localPosition += Vector3.left * 10;
                break;
        }
    }

    public void UnlockDoor()
    {
        GetComponent<TilemapCollider2D>().isTrigger = true;
        lockSprite.GetComponent<SpriteRenderer>().forceRenderingOff = true;
    }

    public void LockDoor()
    {
        GetComponent<TilemapCollider2D>().isTrigger = false;
        lockSprite.GetComponent<SpriteRenderer>().forceRenderingOff = false;
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
            /**
            FindObjectOfType<GameManager>().EnterRoom(room);
            //*/
            room.EnterRoom();
        }
    }
}
