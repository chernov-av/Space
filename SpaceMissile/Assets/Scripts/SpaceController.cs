using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceController : SpaceElement
{
    public float angle;
    protected Camera cam;
   
   // public float hitmarkshowtime;

}

public class MissileController : SpaceController
{
    MissileModel mm;
    GameObject target;
    Vector3 target_R = new Vector3(float.NaN,float.NaN,float.NaN);
    Vector3 target_V;

    public void set_missileModelObject(GameObject gameObject, Vector3 mR, Vector3 mV, Vector3 mA)
    {
        this.mm=gameObject.AddComponent<MissileModel>();
        this.set_R(mR);
        this.set_V(mV);
        this.set_A(mA);
        this.mm.Last_position = mR;
    }

    public void missile_movement(GameObject gameobject, bool guide)
    {
        if (target != null)
        {
            this.target_R = target.GetComponent<EnemyView>().eR;
            this.target_V = target.GetComponent<EnemyView>().eV;
            //List of view points and list of visibility of each point
            List<GameObject> viewPointList = new List<GameObject>();
            List<bool> viewPointVisibilityList = new List<bool>();
            //get viewpoints of the selected target
            foreach (Transform child in this.target.transform)
            {
                if (child.tag == "ViewPoint")
                {
                    viewPointList.Add(child.gameObject);
                    Vector3 targetDir = child.gameObject.transform.position - this.mm.Missile_R;
                    //get angle between camera and viewpoint
                    angle = Vector3.Angle(targetDir, gameobject.transform.forward);
                    
                    Camera cam = gameobject.GetComponentInChildren<Camera>();

                    if (angle < cam.fieldOfView / 2)
                    {
                        viewPointVisibilityList.Add(true);
                    }
                    else
                    {
                        viewPointVisibilityList.Add(false);
                    }
                }
            }

            if (viewPointVisibilityList.Contains(true))
            {
                guide = true;
            }
            else { guide = false; }            
        }
        else
        {
            this.target_R = new Vector3(float.NaN, float.NaN, float.NaN);
            this.target_V = new Vector3(float.NaN, float.NaN, float.NaN);
            guide = false;
        }
        //position of missile
        gameobject.transform.position = this.mm.move_missile(this.mm.Missile_R, this.mm.Missile_V, this.target_R, this.target_V, guide);
        //rotation of missile
        if (this.mm.Missile_V != Vector3.zero)
        {
            gameobject.transform.rotation = Quaternion.LookRotation(this.mm.Missile_V);
        }
    }

    public double get_dis()
    {
        return this.mm.Distance_traveled;
    }

    public void set_A(Vector3 A)
    {
        this.mm.Missile_A = A;
    }

    public void set_V(Vector3 V)
    {
        this.mm.Missile_V = V;
    }

    public Vector3 get_V()
    {
        return this.mm.Missile_V;
    }

    public void set_R(Vector3 R)
    {
        this.mm.Missile_R = R;
    }

    public void set_target(GameObject target)
    {
        this.target = target;
        this.target_R = target.GetComponent<EnemyView>().eR;
    }

    public Vector3 get_target_R()
    {
        return this.target_R;
    }

    public double get_damage()
    {
       return this.mm.get_damage(); 
    }

    public void missile_collapse(GameObject gameObject)
    {
        //without target
        Object exp = Instantiate(Resources.Load("Prefabs/WFX_ExplosionMissile"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
    public void missile_collapse(GameObject gameObject, Collision collision)
    {
        // on target hit
        if (collision.gameObject.tag == "Enemy")
        {
            // Destroy(collision.gameObject);
            EnemyView enemy = collision.gameObject.GetComponent<EnemyView>();
            //enemy.ec.destroy(collision.gameObject);
            MissileView mw = gameObject.GetComponent<MissileView>();
            enemy.ec.hit(gameObject,mw.mc.get_damage());

            GameObject.FindGameObjectsWithTag("View")[0].GetComponent<HitmarkerView>().getHitmarker();
        }
        Object exp = Instantiate(Resources.Load("Prefabs/WFX_ExplosionMissile"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
}

public class ShellController: SpaceController
{
    ShellModel sm;
    
    public void set_shellModelObject(GameObject gameObject)
    {
        this.sm = gameObject.AddComponent<ShellModel>();
    }

    public double get_damage()
    {
        return this.sm.get_damage();
    }

    public void shell_destroy(GameObject gameObject)
    {     
        Destroy(gameObject);        
    }
    public void shell_hit(GameObject gameObject, Collision collision)
    {
        // on target hit
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyView enemy = collision.gameObject.GetComponent<EnemyView>();
            ShellView sw = gameObject.GetComponent<ShellView>();
            enemy.ec.hit(gameObject, sw.sc.get_damage());
            GameObject.FindGameObjectsWithTag("View")[0].GetComponent<HitmarkerView>().getHitmarker();

        }
        Object exp = Instantiate(Resources.Load("Prefabs/WFX_ExplosionShell"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
}

public class EnergyController : SpaceController
{
    EnergyModel em;

    public void set_laserModelObject(GameObject gameObject)
    {
        this.em = gameObject.AddComponent<EnergyModel>();
    }

    public void energy_shell_destroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void energy_hit(GameObject gameObject, Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Destroy(collision.gameObject);
            EnemyView enemy = collision.gameObject.GetComponent<EnemyView>();
            //enemy.ec.destroy(collision.gameObject);
            EnergyView ew = gameObject.GetComponent<EnergyView>();
            enemy.ec.hit(gameObject, em.get_damage());

            GameObject.FindGameObjectsWithTag("View")[0].GetComponent<HitmarkerView>().getHitmarker();
        }
        Object exp = Instantiate(Resources.Load("Prefabs/WFX_ExplosionMissile"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
}

public class LaserController : SpaceController
{
    LaserModel lm;

    public void set_laserModelObject(GameObject gameObject)
    {
        this.lm = gameObject.AddComponent<LaserModel>();
    }

    public void laser_hit(GameObject gameObject, GameObject targetObject)
    {
        if (targetObject.tag == "Enemy")
        {
            EnemyView enemy = targetObject.GetComponent<EnemyView>();
            enemy.ec.hit(gameObject, lm.get_damage());
            GameObject.FindGameObjectsWithTag("View")[0].GetComponent<HitmarkerView>().getHitmarker();
        }
    }
}

public class EnemyController : SpaceController
{
    EnemyModel em;
    
    public void set_enemyObjectModel(GameObject gameObject, Vector3 eR, Vector3 eV, Vector3 eA, double eArm, double eSh)
    {
        this.em = gameObject.AddComponent<EnemyModel>();
        this.em.Enemy_A = eA;
        this.em.Enemy_V = eV;
        this.em.Enemy_R = eR;
        this.em.Armor = eArm;
        this.em.Shield = eSh;
    }

    public void enemy_movement()
    {

    }

    public double get_armor()
    {
        return this.em.Armor;
    }

    public double get_shield()
    {
        return this.em.Shield;
    }

    public void hit(GameObject gameObject, double damage)
    {
        switch (gameObject.tag)
        {
            case "Missile":
                em.count_missile_damage(damage);
                break;

            case "Shell":
                em.count_shell_damage(damage);
                break;

            case "Laser":
                em.count_laser_damage(damage);
                break;

            case "Energy":
                em.count_energy_damage(damage);
                break;
        }
    }

    public void destroy(GameObject gameObject)
    {
        Object exp = Instantiate(Resources.Load("Prefabs/WFX_ExplosiveSmokeSpaceShip"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);

    }
}

