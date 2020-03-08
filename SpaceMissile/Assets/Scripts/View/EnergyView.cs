using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyView : MonoBehaviour
{
    public EnergyController ec;
    public double max_dis = 500; //max distance, when missile will be destroyed
    public double dis = 0;
    Vector3 eR;

    // Start is called before the first frame update
    void Start()
    {
        //sc = new ShellController();
        ec = gameObject.AddComponent<EnergyController>();
        ec.set_laserModelObject(gameObject);
        eR = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.dis = Vector3.Distance(this.eR, transform.position);
        if (this.dis > this.max_dis) //destroy missile on max distance
        {
            this.ec.energy_shell_destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");

        this.ec.energy_hit(gameObject, collision);
    }
}
