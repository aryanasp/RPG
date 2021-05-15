using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    
    [CreateAssetMenu(fileName = "CharacterConfigs", menuName = "CharacterConfigs", order = 0)]
    public class CharacterConfig : ScriptableObject
    {   
        [Header("Controller", order = 1)]
        [SerializeField] private ControllerEnum controller;
        private enum ControllerEnum{ Player, AI}
        private readonly bool[] _controllerBool = {true, false };
        public bool IsControllerPlayer => _controllerBool[(int) controller];
        // TODO tribes should add : Wizard(nature, dark),Knights(elves, orcs, humans, dwarves), Dead(zombies, skeletons, vampires), Dragons(black red green blue)
        [Header("Prefab", order = 1)]
        [SerializeField] private GameObject characterPrefab;
        public GameObject CharacterPrefab => characterPrefab;
        [Space]
        [Space]
        [Header("Characteristics", order = 1)]
        [SerializeField] private RacesEnum race;
        private enum RacesEnum{ Wizard, Knight, Skeleton, DarkMagician, Dragon}
        private readonly string[] _racesString = {"Wizard", "Knight", "Skeleton", "DarkMagician", "Dragon"};
        public string Race => _racesString[(int) race];
        [SerializeField] private Sprite hudImage;
        public Sprite HudImage => hudImage;
        [Space]
        [Space]
        [Header("Attributes", order = 1)]
        [Header("Stats", order = 2)]
        [SerializeField] private float movementSpeed;
        public float MovementSpeed => movementSpeed;
        [Header("Health Setting", order = 2)]
        [SerializeField] private float maxHealthInitialValue;
        public float MaxHealthInitialValue => maxHealthInitialValue;
        [SerializeField] private float healthInitialValue;
        public float HealthInitialValue => healthInitialValue;
        [Header("Mana Setting", order = 2)]
        [SerializeField] private float maxManaInitialValue;
        public float MaxManaInitialValue => maxManaInitialValue;
        [SerializeField] private float manaInitialValue;
        public float ManaInitialValue => manaInitialValue;
        
    }
}