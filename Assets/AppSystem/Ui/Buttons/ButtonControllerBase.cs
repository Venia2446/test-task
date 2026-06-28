using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonControllerBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(HandleOnClick);
        Init();
    }

    protected virtual void Init()
    {

    }

    protected virtual void Terminate()
    {
        button.onClick?.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        Terminate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverIn();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverOut();
    }

    protected virtual void HoverIn()
    {

    }

    protected virtual void HoverOut()
    {

    }

    protected virtual void HandleOnClick() 
    { 
    
    }

    private Button button;

}
