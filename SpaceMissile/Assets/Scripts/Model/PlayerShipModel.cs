using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipModel : SpaceModel
{
    double armor;
    double shield;
    int ammo;
    int missiles;
    double energy;
    float shootspeed = 0.1f;
    float energylaserreduction = 0.01f;

   
    public void reduce_ammo()
    {
        this.ammo -= 1;
    }
   
    public int Ammo
    {
        get { return this.ammo; }
        set { this.ammo = value; }
    }

    public void reduce_missiles()
    {
        this.missiles -= 1;
    }

    public int Missiles
    {
        get { return this.missiles; }
        set { this.missiles = value; }
    }

    public double Armor
    {
        get { return this.armor; }
        set { this.armor = value; }
    }

    public double Schield
    {
        get { return this.shield; }
        set { this.shield = value; }
    }

    public double Energy
    {
        get { return this.energy; }
        set { this.energy = value; }
    }

    public void reduce_energy(double reduction)
    {
        this.energy -= reduction;
    }

    public float get_shootspeed()
    {
        return this.shootspeed;
    }

    public float get_energylaserreduction()
    {
        return this.energylaserreduction;
    }

    public void reload_missiles(int mis)
    {
        this.missiles = mis;
    }

    public void reload_ammo(int ammo)
    {
        this.ammo = ammo;
    }

    public void reload_energy(int en)
    {
        this.energy = en;
    }
}
