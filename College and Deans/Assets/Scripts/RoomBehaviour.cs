using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public Transform[] SpawnPoints;

    public RoomInfo roomInfo;

    public bool hasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Room spawned!");
        foreach (var door in GetComponentsInChildren<DoorBehaviour>())
        {
            door.DisableCollider();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
