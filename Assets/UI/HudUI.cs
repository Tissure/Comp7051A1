using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreValue;

    public void UpdateScore(int value)
    {
        scoreValue.text = value.ToString();
    }
}
