using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextGeneration : MonoBehaviour
{
    [SerializeField] InputLine lineInput;
    public int errorLine;
    private void Awake()
    {
        if(name == "ErrorCodeText")
        GetComponent<Text>().text = GenerateError();
    }

    string GenerateError()
    {
        string text;
        string errorCode = "";
        errorLine = UnityEngine.Random.Range(0, 100);
        int numbers = 4;

        for (int i = 0; i < numbers; i++)
        {
            errorCode += UnityEngine.Random.Range(0,10);
        }
        //text = ">>Error at line " + errorLine + ", error code \"00x00" + errorCode + "\"";
        text = ">>Error at line " + errorLine;
        //pass the errorLine number to InputLine script
        lineInput.errorLine = errorLine.ToString();

        Debug.Log("Generated line: " + errorLine + " with error code: " + "00x00" + errorCode);

        return text;
    }
}
