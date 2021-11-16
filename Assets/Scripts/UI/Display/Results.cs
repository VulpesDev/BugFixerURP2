using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Results : MonoBehaviour
{
    PopUps popups_Panel;
    Text current, target;

    Character playerScript;

    private void Awake()
    {
        current = transform.GetChild(1).GetComponent<Text>();
        target = transform.GetChild(2).GetComponent<Text>();
        current.text = "";
        target.text = "";
    }
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        popups_Panel = transform.parent.GetChild(3).GetComponent<PopUps>();
        current.text = popups_Panel.SuccessRate().ToString("F0") + "%";
        target.text = popups_Panel.percentage.ToString() + "%";
        if(popups_Panel.passed)
            current.color = Color.green;
        else
            current.color = Color.red;
        //After the end
        Invoke("Exit", 1f);
    }
    public void Exit()
    {
        playerScript.canMove = true;
        playerScript.lookSpeed = playerScript.lookSpeedBase;

        if (transform.parent.name == "Monitor_CanvasL")
        transform.parent.parent.parent.GetComponent<MonitorsStages>().ResetMinigameL();
        else
            transform.parent.parent.parent.GetComponent<MonitorsStages>().ResetMinigameR();
    }
}
