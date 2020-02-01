using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellView : SpaceView
{
    public ShellController sc;
    public double max_dis = 1000; //max distance, when missile will be destroyed
    Vector3 sR;
    double dis = 0;

    // Start is called before the first frame update
    void Start()
    {
        sc = new ShellController();
        sR = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.dis = Vector3.Distance(this.sR, transform.position);
        if (this.dis > this.max_dis) //destroy missile on max distance
        {
            this.sc.shell_destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.sc.shell_hit(gameObject, collision);
    }
}
