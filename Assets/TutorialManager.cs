using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static int stage;
    public static bool once = false;
    [SerializeField] TextMeshProUGUI tutorialTxt;
    VideoPlayer vidplayer;

    private void Start()
    {
        vidplayer = GetComponent<VideoPlayer>();
    }
    void FixedUpdate()
    {
        if (once) return;
        switch(stage)
        {
            case 0:
                tutorialTxt.text = "You can move with w,a,s,d and jump with space. Look around with the mouse.";
                vidplayer.clip = Resources.Load("UI/TutorialVideos/BasicJump") as VideoClip;
                break;
            case 1:
                tutorialTxt.text = "You can double jump as well.";
                vidplayer.clip = Resources.Load("UI/TutorialVideos/DoubleJumps") as VideoClip;
                break;
            case 2:
                tutorialTxt.text = "You can dash in the air with left shift.";
                vidplayer.clip = Resources.Load("UI/TutorialVideos/JumpDash") as VideoClip;
                break;
            case 3:
                tutorialTxt.text = "You can see green jump pads around the map. Just step on them.";
                vidplayer.clip = Resources.Load("UI/TutorialVideos/JumpPad") as VideoClip;
                break;
            case 4:
                tutorialTxt.text = "You can slide with left control (lctrl).";
                vidplayer.clip = Resources.Load("UI/TutorialVideos/Slide") as VideoClip;
                break;
            case 5:
                tutorialTxt.text = "Shoot green targets to release platform.";
                vidplayer.clip = Resources.Load("UI/TutorialVideos/Target") as VideoClip;
                break;
        }
        once = true;
    }
}
