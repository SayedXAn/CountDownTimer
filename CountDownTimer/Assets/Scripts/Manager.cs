using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;

public class Manager : MonoBehaviour
{
    [System.NonSerialized] public string latestFile = "";
    [System.NonSerialized] Texture2D loadedLocalImage;
    [SerializeField] Image backgroundImage;
    public AdminPanel adminPanel;
    public GameObject startButtonParent;

    [SerializeField] TMP_Text timerText;
    private int timeAmount = 60;
    private int timerVar;

    bool goTimerGo = true;
    void Start()
    {        
        CheckIfAnyBGPhoto();
        timerVar = timeAmount;
        timerText.text = timerVar.ToString() + " Seconds";
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            StopTimer();
        }
    }
    

    public void ShowTimeUp(int lastTime)
    {
        timerText.text = lastTime.ToString("D2") + "\nTime up!";
    }

    public void StopTimer()
    {
        goTimerGo = false;
        StopCoroutine(CountDown());
        StopCoroutine(CountUp());
        timerVar = timeAmount;
        timerText.text = timerVar.ToString() + " Seconds";
        startButtonParent.SetActive(true);
    }

    public void StartCountDown()
    {
        timerVar = timeAmount+1;
        startButtonParent.SetActive(false);
        goTimerGo = true;
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        timerVar--;
        if(goTimerGo && timerVar > 0)
        {
            timerText.text = ConvertSecondToTime(timerVar);
            AnimateTimerText();
            StartCoroutine(CountDown());
        }        
        else if(timerVar == 0)
        {
            timerText.text = "00";
            goTimerGo = false;
            StopCoroutine(CountDown());
            timerVar = timeAmount;
            ShowTimeUp(0);
        }
    }

    public void StartCountUp()
    {
        timerVar = -1;
        startButtonParent.SetActive(false);
        goTimerGo = true;
        StartCoroutine(CountUp());
    }
    IEnumerator CountUp()
    {
        yield return new WaitForSeconds(1);
        timerVar++;
        if(goTimerGo && timerVar < timeAmount)
        {
            timerText.text = ConvertSecondToTime(timerVar);
            AnimateTimerText();
            StartCoroutine(CountUp());
        }
        else if (timerVar == timeAmount)
        {
            timerText.text = timeAmount.ToString();            
            goTimerGo = false;
            StopCoroutine(CountUp());
            timerVar = timeAmount;
            ShowTimeUp(timeAmount);
        }
    }

    private string ConvertSecondToTime(int time)
    {
        int hour, minute, second;
        string retString = "";
        hour = time / 3600;        
        if(hour>0)
        {
            retString += hour.ToString("D2") + ":";
            time = time % 3600;
        }
        minute = time / 60;        
        //if(minute>0 || hour >0)
        //{
        //    retString += minute.ToString("D2") + ":";
        //    time = time % 60;
        //}
        retString += minute.ToString("D2") + ":";
        time = time % 60;

        second = time;
        retString += second.ToString("D2");
        return retString;
    }

    public void SetTimeAmountFromAdmin()
    {
        if(adminPanel.timeAmount.text != null)
        {
            timeAmount = int.Parse(adminPanel.timeAmount.text);
            timerVar = timeAmount; 
            timerText.text = timerVar.ToString() + " Seconds";
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

    public void AnimateTimerText()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(timerText.transform.DOScale(1.25f, 0.25f));
        sequence.Append(timerText.transform.DOScale(1.00f, 0.25f));
    }
}
