using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Testingdr
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class AutoPilot_To_Target : MonoBehaviour
    {
        private GameObject target; // The selected target within the game
        private bool isAutopilotActive = false; // Flag to indicate if autopilot is active

        private Text coordinatesText;

        private void Start()
        {
            CreateUI();
        }

        private void Update()
        {
            if (target != null && isAutopilotActive)
            {
                PerformAutopilotTask(); // Perform autopilot task if target is selected and autopilot is active
            }
        }

        private void SelectTarget()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.collider.gameObject;
                UpdateCoordinatesText();
            }
        }

        private void PerformAutopilotTask()
        {
            if (IsSpaceStation(target))
            {
                DockWithSpaceStation();
            }
            else if (IsCelestialBody(target))
            {
                OrbitCelestialBody();
            }
        }

        private bool IsSpaceStation(GameObject obj)
        {
            return obj.CompareTag("SpaceStation");
        }

        private bool IsCelestialBody(GameObject obj)
        {
            return obj.CompareTag("CelestialBody");
        }

        private void UpdateCoordinatesText()
        {
            if (target != null)
            {
                coordinatesText.text = "Coordinates: " + target.transform.position.ToString();
            }
            else
            {
                coordinatesText.text = "Coordinates: N/A";
            }
        }

        private void DockWithSpaceStation()
        {
            // Implement logic to dock with the space station
            Debug.Log("Docking with space station...");
        }

        private void OrbitCelestialBody()
        {
            // Implement logic to orbit around the celestial body
            Debug.Log("Orbiting around celestial body...");
        }

        private void CreateUI()
        {
            // Create a canvas for the autopilot UI
            GameObject canvasGO = new GameObject("AutopilotCanvas");
            Canvas autopilotCanvas = canvasGO.AddComponent<Canvas>();
            autopilotCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Create a panel to hold UI elements
            GameObject panelGO = new GameObject("Panel", typeof(RectTransform));
            panelGO.transform.SetParent(canvasGO.transform);
            RectTransform panelRect = panelGO.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 1);
            panelRect.anchorMax = new Vector2(0, 1);
            panelRect.pivot = new Vector2(0, 1);
            panelRect.sizeDelta = new Vector2(300, 200);
            panelRect.anchoredPosition = new Vector2(10, -10);
            Image panelImage = panelGO.AddComponent<Image>();
            panelImage.color = new Color(0, 0, 0, 0.5f); // semi-transparent black

            // Create a coordinates text element
            GameObject coordinatesGO = new GameObject("CoordinatesText", typeof(RectTransform), typeof(Text));
            coordinatesGO.transform.SetParent(panelGO.transform);
            RectTransform coordinatesRect = coordinatesGO.GetComponent<RectTransform>();
            coordinatesRect.anchorMin = new Vector2(0, 1);
            coordinatesRect.anchorMax = new Vector2(0, 1);
            coordinatesRect.pivot = new Vector2(0, 1);
            coordinatesRect.sizeDelta = new Vector2(280, 30);
            coordinatesRect.anchoredPosition = new Vector2(10, -10);
            coordinatesText = coordinatesGO.GetComponent<Text>();
            coordinatesText.text = "Coordinates: N/A";
            coordinatesText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            coordinatesText.fontSize = 16;
            coordinatesText.color = Color.white;
            coordinatesText.alignment = TextAnchor.UpperLeft;
            coordinatesText.horizontalOverflow = HorizontalWrapMode.Wrap;
            coordinatesText.verticalOverflow = VerticalWrapMode.Overflow;

            // Create a button to activate autopilot
            GameObject autopilotButtonGO = new GameObject("AutopilotButton", typeof(RectTransform), typeof(Button), typeof(Image));
            autopilotButtonGO.transform.SetParent(panelGO.transform);
            RectTransform autopilotButtonRect = autopilotButtonGO.GetComponent<RectTransform>();
            autopilotButtonRect.anchorMin = new Vector2(0, 0);
            autopilotButtonRect.anchorMax = new Vector2(0, 0);
            autopilotButtonRect.pivot = new Vector2(0, 0);
            autopilotButtonRect.sizeDelta = new Vector2(150, 30);
            autopilotButtonRect.anchoredPosition = new Vector2(10, 10);
            Button autopilotButton = autopilotButtonGO.GetComponent<Button>();
            autopilotButton.onClick.AddListener(ActivateAutopilot);
            Image buttonImage = autopilotButtonGO.GetComponent<Image>();
            buttonImage.color = Color.blue; // button color
            Text buttonText = autopilotButtonGO.AddComponent<Text>();
            buttonText.text = "Autopilot to Target";
            buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            buttonText.fontSize = 16;
            buttonText.color = Color.white;
            buttonText.alignment = TextAnchor.MiddleCenter;
        }

        private void ActivateAutopilot()
        {
            isAutopilotActive = true;
        }
    }
}
