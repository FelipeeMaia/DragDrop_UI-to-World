using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite normalSprite, clickedSprite;
    public RectTransform textTransform;
    public Vector3 clickOffset;

    public UnityEvent OnClick;

    private Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = clickedSprite;
        textTransform.position -= clickOffset;

        OnClick.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = normalSprite;
        textTransform.position += clickOffset;
    }
}
