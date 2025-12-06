using System;
using UnityEngine;

namespace Moths.GOAP.Samples
{
    [Serializable]
    public class IsSatiated : IGOAPState
    {
        public bool Validate(ref Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                return agent.satiation > 25;
            }

            return false;
        }
    }

    [Serializable]
    public class IsAwake : IGOAPState
    {
        public bool Validate(ref Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                return agent.awakeness > 25;
            }

            return false;
        }
    }

    [Serializable]
    public class HasFood : IGOAPState
    {
        public bool Validate(ref Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                return agent.food > 0;
            }

            return false;
        }
    }

    [Serializable]
    public class HasMoney : IGOAPState
    {
        public bool Validate(ref Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                return agent.money > 0;
            }

            return false;
        }
    }

    [Serializable]
    public class NotGetFired : IGOAPState
    {
        public bool Validate(ref Context context)
        {
            return false;
        }
    }

    [Serializable]
    public class IsHungry : IModifier
    {
        public float GetValue(Context context)
        {
            if (context.TryReadValue(out Agent agent))
            {
                return Mathf.Lerp(1, 0, Mathf.Clamp01(agent.satiation / 50f));
            }
            return 0;
        }
    }
}