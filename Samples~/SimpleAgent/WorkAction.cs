using UnityEngine;

namespace Moths.GOAP.Samples
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GOAP/Work Action")]
    public class WorkAction : GOAPAction
    {
        public override void ExecuteUpdate(ref Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                agent.money++;
                agent.awakeness -= 100;
                agent.satiation -= 50;
                Debug.Log("Agent has worked all day");
            }
        }
    }
}