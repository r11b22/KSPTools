using KSPTools.GUI;
using UnityEngine;
/*
 * this example creates a window with a button and a label on flight start
 */
namespace KSPToolsTest.Examples
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class GUIExample
    {
        GUIManager manager;
        public void Start()
        {
            manager = new GUIManager("ExampleWindow", "Example Window Title");
            CreateWindow();
        }
        private void CreateWindow()
        {
            //These are helper classes for the actual DialogGUI objects
            GUIElement button = new Button("Example Button Text", ButtonFunction);
            GUIElement label = new Label("Example Label Text");

            //Adding the element to the window
            manager.AddElement(button);
            manager.AddElement(label);

            //After adding all the elements create and open the window
            manager.OpenWindow();
        }
        private void ButtonFunction()
        {
            Debug.Log("Button Pressed");
        }
    }
}
