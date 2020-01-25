using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : SpaceView
{
    public bool move;
    EnemyController ec;
    public Vector3 eV; //velocity
    public Vector3 eR; //coordinates
    public Vector3 eA; //angle

    // Start is called before the first frame update
    void Start()
    {
        this.eR = transform.position;
        this.eA = transform.rotation.eulerAngles;
        this.ec = new EnemyController(this.eR, this.eV, this.eA);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
