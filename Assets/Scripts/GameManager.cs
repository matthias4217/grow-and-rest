using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;
using UnityEngine.EventSystems;
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
    [SerializeField] private Sprite toriiHover;
    [SerializeField] private Sprite temple2Hover;
    [SerializeField] private Sprite statueHover;
    [SerializeField] private Sprite guillotineHover;
    [SerializeField] private Sprite gatewayHover;

    StructureChoice[] advancedStructures = new StructureChoice[5];
    private int currentPlayerBlueprint = 0;
    private StructureChoice currentPlayerStrucEnum = StructureChoice.Torii;
    public Structure currentPlayerStructure;
    private bool wasMousePressed = false;

    // we must have a pointer on this
    private bool _instantiated = false;

    private List<StructureChoice> EasyStructures;

    public static GameManager Instance {get; private set; }
    //private List<float> currentGoalList = new List<float>();
    private List<Structure> availableStructures = new List<Structure>();
    private List<Structure> bookedStructures = new List<Structure>();

    public event EventHandler PlayerMouseDownObserver;

    public void OnPlayerMouseDown(PlayerObserverEventArgs poea) => PlayerMouseDownObserver?.Invoke(this, poea);

    public event EventHandler PlayerMouseUpObserver;

    public void OnPlayerMouseUp() => PlayerMouseUpObserver?.Invoke(this, EventArgs.Empty);

    private static ZoomController zoomer;

    [SerializeField] private float startTime;
    private float startTimer;
    private bool generating = false;
    private bool restarting = false;
    public Canvas ui;

    public Color[] colors = { new Color(230.0f / 256.0f, 214.0f / 256.0f, 17.0f / 256.0f), new Color(224.0f / 256.0f, 13.0f / 256.0f, 45.0f / 256.0f), new Color(16.0f / 256.0f, 136.0f / 256.0f, 222.0f / 256.0f), new Color(207.0f / 256.0f, 27.0f / 256.0f, 182.0f / 256.0f), new Color(162.0f / 256.0f, 230.0f / 256.0f, 27.0f / 256.0f), new Color(256.0f / 256.0f, 170.0f / 256.0f, 209.0f / 256.0f) };

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
        zoomer = FindObjectOfType<ZoomController>();
        Random.InitState((int) System.DateTime.Now.Ticks);

        advancedStructures = new[]
        {
            StructureChoice.Torii, StructureChoice.Temple2, StructureChoice.Statue,
            StructureChoice.Guillotine, StructureChoice.Gateway
        };

        EasyStructures = new List<StructureChoice>();
        EasyStructures.Add(StructureChoice.Dolmen);
        EasyStructures.Add(StructureChoice.LittleTree);
        EasyStructures.Add(StructureChoice.Pyramid);
        EasyStructures.Add(StructureChoice.Triangle);

    }

    void Start()
    {
        //SelectBlueprint1();
    }

    public void StartGame()
    {
        SlimeFactory.color = colors[Random.Range(0, 6)];
        zoomer.Direction = 1;
        zoomer.startZoom();
        startTimer = 0.0f;
        generating = true;
    }

    public void PrepareTerrain()
    {
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

    public void UnselectBlueprint(int i)
    {
        Button button = FindButton("Blueprint " + i);
        switch (i)
        {
            case 1: button.image.sprite = torii;
                break;
            case 2: button.image.sprite = temple2;
                break;
            case 3: button.image.sprite = statue;
                break;
            case 4: button.image.sprite = guillotine;
                break;
            case 5: button.image.sprite = gateway;
                break;
            default: break;
        }
    }

    Button FindButton(string name)
    {
        Button[] buttons = Canvas.FindObjectsOfType<Button>();
        foreach (var b in buttons)
        {
            if (b.name == name)
                return b;
        }
        return null;
    }

    public void SelectBlueprint1()
    {
        for (int i = 1; i<6; i++)
        {
            UnselectBlueprint(i);
        }

        Button button = FindButton("Blueprint " + 1);
        button.image.sprite = toriiHover;
        currentPlayerBlueprint = 1;
        currentPlayerStrucEnum = StructureChoice.Torii;
    }
    public void SelectBlueprint2()
    {
        for (int i = 1; i<6; i++)
        {
            UnselectBlueprint(i);
        }

        Button button = FindButton("Blueprint " + 2);
        button.image.sprite = temple2Hover;
        currentPlayerBlueprint = 2;
        currentPlayerStrucEnum = StructureChoice.Temple2;
    }
    public void SelectBlueprint3()
    {
        for (int i = 1; i<6; i++)
        {
            UnselectBlueprint(i);
        }

        Button button = FindButton("Blueprint " + 3);
        button.image.sprite = statueHover;
        currentPlayerBlueprint = 3;
        currentPlayerStrucEnum = StructureChoice.Statue;
    }
    public void SelectBlueprint4()
    {
        for (int i = 1; i<6; i++)
        {
            UnselectBlueprint(i);
        }

        Button button = FindButton("Blueprint " + 4);
        button.image.sprite = guillotineHover;
        currentPlayerBlueprint = 1;
        currentPlayerStrucEnum = StructureChoice.Guillotine;
    }
    public void SelectBlueprint5()
    {
        for (int i = 1; i<6; i++)
        {
            UnselectBlueprint(i);
        }

        Button button = FindButton("Blueprint " + 5);
        button.image.sprite = gatewayHover;
        currentPlayerBlueprint = 1;
        currentPlayerStrucEnum = StructureChoice.Gateway;
    }

    public Tuple<Vector3, Structure> GetRandomInterestPoint()
    {
        Debug.Log("Available struct count " + availableStructures.Count);
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
        //Debug.Log("Easy struc : " + EasyStructures.Count);
        int choice = Random.Range(0, (int) EasyStructures.Count);
        Structure resStruct;
        switch (choice)
        {
            case (int) StructureChoice.Dolmen:
                resStruct = Structure.GetDolmen();
                break;
            case (int) StructureChoice.LittleTree:
                resStruct = Structure.GetLittleTree();
                break;
            case (int) StructureChoice.Pyramid:
                resStruct = Structure.GetPyramid();
                break;
            case (int) StructureChoice.Triangle:
                resStruct = Structure.GetTriangle();
                break;
            default:
                throw new Exception("Out of range !");

        }
        resStruct.originAngle = EarthAvatar.Instance.GetRandomAngle();
        availableStructures.Add(resStruct);
    }

    public void GetNewPlayerStructure(float angle)
    {
        StructureChoice choice = currentPlayerStrucEnum;
        switch (choice)
        {
            case StructureChoice.Gateway:
                currentPlayerStructure = Structure.GetGateway();
                break;
            case StructureChoice.Guillotine:
                currentPlayerStructure = Structure.GetGuillotine();
                break;
            case StructureChoice.Statue:
                currentPlayerStructure = Structure.GetStatue();
                break;
            case StructureChoice.Temple2:
                currentPlayerStructure = Structure.GetTemple2();
                break;
            case StructureChoice.Torii:
                currentPlayerStructure = Structure.GetTorii();
                break;
            default: break;
        }

        currentPlayerStructure.originAngle = angle;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!wasMousePressed)
                GetNewPlayerStructure(EarthAvatar.Instance.GetAngle(mousePosition));
            PlayerObserverEventArgs poea = new PlayerObserverEventArgs();
            poea.Position = mousePosition;
            OnPlayerMouseDown(poea);
            wasMousePressed = true;
        }
        else
        {
            wasMousePressed = false;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            OnPlayerMouseUp();
        }

        if (generating && startTimer >= startTime)
        {
            PrepareTerrain();
            generating = false;
        }
        else if (restarting && startTimer >= startTime)
        {
            ui.gameObject.SetActive(true);
            restarting = false;
        }
        else
        {
            startTimer += Time.deltaTime;
        }
    }



    public static void OnAllTreesDead()
    {
        // TODO : End game
        Debug.Log("End of the game");
        zoomer.Direction = -1;
        zoomer.startZoom();
        Instance.startTimer = 0.0f;
        Instance.restarting = true;
        SlimeAvatar[] slimes = FindObjectsOfType<SlimeAvatar>();
        foreach (SlimeAvatar slime in slimes)
        {
            slime.Erode();
        }
    }
}
