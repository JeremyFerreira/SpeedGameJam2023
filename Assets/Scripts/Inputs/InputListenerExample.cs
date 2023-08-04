using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListenerExample : MonoBehaviour
{
    [SerializeField] private InputButtonScriptableObject _inputButton;
    [SerializeField] private InputVectorScriptableObject _inputVector;

    private void OnEnable()
    {
        _inputButton.OnValueChanged += ButtonMethod;
        _inputVector.OnValueChanged += VectorMethod;
    }
    private void OnDisable()
    {
        _inputButton.OnValueChanged -= ButtonMethod;
        _inputVector.OnValueChanged -= VectorMethod;
    }
    private void ButtonMethod(bool value)
    {
        Debug.Log(value);
    }
    private void VectorMethod(Vector2 value)
    {
        Debug.Log(value);
    }
}
