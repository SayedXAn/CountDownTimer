using UnityEngine;

public class CanvasZoom : MonoBehaviour
{
    // Define variables for canvas object and zoom speed
    public RectTransform[] canvasRect;
    public float zoomSpeed = 0.1f;
    public float stretchSpeed = 0.1f;
    public float moveSpeed = 0.005f;
    public AdminPanel adminPanel;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        // Set the scale of the canvas object to the saved scale
        foreach (RectTransform rect in canvasRect)
        {
            rect.localScale = new Vector3(PlayerPrefs.GetFloat("scaleX", 1f), PlayerPrefs.GetFloat("scaleY", 1f), 0f);
            rect.localPosition = new Vector3(PlayerPrefs.GetFloat("positionX", 0f), PlayerPrefs.GetFloat("positionY", 0f), 0f);
        }
    }

    void Update()
    {
        // Check for keyboard input to zoom in or out
        if (Input.GetKey(KeyCode.W))
        {
            StretchVerticalIn();
        }
        if (Input.GetKey(KeyCode.S))
        {
            StretchVerticalOut();
        }

        if (Input.GetKey(KeyCode.A))
        {
            StretchHorizontalIn();
        }

        if (Input.GetKey(KeyCode.D))
        {
            StretchHorizontalOut();
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            Move(y:1);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            Move(y:-1);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Move(x:-1);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            Move(x:1);
        }

        // Check for keyboard input to zoom in or out
        if (Input.GetKey(KeyCode.Z))
        {
            ZoomIn();
        }
        if (Input.GetKey(KeyCode.X))
        {
            ZoomOut();
        }
        if (Input.GetKey(KeyCode.R))
        {
            ResetPosition();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ResetRect(RectTransform rt)
    {
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = Vector2.zero;
        rt.localRotation = Quaternion.identity;
        rt.localScale = Vector3.one;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = new Vector2(1f, 1f);
        rt.pivot = new Vector2(0.5f, 0.5f);

    }

    // Method to zoom in
    void ZoomIn()
    {
        // Increase the scale of the canvas object

        foreach (RectTransform rect in canvasRect)
        {
            rect.localScale += new Vector3(zoomSpeed*Time.deltaTime, zoomSpeed*Time.deltaTime, 0f);
        }

        PlayerPrefs.SetFloat("scaleX", canvasRect[0].localScale.x);
        PlayerPrefs.SetFloat("scaleY", canvasRect[0].localScale.y);
    }

    // Method to zoom out
    void ZoomOut()
    {
        // Decrease the scale of the canvas object
        foreach (RectTransform rect in canvasRect)
        {
            rect.localScale -= new Vector3(zoomSpeed*Time.deltaTime, zoomSpeed*Time.deltaTime, 0f);
            rect.localScale = new Vector3(Mathf.Max(rect.localScale.x, 0.1f), Mathf.Max(rect.localScale.y, 0.1f), 0f);
        }

        PlayerPrefs.SetFloat("scaleX", canvasRect[0].localScale.x);
        PlayerPrefs.SetFloat("scaleY", canvasRect[0].localScale.y);
    }

    void StretchHorizontalIn()
    {
        foreach (RectTransform rect in canvasRect)
        {
            rect.localScale += new Vector3(stretchSpeed*Time.deltaTime, 0f, 0f);
            rect.localScale = new Vector3(Mathf.Max(rect.localScale.x, 0.1f), Mathf.Max(rect.localScale.y, 0.1f), 0f);
        }
        PlayerPrefs.SetFloat("scaleX", canvasRect[0].localScale.x);
        PlayerPrefs.SetFloat("scaleY", canvasRect[0].localScale.y);
    }

    void StretchHorizontalOut()
    {
        foreach (RectTransform rect in canvasRect)
        {
            rect.localScale -= new Vector3(stretchSpeed*Time.deltaTime, 0f, 0f);
            rect.localScale = new Vector3(Mathf.Max(rect.localScale.x, 0.1f), Mathf.Max(rect.localScale.y, 0.1f), 0f);
        }
        PlayerPrefs.SetFloat("scaleX", canvasRect[0].localScale.x);
        PlayerPrefs.SetFloat("scaleY", canvasRect[0].localScale.y);
    }


    void StretchVerticalIn()
    {
        foreach (RectTransform rect in canvasRect)
        {
            rect.localScale += new Vector3(0f, stretchSpeed*Time.deltaTime, 0f);
            rect.localScale = new Vector3(Mathf.Max(rect.localScale.x, 0.1f), Mathf.Max(rect.localScale.y, 0.1f), 0f);
        }
        PlayerPrefs.SetFloat("scaleX", canvasRect[0].localScale.x);
        PlayerPrefs.SetFloat("scaleY", canvasRect[0].localScale.y);
    }

    void StretchVerticalOut()
    {
        foreach (RectTransform rect in canvasRect)
        {
            rect.localScale -= new Vector3(0f, stretchSpeed*Time.deltaTime, 0f);
            rect.localScale = new Vector3(Mathf.Max(rect.localScale.x, 0.1f), Mathf.Max(rect.localScale.y, 0.1f), 0f);
        }
        PlayerPrefs.SetFloat("scaleX", canvasRect[0].localScale.x);
        PlayerPrefs.SetFloat("scaleY", canvasRect[0].localScale.y);
    }

    void Move(int x=0, int y=0)
    {
        foreach (RectTransform rect in canvasRect)
        {
            rect.localPosition += new Vector3(x*moveSpeed*Time.deltaTime, y*moveSpeed*Time.deltaTime, 0f);
        }
        PlayerPrefs.SetFloat("positionX", canvasRect[0].localPosition.x);
        PlayerPrefs.SetFloat("positionY", canvasRect[0].localPosition.y);
    }

    void ResetPosition()
    {
        foreach (RectTransform rect in canvasRect)
        {
            ResetRect(rect);
        }
        PlayerPrefs.SetFloat("scaleX", canvasRect[0].localScale.x);
        PlayerPrefs.SetFloat("scaleY", canvasRect[0].localScale.y);
    }

    public void SetValuesFromAdminPanel()
    {
        if(adminPanel.zoomSpeed.text != "")
        {
            zoomSpeed = float.Parse(adminPanel.zoomSpeed.text);
        }
        if (adminPanel.stretchSpeed.text != "")
        {
            stretchSpeed = float.Parse(adminPanel.stretchSpeed.text);
        }
        if (adminPanel.moveSpeed.text != "")
        {
            moveSpeed = float.Parse(adminPanel.moveSpeed.text);
        }
    }
}

