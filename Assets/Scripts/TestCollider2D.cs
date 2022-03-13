using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestCollider2D : MonoBehaviour,IPointerClickHandler
{
    public Action onClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogWarning(this.name+" click");
        onClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogWarning(this.name+" enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.LogWarning(this.name+" exit");
    }
}
