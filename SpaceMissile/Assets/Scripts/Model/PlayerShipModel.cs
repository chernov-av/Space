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

    public PlayerShipModel(double armor,double shield,int ammo, double energy, int missiles)
    {
        this.armor = armor;
        this.shield = shield;
        this.ammo = ammo;
        this.energy = energy;
        this.missiles = missiles;
    }

    public void reduce_ammo()
    {
        this.ammo -= 1;
    }
   
    public int get_ammo()
    {
        return this.ammo;
    }

    public void reduce_missiles()
    {
        this.missiles -= 1;
    }

    public int get_missiles()
    {
        return this.missiles;
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

    public void reduce_energy()
    {
        this.energy -= 1;
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
