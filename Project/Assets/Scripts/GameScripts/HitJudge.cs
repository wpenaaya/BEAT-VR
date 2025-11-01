using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitJudge : MonoBehaviour
{
    public float timeTolerance = 0.05f;

    public MapPlayer activeMapPlayer;


    private static float FindBeatDistanceToNearestMapNote(List<MapNote> track, float currentBeat)
    {
        IComparer<MapNote> comparer = Comparer<MapNote>.Create(
            (MapNote x, MapNote y) =>
            {
                return x.position.CompareTo(y.position);
            });
        int nearest = track.BinarySearch(new MapNote(currentBeat), comparer);

        if (nearest < 0)
            nearest = ~nearest;

        if(nearest >= track.Count)
        {
            return currentBeat - track[nearest - 1].position;
        }

        float distance = track[nearest].position - currentBeat;
        if (nearest > 0)
        {
            float distance2 = currentBeat - track[nearest-1].position;
            return (distance < distance2) ? distance : distance2;
        }
        return distance;
    }

    public HitScores JudgeHit(DrumInputs input)
    {
        float currentBeat = activeMapPlayer.mapBeat;
        float currentTime = activeMapPlayer.mapTime;

        float beatError = FindBeatDistanceToNearestMapNote(activeMapPlayer.activeMap.notes[(int)input], currentBeat);
        float timeError = System.Math.Abs(beatError * activeMapPlayer.beatDuration);

        if (timeError > timeTolerance * 3)
            return HitScores.MISS;
        else if (timeError > timeTolerance * 2)
            return HitScores.OKAY;
        else if (timeError > timeTolerance)
            return HitScores.GREAT;
        else
            return HitScores.PERFECT;
    }
}
