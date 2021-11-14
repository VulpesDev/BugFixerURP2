using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesManager : MonoBehaviour
{

    public bool ready()
    {
        switch (Cases())
        {
            case 0:
                if (transform.GetChild(1).GetComponent<StringWriter>().ready)
                    return true;
                else return false;
            case 1:
                
                if (GetComponentInChildren<InputLine>().ready)
                {
                    if (!transform.GetChild(1).GetComponent<InputLine>().correct)
                    {
                        transform.parent.GetChild(4).GetComponent<Results>().Exit();
                        //what to do if not correct
                    }
                    return true;
                }
                    
                else return false;
            case 2:
                if (GetComponent<PopUps>().ready)
                {
                    return true;
                }
                else return false;
            case 3:
                if (GetComponent<StatusManager>().ready)
                {
                    return true;
                }
                else return false;
            default:
                return false;
        }
    }

    void Exit()
    {

    }

    int Cases()
    {
        if (name == "FirstStage_Panel") return 0;
        else if (name == "SecondStage_Panel") return 1;
        else if (name == "PopUps_Panel") return 2;
        else if (name == "Status") return 3;
        else return 4;
    }
}
