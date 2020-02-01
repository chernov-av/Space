using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : SpaceController
{
    public GameObject Missile;
    public GameObject Bullet;
    float ShootTimer = 0.0f;    
    // Update is called once per frame
    void Update()
    {
        //if mouse rightkey pressed fix target
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.fix_target();
        }
        
        // if mouse leftkey pressed? call fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.launch_missile();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            this.shoot_bullet();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.reload();
        }

        if (this.ShootTimer > 0)
        {
            this.ShootTimer -= Time.deltaTime;
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
    }

    void shoot_bullet()
    {
        var spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");
        PlayerShipModel psm = spaceship[0].GetComponent<PlayerShipView>().psc.get_psm();
        
        if (psm.get_ammo() > 0 & this.ShootTimer<=0)
        {
            var shell = Instantiate(Bullet, spaceship[0].transform.position + new Vector3(8.0f, 0.0f, 0.0f), spaceship[0].transform.rotation) as GameObject;
            shell.transform.parent = spaceship[0].transform;
            shell.transform.localPosition = new Vector3(0.0f, 0.0f, 8.0f);
            shell.transform.parent = null;
            shell.GetComponent<Rigidbody>().AddForce(transform.forward * 0.01f, ForceMode.Impulse);
            psm.reduce_ammo();
            this.ShootTimer = psm.get_shootspeed();
        }
    }

    void launch_missile()
    {
        //get list of missiles
        List<GameObject> missiles = new List<GameObject>();
        missiles.AddRange(GameObject.FindGameObjectsWithTag("Missile"));

        var spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");

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
            Vector3 V = Aeu * 50;
            missile.mc.set_V(V);
            missile.mc.set_R(missiles[0].transform.position);
            //toggle fire
            var missile_fire = missile.transform.Find("fire");
            missile_fire.gameObject.SetActive(true);
            //toggle smoke
            var missile_smoke = missile.transform.Find("smoke");
            missile_smoke.gameObject.SetActive(true);
        }
    }

    void fix_target()
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


        //get list of targets
        List<GameObject> targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        //set target for missile
        if (missiles.Count > 0)
        {
            //set target coords
            MissileView missile = missiles[0].GetComponent<MissileView>();
            EnemyView target = targets[0].GetComponent<EnemyView>();
            missile.mc.set_target(target);
        }
    }
}
