using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerShipView : SpaceElement
{
    public PlayerShipController psc;
    public double armor;
    public double shield;
    public int ammo;
    public double energy;
    public int missiles;
    public Slider SliderArmor;
    public Slider SliderShield;
    public Slider SliderEnergy;
    public Text TextMode;
    public Text TextAmmo;
    WeaponController wc;
    // Start is called before the first frame update
    void Start()
    {
        this.psc = gameObject.AddComponent<PlayerShipController>();
        this.psc.set_playerShipModelObject(gameObject, this.armor, this.shield, this.ammo, this.energy, this.missiles);
        
        this.SliderArmor.maxValue = (float)this.armor;
        this.SliderShield.maxValue = (float)this.shield;
        this.SliderEnergy.maxValue = (float)this.energy;

        this.TextAmmo.text = this.ammo.ToString();
        wc = gameObject.GetComponent<WeaponController>();
        this.TextMode.text = wc.current_mode.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        this.ammo = psc.get_ammo();
        this.armor = psc.get_armor();
        this.shield = psc.get_shield();
        this.energy = psc.get_energy();
        this.missiles = psc.get_missiles();

        //UI
        this.SliderArmor.value = (float)this.armor;
        this.SliderShield.value = (float)this.shield;
        this.SliderEnergy.value = (float)this.energy;
        this.TextMode.text = wc.current_mode.ToString();
        switch (wc.current_mode)
        {
            case WeaponController.weapon_modes.MG:
                this.TextAmmo.text = this.ammo.ToString();
                break;
            case WeaponController.weapon_modes.Missiles:
                this.TextAmmo.text = this.missiles.ToString();
                break;
        }
        

    }
}
