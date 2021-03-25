using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    private Player _player;
    //Config health stat bar
    [SerializeField] private Stat health;
    //config initial values for current and max health
    [SerializeField] private float initialHealth;

    [SerializeField] private float maxInitialHealth;
    // Start is called before the first frame update

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.HealthReducer += DecreaseHealth;
        _player.HealthRaiser += RaiserHealth;
    }

    private void OnDisable()
    {
        _player.HealthReducer -= DecreaseHealth;
        _player.HealthRaiser -= RaiserHealth;
    }

    void Start()
    {
        health.Initialize(initialHealth, maxInitialHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void DecreaseHealth(float amount)
    {
        health.BarCurrentValue -= amount;
    }

    private void RaiserHealth(float amount)
    {
        health.BarCurrentValue += amount;
    }
}
