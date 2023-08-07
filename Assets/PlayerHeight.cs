using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeight : MonoBehaviour
{
    public Transform player;
    public Transform Start;
    public Transform End;
    public float percentage;
    public TextMeshProUGUI percentageText;
    public Slider percentageSlider;

    // Update is called once per frame
    void Update()
    {
        float value = (player.position.y - Start.position.y)/(End.position.y - Start.position.y);
        percentageSlider.value = value;
        percentage = value * 100;
        percentageText.text = ((int)percentage).ToString() + "%";
    }
}
