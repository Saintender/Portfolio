using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

    //Array of layers from the enum in priority order
    public Layer[] layerStack =
    {
        Layer.Button,
        Layer.Stack,
        Layer.Card,
        Layer.Board
    };

    //"Distance" raycast must go before assuming endstop
    [SerializeField] float endStopDistance = 100f;

    //The following are initializers and getters for the necessary member variables
    Camera gameCamera;

    RaycastHit cursorHit;
    public RaycastHit Hit
    {
        get { return cursorHit; }
    }

    Layer cursorLayerHit;
    public Layer layerHit
    {
        get { return cursorLayerHit; }
    }
	
	void Start () {
        //Set the camera to the current game camera
        gameCamera = Camera.main;
	}
	
	void Update () {
		//Loop through layer stack in order to see if we're seeing that layer
        foreach(Layer layer in layerStack)
        {
            var hit = RaycastForLayer(layer); //Method down below
            if (hit.HasValue)
            {
                cursorHit = hit.Value;
                cursorLayerHit = layer;
                return;
            }
        }
        cursorHit.distance = endStopDistance;
        cursorLayerHit = Layer.EndStop;
	}

    //Method declaration to look for the current layer : ? means that it can return null
    RaycastHit? RaycastForLayer(Layer layer)
    {
        //layerMask is a bitshifted int for speed
        int layerMask = 1 << (int)layer;
        
        //stores the mouse position as a vector
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        //hit can be initialized without declaration due to the out modifier in the parameter below
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, endStopDistance, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
