using GOAP.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GOAP
{
    public class AIAgent : MonoBehaviour
    {
        [SerializeField] GOAPAction[] _actions;

        public ReadonlyArray<GOAPAction> Actions => _actions;

        [SerializeField]
        List<GOAPGoal> _goals = new List<GOAPGoal>(32);

        private AISensor[] _sensors;

        private Dictionary<Type, AISensor> _sensorDict;

        private bool _isPlanStopped = false;
        private Context _context;
        private GOAPPlan _currentPlan;

        protected virtual void Awake()
        {
            _sensors = GetComponentsInChildren<AISensor>();
            _sensorDict = new Dictionary<Type, AISensor>();

            for (int i = 0; i < _sensors.Length; i++)
            {
                var t = (_sensors[i]).GetType();
                _sensorDict[t] = _sensors[i];
            }

            _context = Context.Create();

            PrepareContext(ref _context);
        }

        protected virtual void Update()
        {
            if (_isPlanStopped) return;

            ReorderGoals();

            if (!_currentPlan.IsComplete() && _currentPlan.Current)
            {
                _currentPlan.Current.ExecuteUpdate(ref _context);

                var promisedStates = _currentPlan.Current.PromisedState;

                bool actionCompleted = true;
                for (int i = 0; i < promisedStates.Length; i++)
                {
                    if (!promisedStates[i].Validate(ref _context))
                    {
                        actionCompleted = false;
                        break;
                    }
                }

                if (actionCompleted)
                {
                    _currentPlan.Next();
                }
            }
        }

        protected virtual void PrepareContext(ref Context context) 
        {
            _context.SetValue(this);
        }

        public TSensor GetSensor<TSensor>() where TSensor : AISensor
        {
            if (!_sensorDict.ContainsKey(typeof(TSensor))) return default;
            return (TSensor)_sensorDict[typeof(TSensor)];
        }

        private void ReorderGoals()
        {
            if (_goals.Count == 0) return;
            var currentGoal = _goals[0];
            _goals.Sort(GoalsComparison);

            if (currentGoal != _goals[0])
            {
                Replan();
            }
        }

        private int GoalsComparison(GOAPGoal x, GOAPGoal y)
        {
            return y.GetPriority(_context).CompareTo(x.GetPriority(_context));
        }

        public void StopPlan()
        {
            _isPlanStopped = true;
        }

        public void Replan()
        {
            if (_goals.Count == 0) return;
            _isPlanStopped = false;
            _currentPlan = GOAPPlanner.CreatePlan(this, ref _context, _goals[0]);
            Debug.Log($"AIAgent {this.name} Replan for Goal: {_goals[0]}");
        }

        public bool HasGoal(GOAPGoal goal)
        {
            return _goals.Contains(goal);
        }

        public void SetGoal(GOAPGoal goal)
        {
            if (HasGoal(goal)) return;
            _goals.Add(goal);
            if (_goals.Count == 1)
            {
                Replan();
            }
        }

        public void RemoveGoal(GOAPGoal goal)
        {
            if (!HasGoal(goal)) return;
            _goals.Remove(goal);
        }
    }
}