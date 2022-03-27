using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestCollider2D : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Action onClick;
    public Action<bool,Vector2> onPress;

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
        onPress?.Invoke(true,eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.LogWarning(this.name+" exit");
        onPress?.Invoke(false,eventData.position);

    }
}
