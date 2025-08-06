using TMPro;
using UnityEngine;

public class AdminPanel : MonoBehaviour
{
    public TMP_InputField timeAmount;
    public TMP_InputField zoomSpeed;
    public TMP_InputField stretchSpeed;
    public TMP_InputField moveSpeed;
    public GameObject adminPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            adminPanel.SetActive(!adminPanel.activeInHierarchy);
        }
    }
}
