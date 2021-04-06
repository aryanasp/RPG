using System;
using Model;
using UnityEngine;

namespace Controller
{
    public class DebuffController : MonoBehaviour
    {
        [SerializeField] private Transform underLegs;
        
        //Debuffs status in list
        public DebuffStatus[] DebuffStatusList;
        
        public event Action<GameObject, int, Vector2> DoDebuff;
        
        private void Awake()
        {
            DebuffStatusList = new DebuffStatus[100];
            //Initialize debuff status list
            for (int i = 0; i < DebuffStatusList.Length; i++)
            {
                DebuffStatusList[i] = new DebuffStatus(i, "", 1000f, false);
            }
        }

        public void DebuffReset(string debuffName)
        {
            bool foundPastDebuff = false;
            //check if there is same debuff which is active in the same time
            for (int i = 0; i < DebuffStatusList.Length; i++)
            {
                //reset debuff status
                if (DebuffStatusList[i].DebuffName == debuffName)
                {
                    foundPastDebuff = true;
                    DebuffStatusList[i].IsInDebuff = true;
                    DebuffStatusList[i].TimePassedFromDebuff = 0f;
                }
            }
            //if there is no same debuff, create a new one
            if (!foundPastDebuff)
            {
                //reset debuff status
                int element = AssignFreeDebuffSlot();
                DebuffStatusList[element].DebuffName = debuffName;
                DebuffStatusList[element].IsInDebuff = true;
                DebuffStatusList[element].TimePassedFromDebuff = 0f;
            }

        }

        // Update is called once per frame
        void Update()
        {
            IncrementTimePastFromDebuff();
            Debuff();
            
        }

        private void IncrementTimePastFromDebuff()
        {
            foreach (var debuffStatus in DebuffStatusList)
            {
                if (!debuffStatus.IsFreeSlot)
                {
                    debuffStatus.TimePassedFromDebuff += Time.deltaTime;
                }
            }
        }

        private void Debuff()
        {
            foreach (var debuffStatus in DebuffStatusList)
            {
                if (!debuffStatus.IsFreeSlot)
                {
                    DoDebuff?.Invoke(gameObject, debuffStatus.DebuffSlotId, underLegs.position);
                }
            }
            
        }

        public int AssignFreeDebuffSlot()
        {
            for (int i = 0; i < DebuffStatusList.Length; i++)
            {
                if (DebuffStatusList[i].IsFreeSlot)
                {
                    return i;
                }
            }
            throw new Exception("there is no free slot");
        }
        
    }
    
    public class DebuffStatus
    {
        private bool _isFreeSlot;
        public bool IsFreeSlot => _isFreeSlot;

        private int _debuffSlotId;
        public int DebuffSlotId
        {
            get => _debuffSlotId;
        }
        
        private string _debuffName;
        public string DebuffName
        {
            get => _debuffName;
            set
            {
                _debuffName = value;
                _isFreeSlot = _debuffName == "";
            }
        }

        private float _timePassedFromDebuff;
        public float TimePassedFromDebuff
        {
            get => _timePassedFromDebuff;
            set => _timePassedFromDebuff = Mathf.Clamp(value, 0f, 1000f);
        }

        private bool _isInDebuff;
        public bool IsInDebuff
        {
            get => _isInDebuff;
            set => _isInDebuff = value;
        }

        public DebuffStatus(int debuffSlotId, string debuffName, float timePassedFromDebuff, bool isInDebuff)
        {
            _debuffSlotId = debuffSlotId;
            _debuffName = debuffName;
            _isFreeSlot = debuffName == "";
            _timePassedFromDebuff = timePassedFromDebuff;
            _isInDebuff = isInDebuff;
        }

        public void FreeDebuffSlot()
        {
            _isFreeSlot = true;
            _debuffName = "";
            _timePassedFromDebuff = 1000f;
            IsInDebuff = false;
        }
    }
}
