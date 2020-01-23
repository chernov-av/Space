using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guidance
{
    Vector3 vizir_R;
    Vector3 vizir_V;
    Vector3 U;
    Vector3 missile_R;
    Vector3 missile_V;
    Vector3 target_R;
    Vector3 target_V;
    Vector3 Vn;
    float k = 3;

    //Guidance. PPN^ control vector is orthogonal to missile velocity vector

    public Guidance(Vector3 mR, Vector3 mV, Vector3 tR, Vector3 tV)
    {
        missile_R = mR;
        target_R = tR;
        missile_V = mV;
        target_V = tV;
    }

    public Vector3 get_U()
    {
        vizir_R = target_R - missile_R;
        vizir_V = target_V - missile_V;

        float T = Vector3.Dot(vizir_V, vizir_R);
        T = T / Mathf.Pow((Vector3.Magnitude(vizir_R)),2);

        Vn = vizir_V - T*vizir_R;

        float omega_modul = Vector3.Magnitude(Vn) / Vector3.Magnitude(vizir_R);

        Vector3 Vn_norm = Vector3.Normalize(Vn);
        Vector3 vizir_norm = Vector3.Normalize(vizir_R);

        Vector3 omega_norm = Vector3.Cross(vizir_norm, Vn_norm);

        Vector3 omega = omega_norm * omega_modul;

        U = k * Vector3.Cross(omega, missile_V);

        return U;
    }
}
