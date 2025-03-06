using GOAP.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public abstract class GOAPAction : ScriptableObject
    {
        [SerializeField] float _chance = 10;
        [SerializeField] GOAPState[] _prerequisiteStates;
        [SerializeField] GOAPState[] _promisedStates;
        [SerializeField] Modifier[] _chanceModifiers;

        public ReadonlyArray<GOAPState> PrerequisiteState => new ReadonlyArray<GOAPState>(_prerequisiteStates);

        public ReadonlyArray<GOAPState> PromisedState => new ReadonlyArray<GOAPState>(_promisedStates);


        public float CalculateChance(Context context)
        {
            float cost = _chance;
            for (int i = 0; i < _chanceModifiers.Length; i++) cost *= _chanceModifiers[i].GetValue(context);
            return cost;
        }

        public virtual void PrepareContext(ref Context context) { }

        public abstract void ExecuteUpdate(ref Context context);

        protected virtual void Complete(ref Context context, bool success)
        {
            if (success)
            {
            }
        }
    }
}