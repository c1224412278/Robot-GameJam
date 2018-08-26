using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    [SerializeField] private GameObject m_ObjWater;
    private void Start()
    {
        StartCoroutine(Fn_BornWater());
    }
    private IEnumerator Fn_BornWater()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            Instantiate(m_ObjWater , new Vector3(Random.Range(-5f , 5f) , Random.Range(-3f , 3f) , 0f) , Quaternion.identity);
        }
    }
}
