using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Quads : MonoBehaviour
{
    public Quads q1;
    public Quads q2;
    public Quads q3;
    public Quads q4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] q1Ghosts = q1.check_amount();
        if (q1Ghosts.Length > 2)
        {
            q1Ghosts[0].GetComponent<GhostScript>().GoToQuadrant(q2.gameObject);
        }
        GameObject[] q2Ghosts = q2.check_amount();
        if (q2Ghosts.Length > 2)
        {
            q2Ghosts[0].GetComponent<GhostScript>().GoToQuadrant(q3.gameObject);
        }
        GameObject[] q3Ghosts = q3.check_amount();
        if (q3Ghosts.Length > 2)
        {
            GhostScript gs = q3Ghosts[0].GetComponent<GhostScript>();
            gs.GoToQuadrant(q4.gameObject);
            
        }
        GameObject[] q4Ghosts = q4.check_amount();
        if (q4Ghosts.Length > 2)
        {
            q4Ghosts[0].GetComponent<GhostScript>().GoToQuadrant(q1.gameObject);
        }
    }
}
