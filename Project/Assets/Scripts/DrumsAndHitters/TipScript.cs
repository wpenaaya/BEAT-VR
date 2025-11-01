using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipScript : AbstractDrumHitter
{
    protected override void Hit(Hittable drum, float volume)
    {
        drum.TipHit(volume);
    }
}
