using UnityEngine;

public class QuestPickup : MonoBehaviour, IInteractable
{
    public string objectiveId;
    public int amount = 1;


    public void Interact()
    {
        QuestManager.Instance.AddProgress(objectiveId, amount);
        Destroy(gameObject);
    }
}