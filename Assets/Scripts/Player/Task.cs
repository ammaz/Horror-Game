/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Task : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string Name;
        public Sprite Icon;
        public string Description;
    }

    [Header("Info")] public Info Information;

    [System.Serializable]
    public struct Stat
    {
        public int Currency;
        public int XP;
    }

    [Header("Reward")] public Stat Reward = new Stat { Currency = 10, XP = 10 };

    public bool Completed { get; private set; }
    public TaskCompletedEvent QuestCompleted;

    public abstract class TaskGoal : ScriptableObject
    {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;

        public bool Completed { get; protected set; }
        [HideInInspector] public UnityEvent GoalCompleted;

        public virtual string GetDescription()
        {
            return Description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted= new UnityEvent();
        }

        protected void Evaluate()
        {
            if (CurrentAmount >= RequiredAmount)
            {
                Complete();
            }
        }

        private void Complete()
        {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

        public void Skip()
        {
            //Charge the player some game currency
            Complete();
        }
    }

    public List<TaskGoal> Goals;

    public void Initialize()
    {
        Completed = false;
        QuestCompleted = new TaskCompletedEvent();

        foreach(var goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        Completed = Goals.All(g=>g.Completed);
        if (Completed)
        {
            //give reward
            QuestCompleted.Invoke(this);
            QuestCompleted.RemoveAllListeners();
        }

    }
}

public class TaskCompletedEvent : UnityEvent<Task> { }*/
