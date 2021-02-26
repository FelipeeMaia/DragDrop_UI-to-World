using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragDrop : MonoBehaviour
{
    //Dragable gear info.
    public GameObject dragableGear;
    private RectTransform gearTransform;
    private GearColor gearColor;
    private CanvasGroup gearTransparency;

    //move info
    private GearSlot lastSlot;
    private Vector3 lastMousePos;
    private bool isDragging;

    //ui raycast necessary references
    GraphicRaycaster gRaycaster;
    PointerEventData m_PointerEventData;
    EventSystem eventSystem;

    private void Start()
    {
        lastMousePos = Input.mousePosition;

        gRaycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();

        gearTransform = dragableGear.GetComponent<RectTransform>();
        gearColor = dragableGear.GetComponent<GearColor>();
        gearTransparency = dragableGear.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClickDown();

        if (Input.GetMouseButtonUp(0))
            OnClickUp();

        if (isDragging)
            Dragging();

        lastMousePos = Input.mousePosition;
    }

    private void OnClickDown()
    {
        GearSlot _gearSlot = AllRaycasts("Slot");
        if (_gearSlot == null) return;
        if (!_gearSlot.isActive) return;

        lastSlot = _gearSlot;

        //prepara a  engrenagem arrastavel pra seguir
        gearTransform.position = Input.mousePosition;
        gearColor.Color = _gearSlot.GearExit();
        gearTransparency.alpha = 0.7f;

        //liga o arrastamento
        isDragging = true;
    }

    private void OnClickUp()
    {
        if (!isDragging) return;
        GearSlot _gearSlot = AllRaycasts("Slot");

        if (_gearSlot == null)       //se não achar nada devolve a engrenagem
        {
            lastSlot.GearEnter(gearColor.Color);
        }
        else if(!_gearSlot.isActive) //se achar slot vazio, coloca a engrenagem
        {
            _gearSlot.GearEnter(gearColor.Color);
        } 
        else                         //se acahr slot com engrenagem, troca as engrenagens
        {
            lastSlot.GearEnter(_gearSlot.GearExit());
            _gearSlot.GearEnter(gearColor.Color);
        }

        //desliga a engrenagem arrastavel
        gearTransparency.alpha = 0f;
        isDragging = false;
    }

    private void Dragging()
    {
        Vector3 delta = Input.mousePosition - lastMousePos;

        if (delta != Vector3.zero)
            gearTransform.position += delta;
    }

    private GearSlot AllRaycasts(string wantedTag)
    {
        GearSlot raycasted;

        raycasted = WorldRaycast(wantedTag);

        if (raycasted == null)
            raycasted = UIRaycast(wantedTag);

        return raycasted;    
    }

    private GearSlot WorldRaycast(string wantedTag)
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.tag == wantedTag)
                return hit.transform.gameObject.GetComponent<GearSlot>();
        }

        return null;
    }

    private GearSlot UIRaycast(string wantedTag)
    {
        m_PointerEventData = new PointerEventData(eventSystem);
        m_PointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        gRaycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            if (results[0].gameObject.tag == wantedTag)
                return results[0].gameObject.GetComponent<GearSlot>(); ;
        }

        return null;
    }
}
