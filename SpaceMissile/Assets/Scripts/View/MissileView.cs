using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileView : SpaceElement
{
    public bool guide;
    public MissileController mc;
    public Vector3 mV; //velocity
    public Vector3 mR; //coordinates
    public Vector3 mA; //angle
    public double max_dis = 1000; //max distance, when missile will be destroed
    public double dis=0;
    
    // Start is called before the first frame update
    void Start()
    {
        //Controller object creation
        this.mR = transform.position;
        this.mA = transform.rotation.eulerAngles;
        this.mc = new MissileController(this.mR, this.mV, this.mA);
    }

    // Update is called once per frame
    void Update()
    {
        this.mR = transform.position;
        if (guide) //if guidance on, start move
        {
            this.mc.missile_movement(gameObject, guide);
            this.mc.missile_rotation(gameObject);
            this.dis = this.mc.get_dis();
            if (this.dis > this.max_dis) //destroy missile on max distance
            {
                this.mc.missile_collapse(gameObject);
            }
        }
       
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");
        this.mc.missile_collapse(gameObject, collision);       
    }
}
