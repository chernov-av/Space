using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : SpaceElement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (app.view.missile != null) 
        {
            app.model.missile_R = app.view.missile.transform.position;
        }

        if (app.view.target != null)
        {
            app.model.target_R = app.view.target.transform.position;
        }
    }

    public void missile_movement()
    {
        app.view.missile.transform.position = app.model.guide_missile(app.model.missile_R, app.model.missile_V, app.model.target_R, app.model.target_V);
        app.view.missile.transform.rotation = Quaternion.LookRotation(app.model.missile_V);
    }
    
    public void missile_collapse(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject);   
        }


    }
}
