using UnityEngine;

public class QuestWalkOn : MonoBehaviour
{
    public string objectiveId;
    public int amount = 1;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        QuestManager.Instance.AddProgress(objectiveId, amount);

        Destroy(gameObject);
    }
}
