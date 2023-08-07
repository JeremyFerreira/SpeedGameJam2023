using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxe : MonoBehaviour
{
    [SerializeField]
    private float layer;

    [SerializeField]
    private GameObject _camera;

    private float _initialPotion;
    private Vector3 _intitial;

    // Start is called before the first frame update
    void Start()
    {
        _intitial = transform.position;
        _initialPotion = _intitial.x;// + transform.parent.position.x;
        if (layer == 0)
            layer = 1;
        layer /= 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(_camera == null) { return; }
        Vector3 Position = _camera.transform.position;
        float Temp = Position.x * (1 - layer);
        float Distance = Position.x * layer;

        Vector3 NewPosition = new Vector3(_initialPotion + Distance, transform.position.y, transform.position.z);

        transform.position = NewPosition;
    }


}
