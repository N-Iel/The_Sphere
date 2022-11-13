using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public Lvl1_2Manager lvlManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lvlManager.OnChekpointCrossed();
            gameObject.SetActive(false);
        }
    }
}
