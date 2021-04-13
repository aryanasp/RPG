using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Model;
using UnityEngine;
using View;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUICanvasPrefab;
    [SerializeField] private GameObject characterPrefab1;
    [SerializeField] private GameObject characterPrefab2;
    
    private void Awake()
    {
        // Initialize Character Stat
        // Initialize model and view for a character stats
        GameObject characterGameObject = Instantiate(characterPrefab1, new Vector3(0, 0, 0), Quaternion.identity);
        var model1 = new CharacterStatModel();
        var views1 = characterGameObject.GetComponentsInChildren<CharacterStatView>();
        var character = new CharacterModel(characterGameObject, model1);
        var controllers1 = new List<CharacterStatController>();
        foreach (var characterStatView in views1)
        {
            controllers1.Add(new CharacterStatController(model1, characterStatView));
        }
        
        // Initialize Character Stat
        // Initialize model and view for a character stats
        GameObject characterGameObject2 = Instantiate(characterPrefab2, new Vector3(1, 1, 0), Quaternion.identity);
        var model2 = new CharacterStatModel();
        var views2 = characterGameObject2.GetComponentsInChildren<CharacterStatView>();
        var character2 = new CharacterModel(characterGameObject2, model2);
        var controllers2 = new List<CharacterStatController>();
        foreach (var characterStatView in views2)
        {
            controllers2.Add(new CharacterStatController(model2, characterStatView));
        }
        
        // Initialize Hud
        // Initialize model and view factories
        GameObject canvasGameObject = Instantiate(gameUICanvasPrefab);
        var hudCharacterModelFactory = new ModelFactory.HudCharacterModelFactory();
        var hudCharacterViewFactory = new ViewFactory.HudCharacterViewFactory();
        var hudStatViewFactory = new ViewFactory.HudStatViewFactory();
        // Initialize model and view values
        var hudCharacterModel = hudCharacterModelFactory.Model;
        var hudCharacterView = hudCharacterViewFactory.View;
        var hudStatViews = hudStatViewFactory.Views;
        // Initialize controller factory
        var hudControllerFactory = new ControllerFactory.HudCharacterControllerFactory(hudCharacterModel, hudCharacterView, hudStatViews);
        // Initialize controller value
        var hudControllers = hudControllerFactory.Controllers;
        
        
    }
}
