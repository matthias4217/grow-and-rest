﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

public class PlayerObserverEventArgs : EventArgs
{
    public Vector2 Position { get; set; }
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private int nbrInterestPoints = 3;
    private bool _instantiated = false;
    public static GameManager Instance {get; private set; }
    private List<float> currentGoalList = new List<float>();

    public event EventHandler PlayerObserver;

    public void OnPlayerClick(PlayerObserverEventArgs poea) => PlayerObserver?.Invoke(this, poea);

    private void Awake()
    {
        if (_instantiated)
        {
            Debug.LogError("Trying to instantiate multiple game managers !");
            Destroy(this);
        }
        else
        {
            _instantiated = true;
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        Random.InitState((int) System.DateTime.Now.Ticks);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<nbrInterestPoints;i++)
            currentGoalList.Add(EarthAvatar.Instance.GetRandomAngle());



    }

    public float GetRandomInterestPoint()
    {
        return currentGoalList[Random.Range(0, currentGoalList.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            PlayerObserverEventArgs poea = new PlayerObserverEventArgs();
            poea.Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnPlayerClick(poea);
        }
    }
}
