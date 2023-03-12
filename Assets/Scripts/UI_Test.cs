using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Test : MonoBehaviour
{

    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => StartClick());
    }

    private void StartClick()
    {
        
    }
    
}