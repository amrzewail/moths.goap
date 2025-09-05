using Moths.GOAP.Collections;
using Moths.Serialization;
using UnityEngine;

namespace Moths.GOAP
{
    [CreateAssetMenu(menuName = "Moths/GOAP/Goal")]
    public class GOAPGoal : ScriptableObject
    {
        [SerializeField] GOAPState[] _desiredStates;
        [SerializeField] int _basePriority = 100;
        [SerializeField] InterfaceReference<IModifier>[] _priorityModifiers;

        public ReadonlyArray<GOAPState> DesiredStates => new ReadonlyArray<GOAPState>(_desiredStates);

        public int GetPriority(Context context)
        {
            float modifiers = 1;
            for (int i = 0; i < _priorityModifiers.Length; i++) modifiers *= _priorityModifiers[i].Value.GetValue(context);
            return Mathf.CeilToInt(_basePriority * modifiers);
        }
    }
}