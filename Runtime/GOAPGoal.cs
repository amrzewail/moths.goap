using Moths.GOAP.Collections;
using Moths.Serialization;
using UnityEngine;

namespace Moths.GOAP
{
    [CreateAssetMenu(menuName = "Moths/GOAP/Goal")]
    public class GOAPGoal : ScriptableObject
    {
        [SerializeField] InterfaceReference<IGOAPState>[] _desiredStates;
        [SerializeField] int _basePriority = 100;
        [SerializeField] InterfaceReference<IModifier>[] _priorityModifiers;

        public ReadonlyArray<IGOAPState> DesiredStates => new (_desiredStates);

        public int GetPriority(Context context)
        {
            float modifiers = 1;
            for (int i = 0; i < _priorityModifiers.Length; i++) modifiers *= _priorityModifiers[i].Value.GetValue(context);
            return Mathf.CeilToInt(_basePriority * modifiers);
        }

        public bool IsCompleted(ref Context context)
        {
            bool isCompleted = true;
            for (int i = 0; i < _desiredStates.Length; i++)
            {
                if (_desiredStates[i] == null) continue;
                var state = _desiredStates[i].Value;
                if (state == null) continue;
                if (state.Validate(ref context)) continue;
                isCompleted = false;
                break;
            }
            return isCompleted;
        }
    }
}