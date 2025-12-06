using UnityEngine;

namespace Moths.GOAP.Samples
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GOAP/Buy Food Action")]
    public class BuyFoodAction : GOAPAction
    {
        public override void ExecuteUpdate(ref Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                agent.money--;
                agent.food++;
                Debug.Log("Agent has bought food");
            }
        }
    }
}