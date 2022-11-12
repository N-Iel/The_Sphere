using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{

    Animator animator;
    MeshRenderer mesh;
    bool isActivated;
    public Material matOn, matOff;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
        isActivated = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // It will trigger an animation
        if (collision.gameObject.CompareTag("Player"))
            animator.SetBool("isTurned", true);

    }

    // It will move up the btn and update the material
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = !isActivated;

            Debug.Log("test");

            // Trigger push animation
            animator.SetBool("isTurned", false);

            // It will change the material
            mesh.material =  isActivated ? matOff : matOn;

            // it will send the pressed signal
        }
    }
}
