using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class DrumGame : MonoBehaviour
{
    public AudioSource audioSource;
    public List<DrumScript> drums;
    private MapPlayer mapPlayer;

    public NoteCreator noteCreator;
    private HitJudge judge;
    public ScoreSystem scoreSystem;

    [SerializeField] public UIUpdate updateUIScore = null;
    [SerializeField] public UIUpdate updateUITime = null;
    [SerializeField] public UIUpdate updateUISongName = null;

    public UnityEvent songStarting = null;
    public UnityEvent songEnd = null;

    public UnityEvent pulseFX = null;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var drum in drums)
        {
            drum.OnHit += HitEvent;
        }
    }

    // TODO REMOVE
    private void Update()
    {
        if(mapPlayer != null && mapPlayer.playing)
        {
            UpdateUITime();
        }
    }

    [ContextMenu("Play")]
    public void Play(Map map)
    {
        // Stop previous song
        mapPlayer?.Stop();

        // Create the mapPlayer
        mapPlayer = gameObject.AddComponent<MapPlayer>();
        mapPlayer.InitMapPlayer(map, noteCreator, audioSource, 3f);
        mapPlayer.songStopped.AddListener(Stop);

        // Setup the NoteCreator, register events
        noteCreator.noteDuration = mapPlayer.beatsShownAhead * mapPlayer.beatDuration;
        mapPlayer.SpawnNoteEvent += noteCreator.SpawnNote;

        // Setup the judge
        judge = gameObject.AddComponent<HitJudge>();
        judge.activeMapPlayer = mapPlayer;

        // Setup the ScoreSystem
        scoreSystem.Clear();
        UpdateUIScore();
        UpdateUITime();
        UpdateUISongName();

        songStarting.Invoke();

        // Play
        mapPlayer.Play();
    }

    public void Stop()
    {
        Destroy(judge);
        Destroy(mapPlayer);
        scoreSystem.Clear();

        songEnd.Invoke();
    }

    HitScores HitEvent(DrumInputs drumInput)
    {
        if (mapPlayer != null && mapPlayer.playing)
        {
            //Debug.Log("DrumGame: mapPlayer is playing");
            pulseFX.Invoke();
        } else
        {
            //Debug.Log("DrumGame: mapPlayer is not playing. Returning NONE");
            return HitScores.NONE;
        }
        //Debug.Log("Event gotten!: " + drumInput.ToString());
        //Debug.Log("Passing to HitJudge for Judgement...");

        // Record the Score
        HitScores hitScore = judge.JudgeHit(drumInput);
        scoreSystem.RecordHit(hitScore);

        UpdateUIScore();

        return hitScore;
    }

    public void UpdateUIScore()
    {
        string totalScoreString = scoreSystem.totalScore.ToString();
        updateUIScore.Invoke(totalScoreString);
    }

    void UpdateUITime()
    {
        string text = mapPlayer.mapTime.ToString("n1") + " / " + mapPlayer.activeMap.songLength.ToString("n1");
        updateUITime.Invoke(text);
    }

    public void UpdateUISongName()
    {
        updateUISongName.Invoke(mapPlayer.activeMap.mapName);
    }
}
