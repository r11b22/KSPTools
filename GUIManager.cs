using System;
using UnityEngine;
/* 
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 * Requires UnityEngine.TextRenderingModule
 */
namespace KSPTools.GUI
{
    public class GUIManager
    {
        private string windowIdentifier;
        private string windowTitle;
        private const string CONTROLLOCKNAME = "UILock";
        DialogGUIBase dialogGUI;
        private const ControlTypes CONTROLLOCK = ControlTypes.ALL_SHIP_CONTROLS;

        public GUIManager(string windowIdentifier, string windowTitle)
        {
            this.windowIdentifier = windowIdentifier;
            this.windowTitle = windowTitle;
            dialogGUI = new DialogGUIVerticalLayout();
        }
        public void LockControls()
        {
            InputLockManager.SetControlLock(CONTROLLOCK, CONTROLLOCKNAME);
            Debug.Log("Controls disabled");
        }
        public void UnlockControls()
        {
            InputLockManager.RemoveControlLock(CONTROLLOCKNAME);
            Debug.Log("Controls enabled");
        }
        public void OpenWindow(bool controlsLocked = true)
        {
            DialogGUIButton closeButton = new DialogGUIButton("Close", CloseWindow, false);
            AddRawElement(closeButton);
            MultiOptionDialog dialog = new MultiOptionDialog(windowIdentifier, "", windowTitle, HighLogic.UISkin, dialogGUI);
            PopupDialog.SpawnPopupDialog(dialog, false, HighLogic.UISkin);
            if (controlsLocked)
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
