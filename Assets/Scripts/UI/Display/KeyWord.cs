using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class KeyWord : MonoBehaviour
{
    [HideInInspector]public float speed = 150;
    public bool ready, success;
    KeyCode requested;
    RectTransform rtr_Loading;
    Text frontText;



    private void Start()
    {
        frontText = transform.GetChild(0).GetComponent<Text>();
        rtr_Loading = transform.GetChild(1).GetComponent<RectTransform>();
        string key = RandomString(1);
        requested = string2Keys[key];
        frontText.text = key;
        Debug.Log(key + " " + requested);

        MusicManager.PopUpUI();
    }

    float rvelocity = 0;

    void Update()
    {
        Timer();
        CheckForInput();

        if (ready && success)
        {
            if (GetComponent<Image>().color != Color.blue)
            {
                GetComponent<Image>().color = Color.blue;

                MusicManager.CorrectUI();
            }
        }
        else if (ready && !success)
        {
            if (GetComponent<Image>().color != Color.red)
            {
                GetComponent<Image>().color = Color.red;

                MusicManager.IncorrectUI();
            }
        }
    }

    const int deltaSizeMinimum = -285;
    void Timer()
    {
        if (ready) return;
        //Debug.Log(rtr_Loading.offsetMax.x);
        if (rtr_Loading.offsetMax.x <= deltaSizeMinimum)
        {
            Debug.Log("Time's up: " + rtr_Loading.offsetMax.x);
            //times up
            success = false;
            ready = true;
        }
        else
        {
            rvelocity -= speed * Time.deltaTime;
            rtr_Loading.offsetMax = new Vector2(rvelocity, rtr_Loading.offsetMax.y);
        }
    }

    void CheckForInput()
    {
        if (ready) return;
        if (Input.anyKey)
        {
            foreach (KeyCode lastHitKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(lastHitKey))
                {
                    if (requested == lastHitKey)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                    ready = true;
                }
            }
            Debug.Log("Ready: " + ready + "  Succeeded: " + success);
        }
    }

    private static System.Random random = new System.Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    Dictionary<string, KeyCode> string2Keys = new Dictionary<string, KeyCode>()
    {
        {"0", KeyCode.Alpha0 },
        {"1", KeyCode.Alpha1 },
        {"2", KeyCode.Alpha2 },
        {"3", KeyCode.Alpha3 },
        {"4", KeyCode.Alpha4 },
        {"5", KeyCode.Alpha5 },
        {"6", KeyCode.Alpha6 },
        {"7", KeyCode.Alpha7 },
        {"8", KeyCode.Alpha8 },
        {"9", KeyCode.Alpha9 },
        {"A", KeyCode.A },
        {"B", KeyCode.B },
        {"C", KeyCode.C },
        {"D", KeyCode.D },
        {"E", KeyCode.E },
        {"F", KeyCode.F },
        {"G", KeyCode.G },
        {"H", KeyCode.H },
        {"I", KeyCode.I },
        {"J", KeyCode.J },
        {"K", KeyCode.K },
        {"L", KeyCode.L },
        {"M", KeyCode.M },
        {"N", KeyCode.N },
        {"O", KeyCode.O },
        {"P", KeyCode.P },
        {"Q", KeyCode.Q },
        {"R", KeyCode.R },
        {"S", KeyCode.S },
        {"T", KeyCode.T },
        {"U", KeyCode.U },
        {"V", KeyCode.V },
        {"W", KeyCode.W },
        {"X", KeyCode.X },
        {"Y", KeyCode.Y },
        {"Z", KeyCode.Z },
    };
}
