using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public Map activeMap;
    public float beatsShownAhead;
    private int[] nextNoteIndex;
    public float beatDuration;
    public float mapTime = 0f;
    public float mapStartTime;
    public float mapBeat;

    public NoteCreator noteCreator;
    public event System.Action<DrumInputs> SpawnNoteEvent;

    public UnityEvent songStopped;

    public bool playing = false;

    // Start is called before the first frame update
    public void InitMapPlayer(Map map, NoteCreator noteCreator, AudioSource audioSource, float beatsShownAhead)
    {
        this.activeMap = Instantiate(map);
        this.audioSource = audioSource;
        this.noteCreator = noteCreator;
        this.beatsShownAhead = beatsShownAhead;
        beatDuration = 60.0f / activeMap.tempo;
        nextNoteIndex = new int[System.Enum.GetNames(typeof(DrumInputs)).Length];
        audioSource.clip = activeMap.song;

        this.songStopped = new UnityEvent();
    }

    public void Play()
    {
        mapStartTime = (float)AudioSettings.dspTime;
        audioSource.Play();
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            mapTime = (float)(AudioSettings.dspTime - mapStartTime - activeMap.startOffset);
            mapBeat = mapTime / beatDuration;

            for (int i = 0; i < nextNoteIndex.Length; i++)
            {
                /* NOTE:
                 * THE LOGIC HERE WAS TAKEN FROM https://www.gamasutra.com/blogs/YuChao/20170316/293814/Music_Syncing_in_Rhythm_Games.php
                 */
                if (nextNoteIndex[i] < activeMap.notes[i].Count && activeMap.notes[i][nextNoteIndex[i]].position < mapBeat + beatsShownAhead)
                {
                    SpawnNote(i, nextNoteIndex[i]);
                    nextNoteIndex[i]++;
                }
            }
        }

        if(playing && mapTime > activeMap.songLength)
        {
            Stop();
        }
    }

    public void Stop()
    {
        playing = false;
        audioSource.Stop();
        songStopped.Invoke();
    }

    void SpawnNote(int track, int index)
    {
        SpawnNoteEvent.Invoke((DrumInputs) track);
    }

    
}
