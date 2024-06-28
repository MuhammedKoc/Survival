using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using Object = UnityEngine.Object;

public class ArteusLibrary : MonoBehaviour
{
    static int UILayer = LayerMask.NameToLayer("UI");

    //TODO: Summary'leri doldur.
    
    /// <summary>
    /// It waits until the given Object is not Null.
    /// </summary>
    /// <returns> </returns>
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    /// <summary>
    /// It waits until the given Object is not Null.
    /// </summary>
    /// <param name="object">expected parameter value.</param>
    private static IEnumerator WaitForObejctNotNull(Object variable, Action onNotNull)
    {
        yield return new WaitUntil(() => variable != null);
        
        onNotNull.Invoke();
    }
}
