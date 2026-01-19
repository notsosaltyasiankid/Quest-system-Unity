using UnityEngine;

public class QuestMenuInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] QuestMenuController questMenu;

    public void Interact()
    {
        if (questMenu == null)
        {
            Debug.LogWarning("QuestMenuController not assigned");
            return;
        }

        questMenu.Toggle();
    }
}