using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int connectionsCount;
    public string name;
    [SerializeField] GameObject lrPrefab;
    GameObject previousSegment;
    public List<GameObject> connectedWorlds = new List<GameObject>();
    bool isSelectedWorld, iscurrentWorld;
    public bool isCaptured;

    void Start()
    {
        // get previous segment
        if(!transform.parent.GetComponent<Segment>().isStart) previousSegment = Generation.segments[transform.parent.GetComponent<Segment>().segmentIndex - 1];

        if(previousSegment != null)
        {
            // make connections
            int pvSegmentChildCount = previousSegment.transform.childCount;
            for(int i = 0; i < pvSegmentChildCount; i++)
            {
                int rng = Random.Range(1, 12);
                if(rng == 2)
                {
                    Connect(previousSegment.transform.Find(i.ToString()).gameObject);
                }
            }

            if(connectionsCount == 0)
            {
                Connect(previousSegment.transform.Find(Random.Range(0, pvSegmentChildCount).ToString()).gameObject);
            }
        }
    }

    // connect two worlds duh
    private void Connect(GameObject worldToConnectTo)
    {
        connectedWorlds.Add(worldToConnectTo);
        worldToConnectTo.transform.GetComponent<World>().connectedWorlds.Add(this.gameObject);
        connectionsCount++;
        worldToConnectTo.transform.GetComponent<World>().connectionsCount++;
        // draw line
        GameObject lineGO = Instantiate(lrPrefab);
        lineGO.transform.GetComponent<lineRenderer>().drawLine(this.transform, worldToConnectTo.transform);
    }

    private void Update()
    {
        // change the colors of the highlight shader
        if (playerManager.selectedWorld == this.gameObject)
        {
            isSelectedWorld = true;
            transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 1);
            transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 1);
            transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(2.4f, 1.2f, 2.5f));
            transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(2.4f, 1.2f, 2.5f));
        } else
        {
            isSelectedWorld = false;
            transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(2, 2, 2));
            transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(2, 2, 2));
            if (UIcontroller.worldGO != this.gameObject)
            {
                transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 0);
                transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 0);
            }
        }

        if (playerManager.currentWorld == this.gameObject)
        {
            iscurrentWorld = true;
            transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 1);
            transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 1);
            transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(1.51f, 2.51f, 1.51f));
            transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(1.51f, 2.51f, 1.51f));
        } else
        {
            iscurrentWorld = false;
            transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(2, 2, 2));
            transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", new Color(2, 2, 2));
            if (UIcontroller.worldGO != this.gameObject)
            {
                transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 0);
                transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 0);
            }
        }

        if(isCaptured)
        {
            if(playerManager.currentWorld != this.gameObject && playerManager.selectedWorld != this.gameObject)
            {
                transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 1);
                transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 1);
                transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", Color.cyan * 2);
                transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetColor("_Color", Color.cyan * 2);
            }
        }

    }
}
