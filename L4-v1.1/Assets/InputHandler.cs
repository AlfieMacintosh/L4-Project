/* InputHandler.cs*/
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
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            UnityEngine.Debug.Log("Cube was clicked");
        }
        else if (e.target.name == "Overlay_button")
        {
            sceneTransitionManager.GoToScene(1);
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            UnityEngine.Debug.Log("Cube was entered");
        }
        else if (e.target.name == "Button")
        {
            UnityEngine.Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            UnityEngine.Debug.Log("Cube was exited");
        }
        else if (e.target.name == "Button")
        {
            UnityEngine.Debug.Log("Button was exited");
        }
    }
}