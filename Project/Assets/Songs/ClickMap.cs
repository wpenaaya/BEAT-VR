using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMap : Map
{
    new void Start()
    {
        base.Start();
        // Fill LEFT track with some sample notes.
        for (var i = 4; i < 50; i += 4)
        {
            notes[(int)DrumInputs.LEFT].Add(new MapNote(i));
        }
        for (var i = 5; i < 50; i += 4)
        {
            notes[(int)DrumInputs.MIDLEFT].Add(new MapNote(i));
        }
        for (var i = 6; i < 50; i += 4)
        {
            notes[(int)DrumInputs.MIDRIGHT].Add(new MapNote(i));
        }
        for (var i = 7; i < 50; i += 4)
        {
            notes[(int)DrumInputs.RIGHT].Add(new MapNote(i));
        }
    }
}
