using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class TrashBin : MonoBehaviour,  IPointerEnterHandler {
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (LevelEditor.instance.selectedObject != null)
        //    LevelEditor.instance.selectedObject.Remove();
    }
}
