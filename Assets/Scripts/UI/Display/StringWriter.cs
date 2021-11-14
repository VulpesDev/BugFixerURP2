using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringWriter : MonoBehaviour
{
    Text text;
    [SerializeField] int lettersToType;
    [SerializeField]float latency = 0.01f, delay = 0.0f;
    string input;
    public bool ready = false;
    void Start()
    {
        text = GetComponent<Text>();
        input = text.text; // gets the input
        text.text = ""; // clears the text box
        Invoke("InitiateWrite", delay); // starts write function in "delay" seconds
    }

    void InitiateWrite()
    {
        StartCoroutine(Write());
    }

    IEnumerator Write()
    {
        int letters = 0;  // letters writen
        foreach (var letter in input)
        {
            text.text += letter;  // writes letter
            letters++; // adds 1 to letters writen
            if (letters >= lettersToType) // if it's written the required number of letters it waits and resets
            {
                yield return new WaitForSeconds(latency);
                letters = 0;

                if (name != "LoaderText")
                MusicManager.TypeUI();
            }
        }
        ready = true;
    }
}
