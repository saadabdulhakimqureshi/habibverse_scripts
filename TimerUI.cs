/// <summary>
/// TimerUI - In-Game Timer Display
/// 
/// Displays a countdown or count-up timer on the game UI using TextMeshPro.
/// Used for game modes with time limits and match duration tracking.
/// 
/// Features:
/// - Configurable timer duration
/// - Countdown/count-up toggle
/// - TextMeshPro UI integration
/// - Real-time timer updates
/// 
/// Usage: Place on a Canvas with a TextMeshProUGUI component reference.
/// </summary>

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

    

