using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [Header("Replay Player Prefab")]
    [SerializeField] private GameObject replayObjectPrefab;
    public Queue<ReplayData> recordingQueue { get; private set; }

    private bool isReplaying = false;

    private Recording recording;

    //private Bandage bandage;

    //private Traps traps;


    private void Awake() => recordingQueue = new Queue<ReplayData>();

    private void Start()
    {
        //GameEventsManager.instance.onGoalReached += OnGoalReached;
        //GameEventsManager.instance.onRestartLevel += OnRestartLevel;
        EventManager.instance.onLevelFinished += OnGoalReached;
        EventManager.instance.onSpawnPlayerOnStartPosition += OnRestartLevel;

    }

    private void OnDestroy()
    {
        EventManager.instance.onLevelFinished += OnGoalReached;
        EventManager.instance.onSpawnPlayerOnStartPosition += OnRestartLevel;
    }

    private void OnGoalReached() => StartReplay();

    private void OnRestartLevel() => Reset();

    private void Update()
    {
        if (!isReplaying)
            return;

        bool hasMoreFrames = recording.PlayNextFrame();

        if(!hasMoreFrames)
        {
            RestartReplay();
        }
    }

    public void RecordReplayFrame(ReplayData data) => recordingQueue.Enqueue(data);
 
    private void StartReplay()
    {
        isReplaying = true;

        recording = new Recording(recordingQueue);

        recordingQueue.Clear();

        recording.InstantiateReplayObject(replayObjectPrefab);
    }

    private void RestartReplay()
    {
        isReplaying = true;

        recording.RestartFromBeginning();
    }

    private void Reset()
    {
        isReplaying = false;

        recordingQueue.Clear();
        recording.DestroyReplayObjectIfExists();
        recording = null;
    }
}
