using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : SpaceView
{
    public bool move;
    public EnemyController ec;
    public Vector3 eV; //velocity
    public Vector3 eR; //coordinates
    public Vector3 eA; //angle
    public double armor;
    public double shield;


    // Start is called before the first frame update
    void Start()
    {
        this.eR = transform.position;
        this.eA = transform.rotation.eulerAngles;
        this.ec = new EnemyController(this.eR, this.eV, this.eA,this.armor,this.shield);
    }

    // Update is called once per frame
    void Update()
    {
        this.eR = transform.position;
        this.eA = transform.rotation.eulerAngles;
        this.armor = ec.get_armor();
        this.shield = ec.get_shield();
        this.check_armor();
    }

    void check_armor()
    {
        if (this.armor <= 0)
        {
            this.ec.destroy(gameObject);
        }
    }
}
