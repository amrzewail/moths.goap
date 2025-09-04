using Moths.GOAP.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    [CreateAssetMenu(menuName = "Moths/GOAP/World State")]
    public class WorldState : ScriptableObject
    {
        [SerializeField] List<GOAPState> _states;

        public ReadonlyList<GOAPState> States => _states;

        public void RemoveState(GOAPState state)
        {
            _states.Remove(state);
        }

        public void SetState(GOAPState state)
        {
            if (_states.Contains(state)) return;
            _states.Add(state);
        }

        public bool HasState(GOAPState state)
        {
            return _states.Contains(state);
        }
    }
}