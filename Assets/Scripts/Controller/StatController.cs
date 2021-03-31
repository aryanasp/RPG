using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Controller
{
    public class StatController : MonoBehaviour
    {
        private StatModel CurrentStat { get; set; }
        
        public Dictionary<string, StatModel> Stats { private set; get; }

    
        // Start is called before the first frame update
        private void Awake()
        {
            InitializeStats();
        }
    
        private void InitializeStats()
        {
            Stats = new Dictionary<string, StatModel>();
            //Health
            CurrentStat = new HealthModel();
            CurrentStat.Enter(70, 100);
            Stats.Add(CurrentStat.StatName, CurrentStat);
            CurrentStat.Exit();
            //Mana
            CurrentStat = new ManaModel();
            CurrentStat.Enter(40, 50);
            Stats.Add(CurrentStat.StatName, CurrentStat);
            CurrentStat.Exit();
        }
    
    
        private void ChangeStat(StatModel newStat)
        {
            Stats[CurrentStat.StatName] = CurrentStat;
            if (CurrentStat == newStat) return;
            //If new stat wasn't in the stat list
            if (!Stats.ContainsKey((newStat.StatName)))
            {
                Stats.Add(newStat.StatName, newStat);
            }

            //Saving current stat info in the list of stats
            CurrentStat = Stats[newStat.StatName];
            CurrentStat.Exit();
            //Loading last stat info to the new stat
            StatModel oldStat = Stats[newStat.StatName];
            newStat.Enter(oldStat.StatCurrentValue, oldStat.StatMaxValue);
            //switch current stat to new stat
            CurrentStat = newStat;
        }

        public void ChangeStatValue(string statName, float amount)
        {
            ChangeStat(Stats[statName]);
            CurrentStat.ChangeStatValue(amount);
            CurrentStat.UpdateUI();
        }

        public bool IsStatValueEnough(string statName, float amount)
        {
            ChangeStat(Stats[statName]);
            return CurrentStat.StatCurrentValue >= amount;
        }
        
        void Start()
        {
            foreach (StatModel stat in Stats.Values)
            {
                stat.UpdateUI();
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
