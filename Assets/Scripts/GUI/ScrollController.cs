using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(ScrollRect))]
public class ScrollController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private int fragmentCount = 4;
    private ScrollRect scrollRect;
    private int _currentPosition = 0;
    private int currentPosition
    {
        get
        {
            return _currentPosition;
        }
        set
        {
            _currentPosition = Mathf.Clamp(value, 0, GetMaxPosition());
        }
    }

    private float startPosition;
    private bool isScrolling = false;

    private float target;
    private int targetId;
    private float scrollSpeed = 1;

    private Action toogleActionAfterScroll;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        SetPosition(PlayerPrefs.GetInt(Strings.MAP_POSITION, 0));
    }

    void Update()
    {
        if (isScrolling)
        {
            scrollRect.verticalNormalizedPosition = Mathf.MoveTowards(scrollRect.verticalNormalizedPosition, target, Time.deltaTime * scrollSpeed);
            if (Mathf.Abs(scrollRect.verticalNormalizedPosition - target) < 0.001f)
            {
                scrollRect.verticalNormalizedPosition = target;
                currentPosition = targetId;
                isScrolling = false;
                PlayerPrefs.SetInt(Strings.MAP_POSITION, currentPosition);
                if (toogleActionAfterScroll != null)
                {
                    toogleActionAfterScroll();
                    toogleActionAfterScroll = null;
                }
            }
        }
    }

    private void ScrollTo(int position, float speed = 1)
    {
        if (isScrolling) return;
        isScrolling = true;
        target = GetNormalizedVerticalPosition(position);
        targetId = position;
        scrollSpeed = speed;
    }

    public void ScrollTo(Action action)
    {
        toogleActionAfterScroll = action;
        ScrollTo(GetMaxPosition(), 0.25f);
    }

    private void SetPosition(int position)
    {
        scrollRect.verticalNormalizedPosition = GetNormalizedVerticalPosition(position);
        currentPosition = position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = scrollRect.verticalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float distance = scrollRect.verticalNormalizedPosition - GetNormalizedVerticalPosition(currentPosition);
        if (distance * fragmentCount > 0.1)
        {
            currentPosition++;
        }
        else if (distance * fragmentCount < -0.1)
        {
            currentPosition--;
        }
        ScrollTo(currentPosition);
    }
    
    private float GetNormalizedVerticalPosition(int position)
    {
        return (float)position / (fragmentCount - 1);
    }

    private int GetMaxPosition()
    {
        int level = PlayerPrefs.GetInt(Strings.CURRENT_LEVEL);
        if (level > 90) return 3;
        if (level > 60) return 2;
        if (level > 30) return 1;
        return 0;
    }
}
