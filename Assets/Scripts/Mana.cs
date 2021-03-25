using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    private Player _player;
    //Config health stat bar
    [SerializeField] private Stat mana;
    //config initial values for current and max health
    [SerializeField] private float initialMana;

    [SerializeField] private float maxInitialMana;
    // Start is called before the first frame update

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.ManaReducer += DecreaseMana;
        _player.ManaRaiser += RaiserMana;
    }

    private void OnDisable()
    {
        _player.ManaReducer -= DecreaseMana;
        _player.ManaRaiser -= RaiserMana;
    }

    void Start()
    {
        mana.Initialize(initialMana, maxInitialMana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void DecreaseMana(float amount)
    {
        mana.BarCurrentValue -= amount;
    }

    private void RaiserMana(float amount)
    {
        mana.BarCurrentValue += amount;
    }
}
