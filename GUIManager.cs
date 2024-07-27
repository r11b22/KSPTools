using UnityEngine;
/* Requires UnityEngine
 * Requires UnityEngine.TextRenderingModule
 */
namespace KSPTools
{
    public class GUIManager
    {
        private string WindowName;
        private string WindowTitle;
        private const string CONTROLLOCKNAME = "UILock";
        DialogGUIBase dialogGUI;
        private const ControlTypes ControlLock = ControlTypes.ALL_SHIP_CONTROLS;

        public GUIManager(string windowName, string windowTitle)
        {
            WindowName = windowName;
            WindowTitle = windowTitle;
            dialogGUI = new DialogGUIVerticalLayout();

        }
        public void LockControls()
        {
            InputLockManager.SetControlLock(ControlLock, CONTROLLOCKNAME);
            Debug.Log("Controls disabled");
        }
        public void UnlockControls()
        {
            InputLockManager.RemoveControlLock(CONTROLLOCKNAME);
            Debug.Log("Controls enabled");
        }
        public void CreateWindow(bool ControlsLocked = true)
        {
            DialogGUIButton CloseButton = new DialogGUIButton("Close", CloseWindow, false);
            AddElement(CloseButton);
            MultiOptionDialog dialog = new MultiOptionDialog(WindowName, "", WindowTitle, HighLogic.UISkin, dialogGUI);
            PopupDialog.SpawnPopupDialog(dialog, false, HighLogic.UISkin);
            if (ControlsLocked)
            {
                LockControls();
            }
        }
        public void CloseWindow()
        {
            PopupDialog.DismissPopup(WindowName);
            UnlockControls();
        }
        public void AddElement(DialogGUIBase element)
        {
            dialogGUI.AddChild(element);
        }

    }
}
