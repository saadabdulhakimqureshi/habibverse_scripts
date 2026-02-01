/// <summary>
/// PlayerUI - Player In-Game UI Manager
/// 
/// General player UI management during gameplay.
/// Manages crosshairs, sliders, and other gameplay UI elements.
/// 
/// Features:
/// - Crosshair display
/// - UI slider management
/// - Player controller integration
/// - Gameplay UI coordination
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public PlayerController PlayerController;

    public GameObject CrossHairs;
    public GameObject Slider;
    // Start is called before the first frame update
    void Start()
    {

        PlayerController.OnPlayerAim += PlayerController_OnPlayerAim;
        PlayerController.OnPlayerStopAim += PlayerController_OnPlayerStopAim;
        //Hide();
    }

    private void PlayerController_OnPlayerAim(object sender, System.EventArgs e)
    {
        Show();
    }

    private void PlayerController_OnPlayerStopAim(object sender, System.EventArgs e)
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
