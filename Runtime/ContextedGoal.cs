using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moths.GOAP
{
    public struct ContextedGoal
    {
        public int Id { get; private set; }
        public Context Context { get; private set; }
        public GOAPGoal Goal { get; private set; }

        public ContextedGoal(GOAPGoal goal, Context context)
        {
            Id = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            Goal = goal;
            Context = context;
        }

        public static bool operator == (ContextedGoal lhs, ContextedGoal rhs)
        {
            return lhs.Id == rhs.Id;
        }

        public static bool operator !=(ContextedGoal lhs, ContextedGoal rhs)
        {
            return lhs.Id != rhs.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is ContextedGoal)
            {
                return (ContextedGoal)obj == this;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}