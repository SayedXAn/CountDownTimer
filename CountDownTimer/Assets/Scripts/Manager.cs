using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
//using LitJson;
using System.Linq;
using TMPro;

public class Manager : MonoBehaviour
{
    [System.NonSerialized] public string latestFile = "";
    [System.NonSerialized] Texture2D loadedLocalImage;
    [SerializeField] Image backgroundImage;
    public AdminPanel adminPanel;

    [SerializeField] TMP_Text timerText;
    private int timeAmount = 60;
    private int timerVar;

    bool goTimerGo = true;
    void Start()
    {        
        CheckIfAnyBGPhoto();
        timerVar = timeAmount;
        timerText.text = timerVar.ToString();
    }
    void Update()
    {
        
    }

    public void StartCountDown()
    {
        timerVar = timeAmount;
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        timerVar--;
        timerText.text = ConvertSecondToTime(timerVar);
        if(!goTimerGo || timerVar == 0)
        {
            StopCoroutine(CountDown());
            timerVar = timeAmount;
        }
        else
        {
            StartCoroutine(CountDown());
        }
    }

    private string ConvertSecondToTime(int time)
    {
        int hour, minute, second;
        string retString = "";
        hour = time / 3600;        
        if(hour>0)
        {
            retString += hour.ToString() + ":";
            time = time % 3600;
        }
        minute = time / 60;        
        if(minute>0)
        {
            retString += minute.ToString() + ":";
            time = time % 60;
        }            
        second = time;
        retString += minute.ToString();
        return retString;
    }

    public void SetTimeAmountFromAdmin()
    {
        if(adminPanel.timeAmount.text != null)
        {
            timeAmount = int.Parse(adminPanel.timeAmount.text);
        }
    }

    public void CheckIfAnyBGPhoto()
    {
        string directoryPath = "C:\\CDPhoto";      
        if (!Directory.Exists(directoryPath))
        {
            Debug.LogError("Photos directory not found: " + directoryPath);
            return;
        }

        // Find the latest image file
        latestFile = Directory.GetFiles(directoryPath, "*.jpg")
                              .Concat(Directory.GetFiles(directoryPath, "*.png"))
                              .OrderByDescending(File.GetCreationTime)
                              .FirstOrDefault();

        if (string.IsNullOrEmpty(latestFile))
        {
            Debug.LogError("No photo found to process.");
            backgroundImage.gameObject.SetActive(false);
            return;
        }
        else
        {
            backgroundImage.gameObject.SetActive(true);
            byte[] bytes = File.ReadAllBytes(latestFile);
            loadedLocalImage = new Texture2D(2, 2);
            loadedLocalImage.LoadImage(bytes);
            Sprite sprite = Sprite.Create(loadedLocalImage, new Rect(0, 0, loadedLocalImage.width, loadedLocalImage.height), new Vector2(0.5f, 0.5f));
            backgroundImage.sprite = sprite;
            backgroundImage.type = Image.Type.Simple;
            backgroundImage.preserveAspect = true;

        }
    }
}
