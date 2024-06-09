using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public int room_id = 0;
    public List<GameObject> roomObjects = new List<GameObject>();
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(check());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(room_id < PlayerMovement.room)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator check()
    {
        yield return new WaitForSeconds(0.1f);
        if (room_id != PlayerMovement.room)
        {
            foreach (GameObject obj in roomObjects)
            {
                try
                {
                    SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
                    
                }
                catch (System.Exception e)
                {

                }
            }
        }
        else
        {
            foreach (GameObject obj in roomObjects)
            {
                try
                {
                    SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255);
                    box.SetActive(true);
                }
                catch (System.Exception e)
                {

                }
            }
        }
        
        StartCoroutine(check());
    }
}
