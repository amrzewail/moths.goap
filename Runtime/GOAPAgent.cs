using Moths.GOAP.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Moths.GOAP
{
    public class GOAPAgent : MonoBehaviour
    {
        [SerializeField] bool _autoReplan = false;
        [SerializeField] GOAPAction[] _actions;

        public bool AutoReplan { get => _autoReplan; set => _autoReplan = value; }
        public ReadonlyArray<GOAPAction> Actions => _actions;
        public GOAPPlan Plan => _currentPlan;

        public Context Context;

        [SerializeField]
        List<GOAPGoal> _goals = new List<GOAPGoal>(32);

        private AISensor[] _sensors;

        private Dictionary<Type, AISensor> _sensorDict;

        private bool _isPlanStopped = false;
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

            Context = Context.Create();

            PrepareContext(ref Context);
        }

        protected virtual void Update()
        {
            if (_isPlanStopped) return;

            ReorderGoals();

            if (!_currentPlan.IsDoable(ref Context))
            {
                if (_autoReplan)
                {
                    Replan();
                    return;
                }
                StopPlan();
            }

            if (!_currentPlan.IsComplete() && _currentPlan.Current)
            {
                if (AreGoalsCompleted())
                {
                    _currentPlan.Current.CleanUp(ref Context);
                    _currentPlan.Complete();
                    return;
                }

                _currentPlan.Current.ExecuteUpdate(ref Context);

                var promisedStates = _currentPlan.Current.PromisedState;

                bool actionCompleted = true;
                for (int i = 0; i < promisedStates.Length; i++)
                {
                    if (!promisedStates[i].Validate(ref Context))
                    {
                        actionCompleted = false;
                        break;
                    }
                }

                if (actionCompleted)
                {
                    _currentPlan.Current.CleanUp(ref Context);
                    _currentPlan.Next();
                }
            }
            else if (_autoReplan)
            {
                Replan();
            }
        }

        protected virtual void PrepareContext(ref Context context) 
        {
            Context.SetValue(this);
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

        private bool AreGoalsCompleted()
        {
            for (int i = 0; i < _goals.Count; i++)
            {
                if (!_goals[i].IsCompleted(ref Context)) return false;
            }
            return true;
        }

        private int GoalsComparison(GOAPGoal x, GOAPGoal y)
        {
            return y.GetPriority(Context).CompareTo(x.GetPriority(Context));
        }

        public void StopPlan()
        {
            _isPlanStopped = true;
        }

        public void Replan()
        {
            if (_goals.Count == 0) return;
            _isPlanStopped = false;
            _currentPlan = GOAPPlanner.CreatePlan(this, ref Context, _goals[0]);
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