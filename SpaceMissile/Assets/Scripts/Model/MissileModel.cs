using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileModel : SpaceElement
{
    private Vector3 missile_R;
    private Vector3 missile_V;
    private Vector3 missile_A;
    private Vector3 last_position;
    private Vector3 U;

    private double distance_traveled = 0;
    private double damage = 500;
    private float timeError = 0.1f;
    /*
    public MissileModel(Vector3 missile_R, Vector3 missile_V, Vector3 missile_A)
    {
        this.missile_R = missile_R;
        this.missile_V = missile_V;
        this.missile_A = missile_A;
        this.last_position = missile_R;
    }*/

    public Vector3 move_missile(Vector3 mR, Vector3 mV, Vector3 tR, Vector3 tV, bool guide)
    {
        if ((guide) && (!float.IsNaN(tR.x)))
        {
            U = this.guide_missile(mR, mV, tR, tV); //count control signal
        }
        else
        {
            U = Vector3.zero;
        }

        this.missile_V = mV + U / timeError * Time.deltaTime; //count velocity
        this.missile_R = mR + this.missile_V * Time.deltaTime; //count position
        this.distance_traveled += Vector3.Distance(this.missile_R, this.last_position); //count distance
        this.last_position = this.missile_R;
        return missile_R;
    }

    Vector3 guide_missile(Vector3 mR, Vector3 mV, Vector3 tR, Vector3 tV)
    {
        Guidance gd = new Guidance(mR, mV, tR, tV);
        Vector3 u = gd.get_U(); //call function for control signal counting
        return u;
    }

    public double get_damage()
    {
        return this.damage;
    }

    public Vector3 Missile_V
    {
        get { return this.missile_V; }
        set { this.missile_V = value; }
    }

    public Vector3 Missile_R
    {
        get { return this.missile_R; }
        set { this.missile_R = value; }
    }
    public Vector3 Missile_A
    {
        get { return this.missile_A; }
        set { this.missile_A = value; }
    }

    public Vector3 Last_position
    {
        get { return this.last_position; }
        set { this.last_position = value; }
    }

    public double Distance_traveled
    {
        get { return this.distance_traveled; }
        set { this.distance_traveled = value; }
    }
}
