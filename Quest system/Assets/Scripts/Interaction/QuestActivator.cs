using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class QuestActivator : MonoBehaviour, IInteractable
{
    [Header("Welke objective activeer je?")]
    public string objectiveId;

    [Tooltip("Activeer automatisch wanneer speler de trigger ingaat")]
    public bool autoActivateOnEnter = false;

    [Tooltip("Destroy dit object nadat geactiveerd (optioneel)")]
    public bool destroyAfterActivate = false;

    [SerializeField] GameObject menuRoot;

    public void Interact()
    {
        Activate();
    }

    void Activate()
    {
        if (QuestManager.Instance == null)
        {
            Debug.LogWarning("Geen QuestManager in scene!");
            return;
        }

        QuestManager.Instance.ActivateObjective(objectiveId);

        if (destroyAfterActivate)
            Destroy(gameObject);

        Close();
    }
    public void Close()
    {
        menuRoot.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}