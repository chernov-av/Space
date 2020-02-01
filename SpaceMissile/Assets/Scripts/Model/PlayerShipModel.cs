using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipModel : SpaceModel
{
    double armor;
    double shield;
    double ammo;
    double energy;
    float shootspeed=0.1f;

    public PlayerShipModel(double armor,double shield,double ammo, double energy)
    {
        this.armor = armor;
        this.shield = shield;
        this.ammo = ammo;
        this.energy = energy;
    }

    public void reduce_ammo()
    {
        this.ammo -= 1;
    }
   
    public double get_ammo()
    {
        return this.ammo;
    }

    public double get_armor()
    {
        return this.shield;
    }

    public double get_shield()
    {
        return this.shield;
    }

    public double get_energy()
    {
        return this.energy;
    }
    public float get_shootspeed()
    {
        return this.shootspeed;
    }
}
