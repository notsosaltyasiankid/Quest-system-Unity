using UnityEngine;
using System.Collections.Generic;
using System;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Alle beschikbare objectives (vul in inspector)")]
    public List<QuestObjective> objectives = new List<QuestObjective>();

    public event Action OnQuestsChanged;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ActivateObjective(string objectiveId)
    {
        QuestObjective obj = objectives.Find(o => o.id == objectiveId);
        if (obj == null)
        {
            Debug.LogWarning("Objective not found: " + objectiveId);
            return;
        }

        if (!obj.isActive)
        {
            obj.Activate();
            Debug.Log("Activated objective: " + objectiveId);
            OnQuestsChanged?.Invoke();
        }
    }

    public void DeactivateObjective(string objectiveId)
    {
        QuestObjective obj = objectives.Find(o => o.id == objectiveId);
        if (obj == null) return;
        if (obj.isActive)
        {
            obj.Deactivate();
            OnQuestsChanged?.Invoke();
        }
    }

    public void AddProgress(string objectiveId, int amount)
    {
        QuestObjective obj = objectives.Find(o => o.id == objectiveId);

        if (obj == null)
        {
            Debug.LogWarning("Objective not found: " + objectiveId);
            return;
        }

        if (!obj.isActive)
        {
            Debug.Log($"Tried to add progress to inactive objective: {objectiveId}");
            return;
        }

        obj.AddProgress(amount);
        Debug.Log($"{obj.id}: {obj.currentAmount}/{obj.requiredAmount}");

        if (obj.IsCompleted)
        {
            Debug.Log("Objective completed: " + obj.id);
        }

        OnQuestsChanged?.Invoke();
    }
}
