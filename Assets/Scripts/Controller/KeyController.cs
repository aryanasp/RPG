using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class KeyController : MonoBehaviour
    {
        //All Keys
        private Dictionary<string, Dictionary<string, Action>> _allKeys;
        public Dictionary<string, Dictionary<string, bool>> AllInputs;
        //Movement Keys
        private Dictionary<string, Action> _movementKeys;
        public Dictionary<string, bool> MovementInputs;
        
        //Mouse Positions
        public Dictionary<string, float> MousePositions;
       
        //Attack Keys
        private Dictionary<string, Action> _attackKeys;
        public Dictionary<string, bool> AttackInputs;
        // Start is called before the first frame update
        void Awake()
        {
            InitializeStructures();
            InitializeDefaultKeyBindings();
            InitializeDefaultInputs();
        }
        
        private void InitializeStructures()
        {
            //Initialize complete dictionaries
            _allKeys = new Dictionary<string, Dictionary<string, Action>>();
            AllInputs = new Dictionary<string, Dictionary<string, bool>>();
            //Initialize movement dictionaries
            _movementKeys = new Dictionary<string, Action>();
            MovementInputs = new Dictionary<string, bool>();
            //Initialize mouse dictionaries
            MousePositions = new Dictionary<string, float>();
            //Initialize attack dictionaries
            _attackKeys = new Dictionary<string, Action>();
            AttackInputs = new Dictionary<string, bool>();
            //Add Movement dictionaries
            _allKeys["movement"] = _movementKeys;
            AllInputs["movement"] = MovementInputs;
            //Add Attack dictionaries
            _allKeys["attack"] = _attackKeys;
            AllInputs["attack"] = AttackInputs;
        }

        private void InitializeDefaultKeyBindings()
        {
            //Movements
            _movementKeys["Walk"] = new Action(KeyCode.Mouse1, "Press");
            //Select
            _movementKeys["Select"] = new Action(KeyCode.Mouse0, "Press");
            //Attacks
            _attackKeys["Fire"] = new Action(KeyCode.Q, "Charge");
            _attackKeys["Ice"] = new Action(KeyCode.W, "Charge");
        }
        
        
        private void InitializeDefaultInputs()
        {
            foreach (string type in _allKeys.Keys)
            {
                foreach (string key in _allKeys[type].Keys)
                {
                    AllInputs[type][key] = false;
                }
            }
        }
        
        // Update is called once per frame
        void Update()
        {
            GetInputs();
        }

        void GetInputs()
        {
            if (Camera.main)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MousePositions["X"] = worldPosition.x;
                MousePositions["Y"] = worldPosition.y;
            }
            foreach (string type in _allKeys.Keys)
            {
                foreach (string key in _allKeys[type].Keys)
                {
                    if (_allKeys[type][key].ActionType == "Press")
                    {
                        AllInputs[type][key] = Input.GetKey(_allKeys[type][key].KeyCode);
                        
                    }
                    else if (_allKeys[type][key].ActionType == "Charge")
                    {
                        AllInputs[type][key] = Input.GetKeyDown(_allKeys[type][key].KeyCode);
                    }
                    
                }
            }
            
        }
    }

    public class Action
    {
        public KeyCode KeyCode { get; set; }

        public string ActionType { get; set; }

        public Action(KeyCode keyCode, string actionType)
        {
            KeyCode = keyCode;
            ActionType = actionType;
        }
    }
}
