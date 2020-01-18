using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : SpaceElement
{
    public GameObject explosion;
    public double A;
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
    

    public MissileController(Vector3 mR, Vector3 mV)
    {
        this.mm = new MissileModel(mR, mV);
    }

    void Update()
    {
        /*if (app.view.missile != null)
        {
            mm.missile_R = app.view.missile.transform.position;
        }
        */

    }
    public Vector3 missile_movement(bool guide)
    {
        Vector3 pos = this.mm.move_missile(this.mm.missile_R, this.mm.missile_V, app.model.target_R, app.model.target_V, guide);

        //transform.rotation = Quaternion.LookRotation(this.mm.missile_V);
        return pos;
    }

    public double get_dis()
    {
        return this.mm.distance_traveled;
    }

    public Quaternion missile_rotation()
    {
        Quaternion rot = Quaternion.LookRotation(this.mm.missile_V);
        return rot;
    }

    public void missile_collapse(GameObject gameObject)
    {
        Object exp = Instantiate(Resources.Load("BigExplosionEffect"), gameObject.transform.position, gameObject.transform.rotation); 
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
    public void missile_collapse(GameObject gameObject, Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject);
        }
        Object exp = Instantiate(Resources.Load("BigExplosionEffect"), gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        Destroy(exp, 4.0f);
    }
}