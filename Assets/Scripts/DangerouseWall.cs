using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerouseWall : MonoBehaviour
{

    public Transform checkpoint;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.transform.position = checkpoint.position;
    }
}
