using UnityEngine;
using TMPro;
using System.Text;

public class QuestUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questText;

    void Start()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestsChanged += UpdateQuestUI;
        }

        UpdateQuestUI();
    }

    void OnDisable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestsChanged -= UpdateQuestUI;
    }

    void UpdateQuestUI()
    {
        if (QuestManager.Instance == null || questText == null) return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("QUESTS");

        foreach (QuestObjective obj in QuestManager.Instance.objectives)
        {
            if (!obj.isActive) continue;

            if (obj.IsCompleted)
                sb.AppendLine($" {obj.title ?? obj.id} is afgemaakt!");
            else
                sb.AppendLine($"{obj.title ?? obj.id}: {obj.currentAmount} / {obj.requiredAmount}");
        }

        questText.text = sb.ToString();
    }
}
