using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBetweenRooms : MonoBehaviour
{
    public GameObject CurrentRoom;
    private Vector3 camSpeed;
    public float Smoothness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 currentPos = CurrentRoom.transform.position;
        if (transform.position != new Vector3(currentPos.x, currentPos.y, transform.position.z))
        {
            Vector3 newCameraPos = CurrentRoom.transform.position;
            newCameraPos.z = transform.position.z;
            Debug.Log(newCameraPos);
            Vector3 oldCameraPos = transform.position;
            transform.position = Vector3.SmoothDamp(oldCameraPos, newCameraPos, ref camSpeed, Smoothness);
        }
    }
}
