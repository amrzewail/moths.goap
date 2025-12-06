using UnityEngine;

namespace Moths.GOAP.Samples
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GOAP/Sleep Action")]
    public class SleepAction : GOAPAction
    {
        public override void ExecuteUpdate(ref Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                agent.awakeness = 100;
                agent.satiation -= 10;
                Debug.Log("Agent has slept");
            }
        }
    }
}