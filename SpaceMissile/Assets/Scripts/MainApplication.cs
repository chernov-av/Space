using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApplication : MonoBehaviour
{
    public SpaceModel model;
    public SpaceView view;
    public SpaceController controller;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SpaceElement : MonoBehaviour
{
    public MainApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<MainApplication>();
        }
    }
}