using System.Collections;
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
    [SerializeField] private float slimeSize = 0.16f;
    [SerializeField] private int nbrInterestPoints = 3;
    private bool _instantiated = false;
    public static GameManager Instance {get; private set; }
    //private List<float> currentGoalList = new List<float>();
    private List<Structure> structures = new List<Structure>();

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
        for (int i = 0; i < nbrInterestPoints; i++)
        {
            //currentGoalList.Add(EarthAvatar.Instance.GetRandomAngle());
            AddStructure();
        }




    }

    public Vector3 GetRandomInterestPoint()
    {
        int structIndex = Random.Range(0, structures.Count);
        Structure selectedStruct = structures[structIndex];
        var selectedPoint = selectedStruct.GetRandomAvailablePoint();
        if (selectedStruct.IsStructFull())
        {
            structures.Remove(selectedStruct);
            AddStructure();
        }
        // TODO : rotate the struct
        // TODO : scale to a slime size
        float rotationAngle = selectedStruct.originAngle;
        return EarthAvatar.Instance.GetUnityCoords(selectedStruct.originAngle)
               + Quaternion.AngleAxis(rotationAngle, Vector3.back)
               * new Vector3(selectedPoint.Item1 * slimeSize, selectedPoint.Item2 * slimeSize);
    }

    private void AddStructure()
    {
            Structure dolmen = Structure.GetDolmen();
            dolmen.originAngle = EarthAvatar.Instance.GetRandomAngle();
            structures.Add(dolmen);
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
