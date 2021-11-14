using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputLine : MonoBehaviour
{
    public string errorLine;
    InputField inputF;
    public bool ready = false, correct;
    void Start()
    {
        inputF = GetComponent<InputField>();
    }
    void Update()
    {
        inputF.Select();
    }
    public void OnSubmit()
    {
        string input = inputF.text;
        if(input == errorLine)
        {
            //Successful
            Debug.Log("Successful line");
            correct = true;
        }
        else
        {
            //NotSuccessful
            Debug.Log("Denied");
            correct = false;
        }
        ready = true;
    }
}
