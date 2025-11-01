using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MapNote
{
    [SerializeField]
    public float position;

    public MapNote(float position)
    {
        this.position = position;
    }
}

public class Map : MonoBehaviour
{
    public string mapName;
    public AudioClip song;
    public float tempo;
    public float songLength;
    public float startOffset;
    // Notes are stored in a 2D Array.
    // First index is the Enum DrumInputs
    public List<MapNote>[] notes;
    protected void Start()
    {
        // TODO remove, stub data
        // Instantiate tracks
        notes = new List<MapNote>[System.Enum.GetNames(typeof(DrumInputs)).Length];
        // Clickmap stuff
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i] = new List<MapNote>();
        }
    }
}
