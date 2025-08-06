using TMPro;
using UnityEngine;

public class AdminPanel : MonoBehaviour
{
    public TMP_InputField timeAmount;
    public TMP_InputField zoomSpeed;
    public TMP_InputField stretchSpeed;
    public TMP_InputField moveSpeed;
    public GameObject adminPanelforUI;
    public GameObject adminPanelforTimer;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            adminPanelforUI.SetActive(!adminPanelforUI.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            adminPanelforTimer.SetActive(!adminPanelforTimer.activeInHierarchy);
        }
    }
}
