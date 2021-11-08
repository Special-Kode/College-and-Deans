using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorBehaviour : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && GetComponentInParent<RoomBehaviour>() != null)
        {
            //When a door is touched, you're entering the room
            Camera.main.GetComponent<CameraBetweenRooms>().CurrentRoom = GetComponentInParent<RoomBehaviour>().gameObject;
            var room = GetComponentInParent<RoomBehaviour>();
            FindObjectOfType<GameManager>().EnterRoom(room);
        }
    }
}
