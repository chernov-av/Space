using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : SpaceModel
{
    private Vector3 enemy_R;
    private Vector3 enemy_V;
    private Vector3 enemy_A;

    private double armor;
    private double shield;

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

    public void count_laser_damage(double damage)
    {
        if (this.shield > 0)
        {
            this.shield -= damage;
        }
        else
        {
            this.armor -= damage * 10;
        }
    }

    public void count_energy_damage(double damage)
    {
        if (this.shield > 0)
        {
            this.shield -= damage;
        }
    }

    public Vector3 Enemy_R
    {
        get { return this.enemy_R; }
        set { this.enemy_R = value; }
    }

    public Vector3 Enemy_V
    {
        get { return this.enemy_V; }
        set { this.enemy_V = value; }
    }

    public Vector3 Enemy_A
    {
        get { return this.enemy_A; }
        set { this.enemy_A = value; }
    }

    public double Armor
    {
        get { return this.armor; }
        set { this.armor = value; }
    }

    public double Shield
    {
        get { return this.shield; }
        set { this.shield = value; }
    }
}
