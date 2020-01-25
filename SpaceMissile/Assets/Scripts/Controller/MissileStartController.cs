using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileStartController : SpaceController
{
    
    // Update is called once per frame
    void Update()
    {
        // if mouse leftkey pressed? call fire
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.fire();
        }

        //if mouse rightkey pressed fix target
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            this.fix_target();
        }
    }

    void fire()
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
