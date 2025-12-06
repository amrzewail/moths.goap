using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    public interface IGOAPState
    {
        bool Validate(ref Context context);
    }
}