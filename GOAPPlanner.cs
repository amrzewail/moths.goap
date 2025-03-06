using GOAP.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public static class GOAPPlanner
    {
        private static List<GOAPAction> _actionPool = new List<GOAPAction>(100);
        private static List<float> _chancePool = new List<float>(100);
        
        public static GOAPPlan CreatePlan(AIAgent agent, ref Context context, GOAPGoal goal)
        {
            var plan = GOAPPlan.Create();
            var goalStates = goal.DesiredStates;
            var agentActions = agent.Actions;

            for (int i = 0; i < goalStates.Length; i++)
            {
                if (goalStates[i].Validate(ref context)) continue;

                var action = PickActionThatPromisesState(agentActions, goalStates[i], ref context);

                if (action == null) return default;

                plan.Push(action);

                action.PrepareContext(ref context);

                goalStates = action.PrerequisiteState;
                i = -1;
            }

            return plan;
        }



        private static GOAPAction PickActionThatPromisesState(ReadonlyArray<GOAPAction> actions, GOAPState state, ref Context context)
        {
            _actionPool.Clear();
            _chancePool.Clear();

            int count = 0;

            float maxChance = 0;

            for (int i = 0; i < actions.Length; i++)
            {
                var action = actions[i];
                var promisedStates = action.PromisedState;

                for (int j = 0; j < promisedStates.Length; j++)
                {
                    if (promisedStates[j] == state)
                    {
                        _actionPool.Add(action);
                        float chance = action.CalculateChance(context);
                        _chancePool.Add(chance);
                        maxChance += chance;
                        count++;
                        break;
                    }
                }
            }

            if (count == 0) return null;

            float random = UnityEngine.Random.Range(0, maxChance);
            float curr = 0;

            for (int i = 0; i < _actionPool.Count; i++)
            {
                float actionChance = _chancePool[i];
                if (random >= curr && random < curr + actionChance)
                {
                    return _actionPool[i];
                }
                curr += actionChance;
            }

            return _actionPool[UnityEngine.Random.Range(0, count)];
        }

    }
}