using UnityEngine;
using UnityEngine.Jobs;

public class Segment : MonoBehaviour
{
    public bool isStart, isEnd;
    public int segmentIndex, totalConnectionsCount;
    [SerializeField] private GameObject worldPrefab;
    public GameObject genParent;

    void Start()
    {
        if(segmentIndex == 0) isStart = true;
        if(segmentIndex == Generation.segmentsNum) isEnd = true;

        // spawn worlds
        if (isStart || isEnd)
        {
            GameObject go = Instantiate(worldPrefab, this.transform);
            go.name = "0";
            if(isStart)
            {
                playerManager.currentWorld = go;
                playerManager.selectedWorld = go;
                go.GetComponent<World>().isCaptured = true;
                go.GetComponent<World>().name = "start";
            }

            if(isEnd)
            {
                go.GetComponent<World>().name = "end";
            }
        }
        else
        {
            // better rng
            int rng = Random.Range(1, 1000);
            int index = 0;
            if(rng <= 500)
            {
                index = 2;
            } else
            {
                index = 3;
            }
            for (int i = 0; i < index; i++)
            {
                // set worlds variables
                GameObject go = Instantiate(worldPrefab, this.transform);
                go.name = i.ToString();
                go.GetComponent<World>().name = Random.Range(1, 1000).ToString();
                go.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 46), Random.Range(0, 46), Random.Range(0, 46)));
                if(index == 3)
                {
                    switch (i)
                    {
                        case 0:
                            go.transform.localPosition = Vector3.zero;
                            break;
                        case 1:
                            go.transform.localPosition = new Vector3(5, 0, 0);
                            break;
                        case 2:
                            go.transform.localPosition = new Vector3(-5, 0, 0);
                            break;
                    }
                } else if (index == 2)
                {
                    switch (i)
                    {
                        case 0:
                            go.transform.localPosition = new Vector3(2.5f, 0, 0);
                            break;
                        case 1:
                            go.transform.localPosition = new Vector3(-2.5f, 0, 0);
                            break;
                    }
                }
            }
        }
    }

    void Update()
    {
        // get total connections // useless
        int childCount = transform.childCount;
        switch (childCount)
        {
            case 1:
                totalConnectionsCount = transform.Find("0").GetComponent<World>().connectionsCount;
                break;
            case 2:
                totalConnectionsCount = transform.Find("0").GetComponent<World>().connectionsCount + transform.Find("1").GetComponent<World>().connectionsCount;
                break;
            case 3:
                totalConnectionsCount = transform.Find("0").GetComponent<World>().connectionsCount + transform.Find("1").GetComponent<World>().connectionsCount + transform.Find("2").GetComponent<World>().connectionsCount;
                break;
        }
    }
}
