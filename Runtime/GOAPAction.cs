using Moths.GOAP.Collections;
using Moths.Serialization;
using UnityEngine;

namespace Moths.GOAP
{
    public abstract class GOAPAction : ScriptableObject
    {
        [SerializeField] float _chance = 10;
        [SerializeField] InterfaceReference<IGOAPState>[] _prerequisiteStates;
        [SerializeField] InterfaceReference<IGOAPState>[] _promisedStates;
        [SerializeField] InterfaceReference<IModifier>[] _chanceModifiers;

        public ReadonlyArray<IGOAPState> PrerequisiteState => new ReadonlyArray<IGOAPState>(_prerequisiteStates);

        public ReadonlyArray<IGOAPState> PromisedState => new ReadonlyArray<IGOAPState>(_promisedStates);


        public float CalculateChance(Context context)
        {
            float cost = _chance;
            for (int i = 0; i < _chanceModifiers.Length; i++) cost *= _chanceModifiers[i].Value.GetValue(context);
            return cost;
        }

        public virtual void PrepareContext(ref Context context) { }

        public abstract void ExecuteUpdate(ref Context context);

        public abstract void CleanUp(ref Context context);

        protected virtual void Complete(ref Context context, bool success)
        {
            if (success)
            {
            }
        }
    }
}