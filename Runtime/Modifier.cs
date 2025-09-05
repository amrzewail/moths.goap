using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moths.GOAP
{
    public interface IModifier
    {
        public abstract float GetValue(Context context);
    }
}