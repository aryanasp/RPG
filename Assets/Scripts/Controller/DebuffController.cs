using System;
using Model;
using UnityEngine;

namespace Controller
{
    public class DebuffController : MonoBehaviour
    {
        [SerializeField] private Transform underLegs;
        
        //Debuffs status in list
        public DebuffStatus[] DebuffsStatusList;
        
        public event Action<GameObject, int, Transform> DoDebuffAction;
        
        private void Awake()
        {
            DebuffsStatusList = new DebuffStatus[100];
            //Initialize debuff status list
            for (var i = 0; i < DebuffsStatusList.Length; i++)
            {
                DebuffsStatusList[i] = new DebuffStatus(i, "", 1000f, false);
            }
        }

        public void DebuffReset(string debuffName)
        {
            bool foundPastDebuff = false;
            //check if there is same debuff which is active in the same time
            foreach (var debuffStatus in DebuffsStatusList)
            {
                //reset debuff status
                if (debuffStatus.DebuffName == debuffName)
                {
                    foundPastDebuff = true;
                    debuffStatus.IsInDebuff = true;
                    debuffStatus.TimePassedFromDebuff = 0f;
                    debuffStatus.SpecialStatus = 0;
                }
            }
            //if there is no same debuff, create a new one
            if (!foundPastDebuff)
            {
                //reset debuff status
                int element = AssignFreeDebuffSlot();
                DebuffsStatusList[element].DebuffName = debuffName;
                DebuffsStatusList[element].IsInDebuff = true;
                DebuffsStatusList[element].TimePassedFromDebuff = 0f;
                DebuffsStatusList[element].TimePassedFromDebuff = 0;
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
            foreach (var debuffStatus in DebuffsStatusList)
            {
                if (!debuffStatus.IsFreeSlot)
                {
                    debuffStatus.TimePassedFromDebuff += Time.deltaTime;
                }
            }
        }

        private void Debuff()
        {
            foreach (var debuffStatus in DebuffsStatusList)
            {
                if (!debuffStatus.IsFreeSlot)
                {
                    DoDebuffAction?.Invoke(gameObject, debuffStatus.DebuffSlotId, underLegs);
                }
            }
        }

        private int AssignFreeDebuffSlot()
        {
            for (int i = 0; i < DebuffsStatusList.Length; i++)
            {
                if (DebuffsStatusList[i].IsFreeSlot)
                {
                    return i;
                }
            }
            throw new Exception("There is no free slot");
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

        private int _specialStatus;

        public int SpecialStatus
        {
            get => _specialStatus;
            set => _specialStatus = value;
        }

        public DebuffStatus(int debuffSlotId, string debuffName, float timePassedFromDebuff, bool isInDebuff)
        {
            _debuffSlotId = debuffSlotId;
            _debuffName = debuffName;
            _isFreeSlot = debuffName == "";
            _timePassedFromDebuff = timePassedFromDebuff;
            _isInDebuff = isInDebuff;
            _specialStatus = 0;
        }

        public void FreeDebuffSlot()
        {
            _isFreeSlot = true;
            _debuffName = "";
            _timePassedFromDebuff = 1000f;
            _isInDebuff = false;
            _specialStatus = 0;
        }
    }
}
