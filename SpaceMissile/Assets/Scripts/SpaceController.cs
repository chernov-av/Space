using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : SpaceElement
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}

public class MissileController : SpaceController
{
    MissileModel mm;
    EnemyView target;
    Vector3 target_R = new Vector3(float.NaN,float.NaN,float.NaN);
    Vector3 target_V;

    public MissileController(Vector3 mR, Vector3 mV, Vector3 mA)
    {
        this.mm = new MissileModel(mR, mV, mA);
    }

    public void missile_movement(GameObject gameobject, bool guide)
    {
        if (target != null)
        {
            this.target_R = target.eR;
            this.target_V = target.eV;
        }
        else
        {
            this.target_R = new Vector3(float.NaN, float.NaN, float.NaN);
            this.target_V = new Vector3(float.NaN, float.NaN, float.NaN);
        }
        gameobject.transform.position = this.mm.move_missile(this.mm.missile_R, this.mm.missile_V, this.target_R, this.target_V, guide);
        if (this.mm.missile_V != Vector3.zero)
        {
            gameobject.transform.rotation = Quaternion.LookRotation(this.mm.missile_V);
        }
    }

    public double get_dis()
    {
        return this.mm.distance_traveled;
    }

    public void set_V(Vector3 V)
    {
        this.mm.missile_V = V;
    }

    public Vector3 get_V()
    {
        return this.mm.missile_V;
    }

    public void set_R(Vector3 R)
    {
        this.mm.missile_R = R;
    }

    public void set_target(EnemyView target)
    {
        this.target = target;
        this.target_R = target.eR;
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
        Object exp = Instantiate(Resources.Load("BigExplosionEffect"), gameObject.transform.position, gameObject.transform.rotation);
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
        }
        Object exp = Instantiate(Resources.Load("BigExplosionEffect"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
}

public class ShellController: SpaceController
{
    ShellModel sm;
    public ShellController()
    {
        sm = new ShellModel();
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
            // Destroy(collision.gameObject);
            EnemyView enemy = collision.gameObject.GetComponent<EnemyView>();
            //enemy.ec.destroy(collision.gameObject);
            ShellView sw = gameObject.GetComponent<ShellView>();
            enemy.ec.hit(gameObject, sw.sc.get_damage());
        }
        Object exp = Instantiate(Resources.Load("BigExplosionEffect"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
}

public class EnemyController : SpaceController
{
    EnemyModel em;
    public EnemyController(Vector3 eR, Vector3 eV, Vector3 eA,double eArm,double eSh)
    {
        this.em = new EnemyModel(eR, eV, eA, eArm, eSh);
    }

    public void enemy_movement()
    {

    }

    public double get_armor()
    {
        return this.em.armor;
    }

    public double get_shield()
    {
        return this.em.shield;
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
        }
    }

    public void destroy(GameObject gameObject)
    {
        Object exp = Instantiate(Resources.Load("BigExplosionEffect"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);

    }
}