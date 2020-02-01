using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : SpaceController
{
    PlayerShipModel psm;

    public PlayerShipController(double arm, double sh, double am, double en)
    {
        psm = new PlayerShipModel(arm, sh, am, en);

    }

    public double get_ammo()
    {
        return psm.get_ammo();
    }

    public double get_armor()
    {
        return psm.get_armor();
    }

    public double get_shield()
    {
        return psm.get_shield();
    }

    public double get_energy()
    {
        return psm.get_energy();
    }

    public PlayerShipModel get_psm()
    {
        return this.psm;
    }
}
