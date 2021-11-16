using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public bool ready;
    Character playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    public void PlayMinigame()
    {
        ready = true;

        playerScript.canMove = false;
        playerScript.lookSpeed = playerScript.lookSpeed / 3;
    }
}
