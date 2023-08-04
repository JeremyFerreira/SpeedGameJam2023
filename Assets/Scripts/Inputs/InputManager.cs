using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public static PlayerControls _input { private set; get; }
    [Header("Game")]
    [SerializeField] private InputButtonScriptableObject _jump;
    [SerializeField] private InputVectorScriptableObject _move;
    [SerializeField] private InputButtonScriptableObject _pause;
    private bool _isGamepad { get; set; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this);
        _input = new PlayerControls();

    }
    private void OnEnable()
    {
        EnableGameInput();
    }
    private void OnDisable()
    {
        DisableGameInput();
    }
    public void EnableGameInput()
    {
        _input.Enable();

        //Jump
        _input.Game.Jump.performed += ctx => _jump.ChangeValue(true);
        _input.Game.Jump.canceled += ctx => _jump.ChangeValue(false);

        //Move
        _input.Game.Move.performed += ctx => _move.ChangeValue(_input.Game.Move.ReadValue<Vector2>());
        _input.Game.Move.canceled += ctx => _move.ChangeValue(Vector2.zero);

        //Pause
        _input.Game.Pause.performed += ctx => _pause.ChangeValue(true);
        _input.Game.Pause.canceled += ctx => _pause.ChangeValue(false);


    }
    void DisableGameInput()
    {
        //Jump
        _input.Game.Jump.performed -= ctx => _jump.ChangeValue(true);
        _input.Game.Jump.canceled -= ctx => _jump.ChangeValue(false);

        //Move
        _input.Game.Move.performed -= ctx => _move.ChangeValue(_input.Game.Move.ReadValue<Vector2>());
        _input.Game.Move.canceled -= ctx => _move.ChangeValue(Vector2.zero);

        //Pause
        _input.Game.Pause.performed -= ctx => _pause.ChangeValue(true);
        _input.Game.Pause.canceled -= ctx => _pause.ChangeValue(false);


        _input.Disable();
    }
    private void Update()
    {
        //find the last Input Device used and set a bool.
        _isGamepad = IsGamepad();
    }

    public static bool IsGamepad()
    {
        InputDevice lastUsedDevice = null;
        float lastEventTime = 0;
        foreach (var device in InputSystem.devices)
        {
            if (device.lastUpdateTime > lastEventTime)
            {
                lastUsedDevice = device;
                lastEventTime = (float)device.lastUpdateTime;
            }
        }

        return lastUsedDevice is Gamepad;
    }
}

