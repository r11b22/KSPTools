using System;
using UnityEngine;
/* 
 * Requires UnityEngine
 * Requires UnityEngine.CoreModule
 * Requires UnityEngine.TextRenderingModule
 */
namespace KSPTools.GUI
{
    /// <summary>
    /// A helper class for creating Popupdialog GUI
    /// </summary>
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
        /// <summary>
        /// Disable the controls of the player.
        /// </summary>
        public void LockControls()
        {
            InputLockManager.SetControlLock(CONTROLLOCK, CONTROLLOCKNAME);
            Debug.Log("Controls disabled");
        }
        /// <summary>
        /// Enable the controls of the player
        /// </summary>
        public void UnlockControls()
        {
            InputLockManager.RemoveControlLock(CONTROLLOCKNAME);
            Debug.Log("Controls enabled");
        }
        /// <summary>
        /// Create and open the window that is stored in the GUIManager object
        /// </summary>
        /// <param name="controlsLocked"></param>
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
        /// <summary>
        /// Close and cleanup the window. This does not clean up the GUIManager object the window can be reopend any time.
        /// </summary>
        public void CloseWindow()
        {
            PopupDialog.DismissPopup(windowIdentifier);
            UnlockControls();
        }
        /// <summary>
        /// Add a GUIElement to the window
        /// </summary>
        /// <param name="element"></param>
        public void AddElement(GUIElement element)
        {
            dialogGUI.AddChild(element.GetDialogGUIBase());
        }
        /// <summary>
        /// Add any DialogGUIBase element to the window directly
        /// </summary>
        /// <param name="element"></param>
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
