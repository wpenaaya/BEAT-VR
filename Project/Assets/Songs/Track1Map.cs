using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track1Map : Map
{
    new void Start()
    {
        base.Start();

        float[] leftNotes = { 48.0f, 48.5f, 49.0f, 49.5f, 50.0f, 50.5f, 51.0f, 51.5f, 52.0f, 52.5f, 53.0f, 53.5f, 54.0f, 54.5f, 55.0f, 55.5f, 56.0f, 56.5f, 57.0f, 57.5f, 58.0f, 58.5f, 59.0f, 59.5f, 65.0f, 67.0f, 69.0f, 71.0f, 71.5f, 73.0f, 75.0f, 76.75f, 77.0f, 79.0f, 79.5f, 81.0f, 83.0f, 85.0f, 87.0f, 87.5f, 89.0f, 91.0f, 92.75f, 93.0f, 95.0f, 95.5f, 97.0f, 99.0f, 101.0f, 103.0f, 103.5f, 105.0f, 107.0f, 108.75f, 109.0f, 111.0f, 111.5f, 113.0f, 115.0f, 117.0f, 119.0f, 119.5f, 121.0f, 123.0f, 124.75f, 125.0f, 127.0f, 127.5f, 129.0f, 131.0f, 133.0f, 135.0f, 135.5f, 137.0f, 139.0f, 140.75f, 141.0f, 143.0f, 143.5f, 145.0f, 147.0f, 149.0f, 151.0f, 151.5f, 153.0f, 155.0f, 156.75f, 157.0f, 159.0f, 159.5f, 161.0f, 163.0f, 165.0f, 167.0f, 167.5f, 169.0f, 171.0f, 172.75f, 173.0f, 175.0f, 175.5f, 177.0f, 179.0f, 181.0f, 183.0f, 183.5f, 185.0f, 187.0f, 188.75f, 189.0f, 191.0f, 191.5f };
        foreach(float e in leftNotes)
        {
            notes[(int)DrumInputs.LEFT].Add(new MapNote(e));
        }

        float[] midLeftNotes = { 4.0f, 12.0f, 20.0f, 28.0f, 36.0f, 44.0f, 66.0f, 70.0f, 74.0f, 78.0f, 82.0f, 86.0f, 90.0f, 94.0f, 98.0f, 102.0f, 106.0f, 110.0f, 114.0f, 118.0f, 122.0f, 126.0f, 130.0f, 134.0f, 138.0f, 142.0f, 146.0f, 150.0f, 154.0f, 158.0f, 162.0f, 166.0f, 170.0f, 174.0f, 178.0f, 182.0f, 186.0f, 190.0f };
        foreach (float e in midLeftNotes)
        {
            notes[(int)DrumInputs.MIDLEFT].Add(new MapNote(e));
        }

        float[] midRightNotes = { 60.0f, 60.25f, 60.5f, 62.75f, 135.0f, 143.0f, 151.0f, 159.0f, 167.0f, 175.0f, 183.0f, 191.0f };
        foreach (float e in midRightNotes)
        {
            notes[(int)DrumInputs.MIDRIGHT].Add(new MapNote(e));
        }

        float[] rightNotes = { 0.0f, 8.0f, 16.0f, 24.0f, 32.0f, 40.0f, 61.0f, 62.0f, 63.0f, 63.5f, 64.0f };
        foreach(float e in rightNotes)
        {
            notes[(int)DrumInputs.RIGHT].Add(new MapNote(e));
        }
    }
}
