using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipView : SpaceElement
{
    public PlayerShipController psc;
    public double armor;
    public double shield;
    public double ammo;
    public double energy;
    // Start is called before the first frame update
    void Start()
    {
        psc = new PlayerShipController(this.armor, this.shield, this.ammo, this.energy);
    }

    // Update is called once per frame
    void Update()
    {
        this.ammo = psc.get_ammo();
        this.armor = psc.get_armor();
        this.shield = psc.get_shield();
        this.energy = psc.get_energy();
    }
}
