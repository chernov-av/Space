using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : SpaceController
{
    PlayerShipModel psm;       

    public void set_playerShipModelObject(GameObject gameObject, double arm, double sh, int am, double en, int mis)
    {
        this.psm = gameObject.AddComponent<PlayerShipModel>();
        this.psm.Armor = arm;
        this.psm.Schield = sh;
        this.psm.Ammo = am;
        this.psm.Energy = en;
        this.psm.Missiles = mis;
    }

    public int get_ammo()
    {
        return psm.Ammo;
    }

    public double get_armor()
    {
        return psm.Armor;
    }

    public double get_shield()
    {
        return psm.Schield;
    }

    public double get_energy()
    {
        return psm.Energy;
    }
    public int get_missiles()
    {
        return psm.Missiles;
    }

    public PlayerShipModel get_psm()
    {
        return this.psm;
    }
}
