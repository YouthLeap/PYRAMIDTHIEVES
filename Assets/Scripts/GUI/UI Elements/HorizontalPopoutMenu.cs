using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class HorizontalPopoutMenu : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField]
    private Sprite enableSprite;
    [SerializeField]
    private Sprite disableSprite;
    [SerializeField]
    private Image targetGraphic;
    [SerializeField]
    private Image iconGraphic;
    [SerializeField]
    private bool isOn = false;
    [SerializeField]
    private RectTransform content;

    private float width;
    private bool isToggling;
    private float speed = 10;

    private Vector3 iconPosition;
    private Vector3 iconOffset = Vector3.up * 0.5f;
    
    void Start()
    {
        width = content.sizeDelta.x;
        if (isOn) ToggleMenu();
        iconPosition = iconGraphic.rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (isToggling)
        {
            Vector2 size = content.sizeDelta;
            float targetSize = isOn ? width : size.y;
            if (Mathf.Abs(size.x - targetSize) > 10)
            {
                //size.x += Mathf.Sign(targetSize - size.x) * speed * Time.deltaTime;
                size.x = Mathf.Lerp(size.x, targetSize, Time.deltaTime * speed);
            }
            else
            {
                isToggling = false;
                size.x = targetSize;
            }
            content.sizeDelta = size;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleMenu();
    }

    private void ToggleMenu()
    {
        isToggling = true;
        isOn = !isOn;
        targetGraphic.sprite = isOn ? enableSprite : disableSprite;
        iconGraphic.rectTransform.anchoredPosition = isOn ? iconPosition : iconPosition + iconOffset;
    }
}
