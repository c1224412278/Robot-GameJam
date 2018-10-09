using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDetent : MonoBehaviour {

    public bool m_isDynamicObj = false;
    private SpriteRenderer m_renderer;
    private Transform m_transform;

	private void Start () {

        m_transform = transform;
        m_renderer = GetComponent<SpriteRenderer>();
        m_renderer.sortingOrder = -Vector2Int.FloorToInt(m_transform.position).y * 2;

        if (m_isDynamicObj)
            StartCoroutine(UpdateDetent());

    }

    private IEnumerator UpdateDetent()
    {
        while (m_isDynamicObj)
        {
            m_renderer.sortingOrder = -Vector2Int.FloorToInt(m_transform.position).y * 2;
            yield return null;
        }
        yield return null;
    }
}
