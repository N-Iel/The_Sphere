using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// LLevará a cabo el control de las dos fases que componen el nivel, así como iniciar las animaciones.
/// </summary>

public class Lvl1_2Manager : MonoBehaviour
{
    public Animator firstPhaseAnim;
    public GameObject spCollect, spWalls; // This are the walls and collectables for the fp(firstPhase) && sp(secondPhase)

    // This will trigger some animations and enable/disable elements form phase 1 && 2
    public void OnChekpointCrossed()
    {
        // First Phase animation
        firstPhaseAnim.SetBool("checkpoint", true);

        // Second Phase props
        spCollect.SetActive(true);
        spWalls.SetActive(true);
    }
}
