using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserView : MonoBehaviour
{
    public LaserController lc;
    public double max_dis = 1000; //max distance, when missile will be destroyed
    Vector3 lR;
    public double dis = 0;

    // Start is called before the first frame update
    void Start()
    {
        //sc = new ShellController();
        lc = gameObject.AddComponent<LaserController>();
        lc.set_laserModelObject(gameObject);
        lR = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void hit(GameObject targetObject)
    {
        Debug.Log("HIT");

        this.lc.laser_hit(gameObject, targetObject);
    }
}
