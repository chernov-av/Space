using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileModel : SpaceElement
{
    public Vector3 missile_R;
    public Vector3 missile_V;
    public double distance_traveled = 0;
    Vector3 last_position;
    Vector3 U;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public MissileModel(Vector3 missile_R, Vector3 missile_V)
    {
        this.missile_R = missile_R;
        this.missile_V = missile_V;
        this.last_position = missile_R;
    }

    public Vector3 move_missile(Vector3 mR, Vector3 mV, Vector3 tR, Vector3 tV, bool guide)
    {
        if ((guide) && (!float.IsNaN(tR.x)))
        {
            U = this.guide_missile(mR, mV, tR, tV);
        }
        else
        {
            U = Vector3.zero;
        }

        this.missile_V = mV + U * Time.deltaTime;
        this.missile_R = mR + this.missile_V * Time.deltaTime;
        this.distance_traveled += Vector3.Distance(this.missile_R, this.last_position);
        this.last_position = this.missile_R;
        return missile_R;
    }

    Vector3 guide_missile(Vector3 mR, Vector3 mV, Vector3 tR, Vector3 tV)
    {
        Guidance gd = new Guidance(mR, mV, tR, tV);
        Vector3 u = gd.get_U();
        return u;
    }
}
