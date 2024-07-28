using System;
using UnityEngine;
/* 
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 * Requires UnityEngine.TextRenderingModule
 */
namespace KSPTools
{
    public class GUIManager
    {
        private string windowIdentifier;
        private string WindowTitle;
        private const string CONTROLLOCKNAME = "UILock";
        DialogGUIBase dialogGUI;
        private const ControlTypes ControlLock = ControlTypes.ALL_SHIP_CONTROLS;

        public GUIManager(string windowIdentifier, string windowTitle)
        {
            this.windowIdentifier = windowIdentifier;
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
        public void OpenWindow(bool ControlsLocked = true)
        {
            DialogGUIButton CloseButton = new DialogGUIButton("Close", CloseWindow, false);
            AddRawElement(CloseButton);
            MultiOptionDialog dialog = new MultiOptionDialog(windowIdentifier, "", WindowTitle, HighLogic.UISkin, dialogGUI);
            PopupDialog.SpawnPopupDialog(dialog, false, HighLogic.UISkin);
            if (ControlsLocked)
            {
                LockControls();
            }
        }
        public void CloseWindow()
        {
            PopupDialog.DismissPopup(windowIdentifier);
            UnlockControls();
        }
        public void AddElement(GUIElement element)
        {
            dialogGUI.AddChild(element.GetDialogGUIBase());
        }
        public void AddRawElement(DialogGUIBase element)
        {
            dialogGUI.AddChild(element);
        }

    }
    public class GUIElement
    { 
        public DialogGUIBase dialogGUI;
        public GUIElement(DialogGUIBase GUIBase)
        {
            dialogGUI = GUIBase;
        }
        public DialogGUIBase GetDialogGUIBase()
        {
            return dialogGUI;
        }
    }
    public class Button : GUIElement
    {
        public Button(string name, Callback callback) : base(new DialogGUIButton(name, callback, false)) { }

    }
    public class Label : GUIElement
    { 
        public Label (string text) : base(new DialogGUILabel(text)) { }
    }



}
