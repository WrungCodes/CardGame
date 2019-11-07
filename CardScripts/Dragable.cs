using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public Transform ParentToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentToReturnTo = this.transform.parent;
        this.transform.SetParent(GameObject.Find("Board").transform);

        Debug.Log(this.transform.parent.name);
        Debug.Log(this.transform.GetComponent<CardDisplay>().getCard());

        //screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, screenPoint.z));


        GetComponent<CanvasGroup>().blocksRaycasts = false;
        Debug.Log(this.transform.parent.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector3 cursorPoint = new Vector3(eventData.position.x, eventData.position.y, screenPoint.z);
        //Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
            this.transform.SetParent(ParentToReturnTo);
            Debug.Log(this.transform.parent.name);

            GetComponent<CanvasGroup>().blocksRaycasts = true;
        
    }
}
