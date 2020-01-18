using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileView : SpaceElement
{
    public bool guide;
    MissileController mc;
    public Vector3 mV;
    public Vector3 mR;
    public double dis=0;
    
    // Start is called before the first frame update
    void Start()
    {
        this.mR = transform.position;
        this.mc = new MissileController(this.mR, this.mV);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.mc.missile_movement(guide);
        transform.rotation = this.mc.missile_rotation();
        this.dis = this.mc.get_dis();
        if (this.dis>200)
        {
            this.mc.missile_collapse(gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");
        this.mc.missile_collapse(gameObject, collision);       
    }
}
