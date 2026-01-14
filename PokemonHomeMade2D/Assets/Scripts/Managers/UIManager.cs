using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private MicroMenuController microMenuController;
    [SerializeField] private CheatController cheatController;

    private enum UIState
    {
        None,
        Cheat,
        MicroMenu,
    }

    private UIState currentState = UIState.None;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        microMenuController.OnClose += this.OnMicroMenuClose;
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // 1. Touche '²' (Backquote) pour ouvrir/fermer
        if (keyboard.backquoteKey.wasPressedThisFrame && (currentState==UIState.None || currentState == UIState.Cheat))
        {
            cheatController.ToggleConsole(currentState == UIState.None);
            currentState = currentState == UIState.None? UIState.Cheat : UIState.None;
        }

        if(keyboard.tabKey.wasPressedThisFrame && (currentState == UIState.None || currentState == UIState.MicroMenu))
        {
            microMenuController.ToggleMenu(currentState == UIState.None);
            currentState = currentState == UIState.None ? UIState.MicroMenu : UIState.None;
        }
    }

    private void OnMicroMenuClose()
    {
        if (currentState != UIState.MicroMenu) return;

        microMenuController.ToggleMenu(false);
        currentState = UIState.None;
    }
}
