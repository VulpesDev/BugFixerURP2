using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Results : MonoBehaviour
{
    PopUps popups_Panel;
    Text current, target;

    private void Awake()
    {
        current = transform.GetChild(1).GetComponent<Text>();
        target = transform.GetChild(2).GetComponent<Text>();
        current.text = "";
        target.text = "";
    }
    void Start()
    {
        popups_Panel = transform.parent.GetChild(2).GetComponent<PopUps>();
        current.text = popups_Panel.SuccessRate().ToString("F0") + "%";
        target.text = popups_Panel.percentage.ToString() + "%";
        if(popups_Panel.passed)
            current.color = Color.green;
        else
            current.color = Color.red;
        Invoke("TurnOff", 1.5f);
    }
    void TurnOff()
    {
        transform.parent.GetComponent<MonitorManager>().TurnMonitor("Off");
    }
}
