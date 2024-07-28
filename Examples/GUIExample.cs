using KSPTools.GUI;
using UnityEngine;

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
            GUIElement button = new Button("Example Button Text", ButtonFunction);
            GUIElement label = new Label("Example Label Text");

            manager.AddElement(button);
            manager.AddElement(label);

            manager.OpenWindow();
        }
        private void ButtonFunction()
        {
            Debug.Log("Button Pressed");
        }
    }
}
