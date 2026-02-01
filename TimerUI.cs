using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Timer;

    [SerializeField] float timer = 50f;
    [SerializeField] bool timeLeft = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCount();
    }

    void CheckCount()
    {
        if (timeLeft)
        {
            if (timer <= 0)
            {
                ReadySelect.Instance.SetPlayerReady();
                CameraManager.instance.ShowLobby();
                timeLeft = false;
            }
            else
            {
                timer -= Time.deltaTime;
                Timer.text = (timer % 60).ToString("00");
            }
        }
    }
}

    

