using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : SpaceElement
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        /*if (app.view.enemy != null)
        {
            app.model. = app.view.enemy.transform.position;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (app.view.enemy != null)
        {
            app.model.target_R = app.view.enemy.transform.position;
        }
        else
        {
            app.model.target_R.x = float.NaN;
            app.model.target_R.y = float.NaN;
            app.model.target_R.z = float.NaN;
        }*/
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
    }

    public Vector3 get_target_R()
    {
        return this.target_R;
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
            Destroy(collision.gameObject);
        }
        Object exp = Instantiate(Resources.Load("BigExplosionEffect"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
}

public class EnemyController : SpaceController
{
    EnemyModel em;
    public EnemyController(Vector3 eR, Vector3 eV, Vector3 eA)
    {
        this.em = new EnemyModel(eR, eV, eA);
    }

    public void enemy_movement()
    {

    }
}