using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLenght = 5.0f;
    public GameObject m_Dot;
    public InputModule m_InputModule;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        PointerEventData data = m_InputModule.GetData();
        float targetLenght = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLenght : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast(targetLenght);

        Vector3 endPosition = transform.position + (transform.forward * targetLenght);

        if (hit.collider != null)
            endPosition = hit.point;

        m_Dot.transform.position = endPosition;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float lenght)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLenght);

        return hit;
    }
}
