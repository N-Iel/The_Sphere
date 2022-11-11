using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

    GameObject gameManager;
    
    // It will setUp the gameManager
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // It will add score and hide when the player triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().AddScore();
            gameObject.SetActive(false);
        }
    }
}
