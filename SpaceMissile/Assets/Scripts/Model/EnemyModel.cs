using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : SpaceModel
{
    public Vector3 enemy_R;
    public Vector3 enemy_V;
    public Vector3 enemy_A;

    public EnemyModel(Vector3 enemy_R, Vector3 enemy_V, Vector3 enemy_A)
    {
        this.enemy_R = enemy_R;
        this.enemy_V = enemy_V;
        this.enemy_A = enemy_A;
    }

  
}
