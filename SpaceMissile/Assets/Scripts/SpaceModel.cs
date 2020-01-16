using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceModel : SpaceElement
{
    //Data
    public Vector3 target_R;
    public Vector3 target_V;
    public Vector3 missile_R;
    public Vector3 missile_V;
    public Vector3 U;

    public Vector3 guide_missile(Vector3 mR, Vector3 mV, Vector3 tR, Vector3 tV)
    {
        Guidance gd = new Guidance(mR, mV, tR, tV);
        U = gd.get_U();
        mV = mV + U * Time.deltaTime;
        missile_V = mV;
        mR = mR + mV * Time.deltaTime;
        return mR;
    }

}
