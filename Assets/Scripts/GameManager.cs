using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Initializers;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasConfig canvasConfigs;
    [SerializeField] private CharacterConfig wizardConfigs;
    [SerializeField] private CharacterConfig skeletonConfigs;
    
    private void Awake()
    {
        var wizard = new CharacterInitializer(wizardConfigs, Vector3.zero, Vector3.zero);

        var skeleton = new CharacterInitializer(skeletonConfigs, new Vector3(1, 1, 0), Vector3.zero);

        var canvas = new CanvasInitializer(canvasConfigs);


    }
}
