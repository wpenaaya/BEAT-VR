using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates notes
public class NoteCreator : MonoBehaviour
{
    public NoteTrack[] tracks;
    public List<GameObject>[] notes;

    public float noteDuration;

    public GameObject notePrefab;

    void Start()
    {
        notes = new List<GameObject>[tracks.Length];
        for(int i = 0; i < notes.Length; i++)
        {
            notes[i] = new List<GameObject>();
        }
    }

    public void SpawnNote(DrumInputs track)
    {
        GameObject newNote = Instantiate(
                    notePrefab,
                    tracks[(int)track].NoteSpawn.position,
                    tracks[(int)track].NoteSpawn.rotation
                    );
        NoteScript newNoteScript = newNote.GetComponent<NoteScript>();
        newNoteScript.duration = noteDuration;
        newNoteScript.end = tracks[(int)track].NoteEnd.position;
        notes[(int)track].Add(newNote);
    }
}
