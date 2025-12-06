using UnityEngine;

namespace Moths.GOAP.Samples
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GOAP/Eat Action")]
    public class EatAction : GOAPAction
    {
        public override void ExecuteUpdate(ref Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                agent.satiation = 100;
                Debug.Log("Agent has eaten");
            }
        }
    }
}