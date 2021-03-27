using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;


public class Player : Character
{   
    

    private IStatController _currentStat;

    
    //Controlling health and mana via actions .(using observer design pattern) 
    // Start is called before the first frame update

    private void Awake()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        StatsForChild = new Dictionary<string, IStatController>();
        //Health
        _currentStat = new HealthController();
        _currentStat.Enter(90, 100);
        StatsForChild.Add(_currentStat.StatName(), _currentStat);
        //Mana
        _currentStat = new ManaController();
        _currentStat.Enter(70, 200);
        StatsForChild.Add(_currentStat.StatName(), _currentStat);
    }

    //Change the stat
    private void ChangeStat(IStatController newStat)
    {
        StatsForChild[_currentStat.StatName()] = _currentStat;
        if (_currentStat == newStat) return;
        //If new stat wasn't in the stat list
        if (!StatsForChild.ContainsKey((newStat.StatName())))
        {
            StatsForChild.Add(newStat.StatName(), newStat);
        }
        //Saving current stat info in the list of stats
        _currentStat = StatsForChild[newStat.StatName()];
        _currentStat.Exit();
        //Loading last stat info to the new stat
        IStatController oldStat = StatsForChild[newStat.StatName()];
        newStat.Enter(oldStat.StatCurrentValue(), oldStat.StatMaxValue());
        //switch current stat to new stat
        _currentStat = newStat;
    }
    
    protected override void Start()
    {
        foreach (IStatController stat in StatsForChild.Values)
        {
            stat.UpdateUI();
        }
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInputFromKeyboard();
        base.Update();
    }

    void GetInputFromKeyboard()
    {
        
        //Apply 0 movement speed in the starting of each frame
        MoveDirection = Vector2.zero;
        //Handle player movements
        if (Input.GetKey(KeyCode.W))
        {
            MoveDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveDirection += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveDirection += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveDirection += Vector2.left;
        }
        
        
        //Debug Stat Bar
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeStat(StatsForChild["Health"]);
            
            _currentStat.IncreaseStatValue(10);
            _currentStat.UpdateUI();
            
            
            ChangeStat(StatsForChild["Mana"]);
            
            _currentStat.IncreaseStatValue(30);
            _currentStat.UpdateUI();
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeStat(StatsForChild["Health"]);
            
            _currentStat.DecreaseStatValue(10);
            _currentStat.UpdateUI();
            
            
            ChangeStat(StatsForChild["Mana"]);
           
            _currentStat.DecreaseStatValue(30);
            _currentStat.UpdateUI();
            
        }
    }
    
}
