using UnityEngine;

[System.Serializable]
public class QuestObjective
{
    public string id;
    public string title;
    public int requiredAmount;
    public int currentAmount;
    public bool isActive = false;

    public bool IsCompleted => currentAmount >= requiredAmount;

    public void AddProgress(int amount)
    {
        currentAmount += amount;
        currentAmount = Mathf.Clamp(currentAmount, 0, requiredAmount);
    }   

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
