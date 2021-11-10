using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RoomBehaviour : MonoBehaviour
{

    [Header("Adjacent Rooms")]
    public RoomBehaviour topRoom;
    public RoomBehaviour rightRoom;
    public RoomBehaviour bottomRoom;
    public RoomBehaviour leftRoom;

    [Header("Room Information")]
    public RoomInfo roomInfo;
    public Transform[] SpawnPoints;
    public bool hasSpawned;
    public NavMeshSurface2d navMeshSurface;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var door in GetComponentsInChildren<DoorBehaviour>())
        {
            door.DisableCollider(); //Disable door colliders
            door.SetAdjacentRoom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAdjacentRooms(List<GameObject> rooms, List<Vector2> positions, Vector2 moveAmount)
    {
        foreach(var room in rooms)
        {
            if(room.GetComponent<RoomBehaviour>() != null)
            {
                if (room.transform.position == transform.position + Vector3.up * moveAmount.y)
                    topRoom = room.GetComponent<RoomBehaviour>();
                if (room.transform.position == transform.position + Vector3.down * moveAmount.y)
                    bottomRoom = room.GetComponent<RoomBehaviour>();
                if (room.transform.position == transform.position + Vector3.right * moveAmount.x)
                    rightRoom = room.GetComponent<RoomBehaviour>();
                if (room.transform.position == transform.position + Vector3.left * moveAmount.x)
                    leftRoom = room.GetComponent<RoomBehaviour>();
            }
        }
    }
    public void SetNavMesh()
    {
        navMeshSurface = this.gameObject.GetComponentInChildren<NavMeshSurface2d>();
        navMeshSurface.collectObjects = CollectObjects2d.All;
        navMeshSurface.defaultArea = 1;
        // navMeshSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
        navMeshSurface.ignoreNavMeshAgent = false;
        navMeshSurface.gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
        navMeshSurface.BuildNavMesh();
    }
}
