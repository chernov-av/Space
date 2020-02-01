using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : SpaceModel
{
    public Vector3 enemy_R;
    public Vector3 enemy_V;
    public Vector3 enemy_A;

    public double armor;
    public double shield;

    public EnemyModel(Vector3 enemy_R, Vector3 enemy_V, Vector3 enemy_A, double armor, double shield)
    {
        this.enemy_R = enemy_R;
        this.enemy_V = enemy_V;
        this.enemy_A = enemy_A;
        this.armor = armor;
        this.shield = shield;
    }

    public void count_missile_damage(double damage)
    {
        this.armor -= damage;
    }

    public void count_shell_damage(double damage)
    {
        this.armor -= damage;
    }
  
}
