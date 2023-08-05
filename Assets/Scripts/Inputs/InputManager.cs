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
    [SerializeField] private InputButtonScriptableObject _grabRightArm;
    [SerializeField] private InputVectorScriptableObject _rotateRightArm;
    [SerializeField] private InputButtonScriptableObject _grabLeftArm;
    [SerializeField] private InputVectorScriptableObject _rotateLeftArm;
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

        

        //RightARm
        //Rotate
        _input.Game.RotateRightArm.performed += ctx => _rotateRightArm.ChangeValue(_input.Game.RotateRightArm.ReadValue<Vector2>());
        _input.Game.RotateRightArm.canceled += ctx => _rotateRightArm.ChangeValue(Vector2.zero);
        //Grab
        _input.Game.GrabRightArm.performed += ctx => _grabRightArm.ChangeValue(true);
        _input.Game.GrabRightArm.canceled += ctx => _grabRightArm.ChangeValue(false);

        //LeftARm
        //Rotate
        _input.Game.RotateLeftArm.performed += ctx => _rotateLeftArm.ChangeValue(_input.Game.RotateLeftArm.ReadValue<Vector2>());
        _input.Game.RotateLeftArm.canceled += ctx => _rotateLeftArm.ChangeValue(Vector2.zero);
        //Grab
        _input.Game.GrabLeftArm.performed += ctx => _grabLeftArm.ChangeValue(true);
        _input.Game.GrabLeftArm.canceled += ctx => _grabLeftArm.ChangeValue(false);

        //Pause
        _input.Game.Pause.performed += ctx => _pause.ChangeValue(true);
        _input.Game.Pause.canceled += ctx => _pause.ChangeValue(false);


    }
    void DisableGameInput()
    {
        //Jump
        _input.Game.Jump.performed -= ctx => _grabRightArm.ChangeValue(true);
        _input.Game.Jump.canceled -= ctx => _grabRightArm.ChangeValue(false);

        //Move
        _input.Game.Move.performed -= ctx => _rotateRightArm.ChangeValue(_input.Game.Move.ReadValue<Vector2>());
        _input.Game.Move.canceled -= ctx => _rotateRightArm.ChangeValue(Vector2.zero);

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

