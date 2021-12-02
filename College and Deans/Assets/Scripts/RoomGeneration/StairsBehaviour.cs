using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StairsBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<ExternMechanicsPlayer>().stopTimeOnStairs = 0;
            FindObjectOfType<LevelLoader>().LoadNextStage();
        }
    }
}
