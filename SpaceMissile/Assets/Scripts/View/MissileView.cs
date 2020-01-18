using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileView : SpaceElement
{
    public bool guide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (guide)
        {
            case true:
                app.controller.missile_movement();
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");
       // app.controller.missile_collapse(collision);
       //РАЗОБРАТЬ, КАК УДАЛЯТЬ ОБЪЕКТЫ ИЗ КОНТРОЛЛЕРА
        if (collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
