using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : SpaceController
{
    public GameObject Missile;
    public GameObject Bullet;
    public LayerMask mask;
    float ShootTimer = 0.0f;
    float EnergyLaserTimer = 0.0f;
    public float shooringRange;

    public enum weapon_modes { MG=1, Missiles=2, Laser=3, Energy=4 };
    public weapon_modes current_mode = weapon_modes.MG;
    int numModes = System.Enum.GetValues(typeof(weapon_modes)).Length;

    

    // Update is called once per frame
    void Update()
    {
       
        //depending on current weapon
        switch (this.current_mode)
        {
            case weapon_modes.MG:
                //shoot mg
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    this.shoot_bullet();
                }
                //timer for mg
                if (this.ShootTimer > 0)
                {
                    this.ShootTimer -= Time.deltaTime;
                }
                break;

            case weapon_modes.Missiles:
                //fix target for missile
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    this.fix_target(Input.mousePosition);
                }
                //launch missile
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    this.launch_missile();
                }
                break;

            case weapon_modes.Laser:
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    this.shoot_laser();
                }
                if (this.EnergyLaserTimer > 0)
                {
                    this.EnergyLaserTimer -= Time.deltaTime;
                }
                break;

            case weapon_modes.Energy:

                break;
        }
        //Reloading
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.reload();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            current_mode = weapon_modes.MG;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            current_mode = weapon_modes.Missiles;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            current_mode = weapon_modes.Laser;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            current_mode = weapon_modes.Energy;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            current_mode += 1;
            if ((int)current_mode == numModes + 1) current_mode = weapon_modes.MG;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            current_mode -= 1;
            if ((int)current_mode == 0) current_mode = weapon_modes.Energy;
        }      
    }
         
    void reload()
    {        
        var spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");

        var missile_1 = Instantiate(Missile, spaceship[0].transform.position + new Vector3(2.041f, -1.664f, -2.726f), spaceship[0].transform.rotation) as GameObject;
        missile_1.transform.parent = spaceship[0].transform;
        missile_1.transform.localPosition = new Vector3(-2.726f, -1.664f, -2.041f);
        var MissileJoint_1 = missile_1.GetComponent<FixedJoint>();
        MissileJoint_1.connectedBody = spaceship[0].GetComponent<Rigidbody>();
        var Rigidbody_1 = missile_1.GetComponent<Rigidbody>();
        Rigidbody_1.constraints = RigidbodyConstraints.FreezePosition;

        var missile_2 = Instantiate(Missile, spaceship[0].transform.position + new Vector3(2.041f, -1.664f, 2.726f), spaceship[0].transform.rotation) as GameObject;
        missile_2.transform.parent = spaceship[0].transform;
        missile_2.transform.localPosition = new Vector3(2.726f, -1.664f, -2.041f);
        var MissileJoint_2 = missile_2.GetComponent<FixedJoint>();
        MissileJoint_2.connectedBody = spaceship[0].GetComponent<Rigidbody>();
        var Rigidbody_2 = missile_2.GetComponent<Rigidbody>();
        Rigidbody_2.constraints = RigidbodyConstraints.FreezePosition;

        PlayerShipModel psm = spaceship[0].GetComponent<PlayerShipView>().psc.get_psm();
        psm.reload_ammo(500);
        psm.reload_missiles(2);
        psm.reload_energy(1000);

    }

    void shoot_bullet()
    {
        var spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");
        PlayerShipModel psm = spaceship[0].GetComponent<PlayerShipView>().psc.get_psm();
        
        if (psm.Ammo > 0 & this.ShootTimer<=0)
        {
            var xError = Quaternion.AngleAxis(Random.Range(-this.shooringRange, this.shooringRange), transform.up);
            var yError = Quaternion.AngleAxis(Random.Range(-this.shooringRange, this.shooringRange), transform.right);
            var shell = Instantiate(Bullet, spaceship[0].transform.position + new Vector3(8.0f, 0.0f, 0.0f), spaceship[0].transform.rotation*xError*yError) as GameObject;
            shell.transform.parent = spaceship[0].transform;
            shell.transform.localPosition = new Vector3(0.0f, 0.0f, 8.0f);
            shell.transform.parent = null;
            shell.GetComponent<Rigidbody>().AddForce(shell.transform.forward * 0.5f, ForceMode.Impulse);
            psm.reduce_ammo();
            this.ShootTimer = psm.get_shootspeed();
        }
    }

    void shoot_laser()
    {
        var spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");
        PlayerShipModel psm = spaceship[0].GetComponent<PlayerShipView>().psc.get_psm();

        if (psm.Energy > 0 & this.EnergyLaserTimer <= 0)
        {
            psm.reduce_energy();
            this.EnergyLaserTimer = psm.get_energylaserreduction();
        }
    }

    void launch_missile()
    {
        //get list of missiles
        List<GameObject> missiles = new List<GameObject>();
        missiles.AddRange(GameObject.FindGameObjectsWithTag("Missile"));

        var spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");
        PlayerShipModel psm = spaceship[0].GetComponent<PlayerShipView>().psc.get_psm();

        //removes all already launched missiles
        int i = 0;
        while (i<missiles.Count)
        {
            if (missiles[i].transform.parent != spaceship[0].transform)
            {
                missiles.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        //Launch next missile
        if (missiles.Count > 0)
        {
            //destroy joint
            var MissileJoint = missiles[0].GetComponent<FixedJoint>();
            Destroy(MissileJoint);
            //unattach from parent
            missiles[0].transform.parent = null;

            //set missile velocity and positions
            MissileView missile = missiles[0].GetComponent<MissileView>();
            missile.guide = true;
            Vector3 Aeu = missile.transform.rotation * Vector3.forward;
            Vector3 V = Aeu * 150;
            missile.mc.set_V(V);
            missile.mc.set_R(missiles[0].transform.position);
            //toggle fire
            var missile_fire = missile.transform.Find("fire");
            missile_fire.gameObject.SetActive(true);
            //toggle smoke
            var missile_smoke = missile.transform.Find("smoke");
            missile_smoke.gameObject.SetActive(true);
            //toggle camera
            var missile_camera = missile.transform.Find("MissileCamera");
            missile_camera.gameObject.SetActive(true);
            psm.reduce_missiles();
        }
    }

    void fix_target(Vector3 mousePos)
    {
        //get list of missiles
        List<GameObject> missiles = new List<GameObject>();
        missiles.AddRange(GameObject.FindGameObjectsWithTag("Missile"));
        var spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");
        //removes all already launched missiles
        int i = 0;
        while (i < missiles.Count)
        {
            if (missiles[i].transform.parent != spaceship[0].transform)
            {
                missiles.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
        //ray to mouse position to fix target
        //Ray ray = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, 10000))

        Vector3 startPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2, 0));
        startPos.z = Camera.main.transform.position.z;

        Vector3 dir = Camera.main.transform.TransformDirection(Vector3.forward);

        if (Physics.SphereCast(startPos, 10.0f, dir, out hit, 1000, this.mask))
        {
            print(hit.transform.name);
            GameObject target = hit.collider.gameObject;
            if (missiles.Count > 0)
            {
                //set target coords
                MissileView missile = missiles[0].GetComponent<MissileView>();
                missile.mc.set_target(target);
            }
        }
    }


}
