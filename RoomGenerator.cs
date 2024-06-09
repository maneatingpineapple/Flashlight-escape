using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject keyPrefab;
    public GameObject rechargePrefab;
    int last_exit = 0; //0 means exit facing up, 1 means exit facing right, etc etc
    Vector2 last_room_center = new Vector2(100000000000000001, 1);
    List<(float, float)> used = new List<(float, float)>();
    int currRoom = 0;
    public BoxCollider2D playerbc;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GenerateRoom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GenerateRoom();
        }
    }

    public void GenerateRoom()
    {
        if (last_room_center == new Vector2(100000000000000001, 1))
        {
            int room_exit = Random.Range(0, 4);
            while (room_exit == last_exit)
            {
                room_exit = Random.Range(0, 4);
            }
            GameObject newRoom = Instantiate(roomPrefab, Vector2.zero, Quaternion.identity);
            
            SpriteRenderer[] roomChildren = newRoom.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer c in roomChildren)
            {
                newRoom.GetComponent<RoomScript>().roomObjects.Add(c.gameObject);
                switch (room_exit)
                {
                    case 0:
                        if (c.gameObject.name == "up_center")
                        {
                            c.material.color = Color.red;
                            //c.gameObject.SetActive(false);
                        }
                        break;
                    case 1:
                        if (c.gameObject.name == "right_center")
                        {
                            c.material.color = Color.red;
                            //c.gameObject.SetActive(false);
                        }
                        break;
                    case 2:
                        if (c.gameObject.name == "down_center")
                        {
                            c.material.color = Color.red;
                            //c.gameObject.SetActive(false);
                        }
                        break;
                    case 3:
                        if (c.gameObject.name == "left_center")
                        {
                            c.material.color = Color.red;
                        }
                        break;
                }
            }
            used.Add((0, 0));
            //print("Room rotation is: " + room_exit);
            last_exit = room_exit;
            last_room_center = Vector2.zero;
            GameObject newKey = Instantiate(keyPrefab, Vector2.zero, Quaternion.identity);
            int i = 0;
            print("FDS");
            while (newKey.GetComponent<EdgeCollider2D>().IsTouching(playerbc) || i < 100)
            {
                i++;
                print("Gf");
                newKey.transform.position = last_room_center;
                newKey.transform.position += new Vector3(Random.Range(-3.25f, 3.25f), Random.Range(-3.25f, 3.25f), 0);
            }
            newKey.tag = "Key";
            newRoom.GetComponent<RoomScript>().roomObjects.Add(newKey);
            newRoom.GetComponent<RoomScript>().room_id = currRoom;
        } else
        {
            int room_exit = Random.Range(0, 4);
            while (Mathf.Abs(room_exit-2) == last_exit || Mathf.Abs(room_exit - 4) == last_exit || checkUsed(room_exit, last_room_center))
            {
                room_exit = Random.Range(0, 4);
            }
            GameObject newRoom = Instantiate(roomPrefab, last_room_center, Quaternion.identity);
            

            
            SpriteRenderer[] roomChildren = newRoom.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer c in roomChildren)
            {
                newRoom.GetComponent<RoomScript>().roomObjects.Add(c.gameObject);
                switch (room_exit)
                {
                    case 0:
                        if (c.gameObject.name == "up_center")
                        {
                            c.material.color = Color.red;
                        }
                        break;
                    case 1:
                        if (c.gameObject.name == "right_center")
                        {
                            c.material.color = Color.red;
                        }
                        break;
                    case 2:
                        if (c.gameObject.name == "down_center")
                        {
                            c.material.color = Color.red;
                        }
                        break;
                    case 3:
                        if (c.gameObject.name == "left_center")
                        {
                            c.material.color = Color.red;
                        }
                        break;
                }

                switch (last_exit)
                {
                    case 0:
                        if (c.gameObject.name == "down_center")
                        {
                            c.material.color = Color.red;
                            c.gameObject.SetActive(false);
                            newRoom.transform.position += new Vector3(0,10,0);
                            //while (used.Contains((newRoom.transform.position.x, newRoom.transform.position.y)){

                            //}
                        }
                        break;
                    case 1:
                        if (c.gameObject.name == "left_center")
                        {
                            c.material.color = Color.red;
                            c.gameObject.SetActive(false);
                            newRoom.transform.position += new Vector3(10, 0, 0);
                        }
                        break;
                    case 2:
                        if (c.gameObject.name == "up_center")
                        {
                            c.material.color = Color.red;
                            c.gameObject.SetActive(false);
                            newRoom.transform.position += new Vector3(0, -10, 0);
                        }
                        break;
                    case 3:
                        if (c.gameObject.name == "right_center")
                        {
                            c.material.color = Color.red;
                            c.gameObject.SetActive(false);
                            newRoom.transform.position += new Vector3(-10, 0, 0);
                        }
                        break;
                }
            }
            
            used.Add((newRoom.transform.position.x, newRoom.transform.position.y));

            last_exit = room_exit;
            last_room_center = newRoom.transform.position;
            GameObject newKey = Instantiate(keyPrefab, last_room_center, Quaternion.identity);
            //print("room: " + currRoom + " ----- " + newKey.transform.position);
            newKey.transform.position += new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-3.25f, 3.25f), 0);
            newKey.tag = "Key";
            newRoom.GetComponent<RoomScript>().roomObjects.Add(newKey);
            newRoom.GetComponent<RoomScript>().room_id = currRoom;

            if ((int)Random.Range(1, 7) == 5)
            {
                GameObject newCharge = Instantiate(rechargePrefab, last_room_center, Quaternion.identity);
                newCharge.transform.position += new Vector3(Random.Range(-3.25f, 3.25f), Random.Range(-3.25f, 3.25f), 0);
                newRoom.GetComponent<RoomScript>().roomObjects.Add(newCharge);
            }

        }
        currRoom++;
    }

    bool checkUsed(int room_exit, Vector2 last_room_center)
    {
        float x = last_room_center.x;
        float y = last_room_center.y;

        switch (room_exit)
        {
            case 0:
                if (used.Contains((x, y+10)))
                {
                    return true;
                }
                break;
            case 1:
                if (used.Contains((x + 10, y)))
                {
                    return true;
                }
                break;
            case 2:
                if (used.Contains((x, y - 10)))
                {
                    return true;
                }
                break;
            case 3:
                if (used.Contains((x - 10, y)))
                {
                    return true;
                }
                break;
        }
        
        return false;
    }
}
