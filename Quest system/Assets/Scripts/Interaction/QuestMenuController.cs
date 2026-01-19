using UnityEngine;

public class QuestMenuController : MonoBehaviour
{
    [SerializeField] GameObject menuRoot;

    public bool IsOpen { get; private set; }

    public void Open()
    {
        if (IsOpen) return;

        menuRoot.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        IsOpen = true;
    }

    public void Close()
    {
        if (!IsOpen) return;

        menuRoot.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        IsOpen = false;
    }

    public void Toggle()
    {
        if (IsOpen) Close();
        else Open();
    }
}
