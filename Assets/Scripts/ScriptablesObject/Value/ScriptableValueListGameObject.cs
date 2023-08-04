using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Value/ListGameObject")]
public class ScriptableValueListGameObject : ScriptableValue<List<GameObject>>
{
    public override List<GameObject> Value { get { return base.Value; } set => base.Value = value; }
    public int Count => Value.Count;
    public void AddUnique(GameObject gameObject)
    {
        //securit�
        if (base.Value == null) 
        { base.Value = new(); }

        Value.Add(gameObject);
        OnValueChanged?.Invoke(Value);
    }
    public void RemoveUnique(GameObject gameObject)
    {
        //securit�
        if (base.Value == null)
        { base.Value = new(); }

        if (Value.Contains(gameObject))
        {
            Value.Remove(gameObject);
            OnValueChanged?.Invoke(Value);
        }
    }
    public void ResetList()
    {
        Debug.Log("reset");
        Value.RemoveRange(0, Value.Count);
        OnValueChanged?.Invoke(Value);
    }
}
