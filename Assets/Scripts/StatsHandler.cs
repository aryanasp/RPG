using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    private IStatController CurrentStat { get; set; }

    public Dictionary<string, IStatController> Stats { private set; get; }

    
    // Start is called before the first frame update
    private void Awake()
    {
        InitializeStats();
    }
    
    private void InitializeStats()
    {
        Stats = new Dictionary<string, IStatController>();
        //Health
        CurrentStat = new HealthController();
        CurrentStat.Enter(90, 100);
        Stats.Add(CurrentStat.StatName(), CurrentStat);
        //Mana
        CurrentStat = new ManaController();
        CurrentStat.Enter(70, 200);
        Stats.Add(CurrentStat.StatName(), CurrentStat);
    }
    
    
    private void ChangeStat(IStatController newStat)
    {
        Stats[CurrentStat.StatName()] = CurrentStat;
        if (CurrentStat == newStat) return;
        //If new stat wasn't in the stat list
        if (!Stats.ContainsKey((newStat.StatName())))
        {
            Stats.Add(newStat.StatName(), newStat);
        }

        //Saving current stat info in the list of stats
        CurrentStat = Stats[newStat.StatName()];
        CurrentStat.Exit();
        //Loading last stat info to the new stat
        IStatController oldStat = Stats[newStat.StatName()];
        newStat.Enter(oldStat.StatCurrentValue(), oldStat.StatMaxValue());
        //switch current stat to new stat
        CurrentStat = newStat;
    }

    public void IncreaseStatValue(string statName, float amount)
    {
        ChangeStat(Stats[statName]);
        CurrentStat.IncreaseStatValue(amount);
        CurrentStat.UpdateUI();
    }

    public void DecreaseStatValue(string statName, float amount)
    {
        ChangeStat(Stats[statName]);
        CurrentStat.DecreaseStatValue(amount);
        CurrentStat.UpdateUI();
    }

    void Start()
    {
        foreach (IStatController stat in Stats.Values)
        {
            stat.UpdateUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
