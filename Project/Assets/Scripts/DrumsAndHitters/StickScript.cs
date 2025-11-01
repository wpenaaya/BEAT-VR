using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickScript : AbstractDrumHitter
{
    protected override void Hit(Hittable drum, float volume)
    {
        drum.StickHit(volume);
    }
}
