using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moths.GOAP
{
    public abstract class Modifier : ScriptableObject
    {
        public abstract float GetValue(Context context);
    }
}