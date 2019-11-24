using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerObserverEventArgs : EventArgs
{
    public Vector2 Position { get; set; }
}



public class GameManager : MonoBehaviour
{
    [SerializeField] private float slimeSize = 0.17f;
    [SerializeField] private int nbrInterestPoints = 3;
    [SerializeField] private int nbrTrees;
    public int MaxTrees = 5;
    public int MinTrees = 2;
    public GameObject[] TreePrefab;

    [Header("Blueprint sprites")]
    [SerializeField] private Sprite torii;
    [SerializeField] private Sprite temple2;
    [SerializeField] private Sprite statue;
    [SerializeField] private Sprite guillotine;
    [SerializeField] private Sprite gateway;

    LinkedList<StructureChoice> advancedStructures = new LinkedList<StructureChoice>();

    private bool _instantiated = false;
    private Structure currentPlayerBlueprint = Structure.GetDolmen();
    private Image currentBlueprintImage;

    public List<StructureChoice> EasyStructures = new List<StructureChoice>();

    public static GameManager Instance {get; private set; }
    //private List<float> currentGoalList = new List<float>();
    private List<Structure> availableStructures = new List<Structure>();
    private List<Structure> bookedStructures = new List<Structure>();

    public event EventHandler PlayerMouseDownObserver;

    public void OnPlayerMouseDown(PlayerObserverEventArgs poea) => PlayerMouseDownObserver?.Invoke(this, poea);

    public event EventHandler PlayerMouseUpObserver;

    public void OnPlayerMouseUp() => PlayerMouseUpObserver?.Invoke(this, EventArgs.Empty);

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
        Application.targetFrameRate = 60;

        Random.InitState((int) System.DateTime.Now.Ticks);

        advancedStructures.AddLast(StructureChoice.Gateway);
        advancedStructures.AddLast(StructureChoice.Guillotine);
        advancedStructures.AddLast(StructureChoice.Statue);
        advancedStructures.AddLast(StructureChoice.Temple2);
        advancedStructures.AddLast(StructureChoice.Torii);

        EasyStructures.Add(StructureChoice.Dolmen);


    }

    // Start is called before the first frame update
    void Start()
    {
        var canvasImages = Canvas.FindObjectsOfType<Image>();
        foreach (var image in canvasImages)
        {
            if (image.name == "Current Blueprint")
                currentBlueprintImage = image;
        }

        currentBlueprintImage.sprite = torii;
        for (int i = 0; i < nbrInterestPoints; i++)
        {
            AddStructure();
        }

        nbrTrees = Random.Range(MinTrees, MaxTrees);

        for (int j = 0; j < nbrTrees; j++)
        {
            float angle = EarthAvatar.Instance.GetRandomAngle();
            Instantiate(TreePrefab[Random.Range(0, TreePrefab.Length - 1)], EarthAvatar.Instance.GetUnityCoords(angle),
                Quaternion.Euler(0.0f, 0.0f, -angle));
        }
    }

    public Tuple<Vector3, Structure> GetRandomInterestPoint()
    {
        int structIndex = Random.Range(0, availableStructures.Count);
        Structure selectedStruct = availableStructures[structIndex];
        var selectedPoint = selectedStruct.GetRandomAvailablePoint();
        if (selectedStruct.IsStructFull())
        {
            availableStructures.Remove(selectedStruct);
            bookedStructures.Add(selectedStruct);
            AddStructure();
        }
        float rotationAngle = selectedStruct.originAngle;
        return new Tuple<Vector3, Structure> (
            EarthAvatar.Instance.GetUnityCoords(selectedStruct.originAngle)
               + Quaternion.AngleAxis(rotationAngle, Vector3.back)
               * new Vector3(selectedPoint.Item1 * slimeSize, selectedPoint.Item2 * slimeSize),
            selectedStruct
            );
    }

    private void AddStructure()
    {
        int choice = Random.Range(0, (int) EasyStructures.Count);
        Structure resStruct;
        switch (choice)
        {
            case (int) StructureChoice.Dolmen:
                resStruct = Structure.GetDolmen();
                break;
            default:
                throw new Exception("Out of range !");

        }
        resStruct.originAngle = EarthAvatar.Instance.GetRandomAngle();
        availableStructures.Add(resStruct);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerObserverEventArgs poea = new PlayerObserverEventArgs();
            poea.Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnPlayerMouseDown(poea);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            OnPlayerMouseUp();
        }
    }

    public static void OnAllTreesDead()
    {
        // TODO : End game
        Debug.Log("End of the game");
    }
}
