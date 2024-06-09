using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quads : MonoBehaviour
{
    public GameObject ghost_parent;
    BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] check_amount()
    {
        List<GameObject> termsList = new List<GameObject>();

        EdgeCollider2D[] colliders = ghost_parent.GetComponentsInChildren<EdgeCollider2D>();
        foreach (EdgeCollider2D ghost in colliders)
        {
            if (bc.IsTouching(ghost) && ghost.isTrigger) {
                termsList.Add(ghost.gameObject);
            }
        }

        GameObject[] output = termsList.ToArray();

        return output;
    }
}
