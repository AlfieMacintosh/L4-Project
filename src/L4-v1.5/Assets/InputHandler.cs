using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class InputHandler : MonoBehaviour
{
    public SceneTransitionManager sceneTransitionManager;
    public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "testing_button")
        {
            sceneTransitionManager.GoToScene(1);
        }
        else if (e.target.name == "Overlay_button")
        {
            sceneTransitionManager.GoToScene(2);
        }
    }
}