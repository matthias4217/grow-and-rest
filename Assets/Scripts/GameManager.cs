using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerObserverEventArgs : EventArgs
{
    public Vector2 Position { get; set; }
}

public class GameManager : MonoBehaviour
{
    private bool _instantiated = false;

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
    }

    // Start is called before the first frame update
    void Start()
    {

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
