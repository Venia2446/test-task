using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipController : MonoBehaviour
{
    public GameObject tooltipPrefab;
    public TextMeshProUGUI text;

    public static TooltipController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        tooltipPrefab.SetActive(false);
        camera = Camera.main;
    }

    public void RequestShowTooltip(string text, Vector2 inShift)
    {
        shift = inShift;
        UpdatePresetText(text);
        tooltipPrefab.SetActive(true);
    }

    public void RequestHideTooltip()
    {
        tooltipPrefab.SetActive(false);
    }

    void UpdatePresetText(string inText)
    {
        text.SetText(inText);
    }

    void Update()
    {
        if (tooltipPrefab.activeSelf)
        {
            if (tooltipPrefab.transform.position.Equals(Input.mousePosition))
            {
                return;
            }
            var worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
            var newPosition = new Vector3(worldPos.x - shift.x, worldPos.y - shift.y, 0);
            tooltipPrefab.transform.position = newPosition;
        }
    }

    private Camera  camera;
    private Vector2 shift;
}
