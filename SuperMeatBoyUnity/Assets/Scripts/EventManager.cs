using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
            Debug.LogError("EventManagers count > 1");
        instance = this;
    }

    public event Action onLevelFinished;

    public void LevelFinished()
    {
        if (onLevelFinished != null)
            onLevelFinished();
    }

    public event Action onSpawnPlayerOnStartPosition;

    public void SpawnPlayerOnStart()
    {
        if (onSpawnPlayerOnStartPosition != null)
            onSpawnPlayerOnStartPosition();
    }
}
