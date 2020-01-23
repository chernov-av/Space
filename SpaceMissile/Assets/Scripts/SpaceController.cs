using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : SpaceElement
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        if (app.view.target != null)
        {
            app.model.target_R = app.view.target.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (app.view.target != null)
        {
            app.model.target_R = app.view.target.transform.position;
        }
        else
        {
            app.model.target_R.x = float.NaN;
            app.model.target_R.y = float.NaN;
            app.model.target_R.z = float.NaN;
        }
    }


}

public class MissileController : SpaceController
{
    MissileModel mm;
    

    public MissileController(Vector3 mR, Vector3 mV, Vector3 mA)
    {
        this.mm = new MissileModel(mR, mV, mA);
    }

    void Update()
    {
        /*if (app.view.missile != null)
        {
            mm.missile_R = app.view.missile.transform.position;
        }
        */

    }
    public void missile_movement(GameObject gameobject, bool guide)
    {
        gameobject.transform.position = this.mm.move_missile(this.mm.missile_R, this.mm.missile_V, app.model.target_R, app.model.target_V, guide);      
    }

    public double get_dis()
    {
        return this.mm.distance_traveled;
    }

    public void missile_rotation(GameObject gameObject)
    {
        if (this.mm.missile_V != Vector3.zero)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(this.mm.missile_V);
        }
        
    }

    public void set_V(Vector3 V)
    {
        this.mm.missile_V = V;
    }

    public void set_R(Vector3 R)
    {
        this.mm.missile_R = R;
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
        if (collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject);
        }
        Object exp = Instantiate(Resources.Load("BigExplosionEffect"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
}