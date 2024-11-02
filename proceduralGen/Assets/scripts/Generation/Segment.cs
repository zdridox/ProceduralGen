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
                go.gameObject.GetComponent<World>().isCaptured = true;
            }
        }
        else
        {
            int rng = Random.Range(2, 4);
            for (int i = 0; i < rng; i++)
            {
                // set worlds variables
                GameObject go = Instantiate(worldPrefab, this.transform);
                go.name = i.ToString();
                go.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 46), Random.Range(0, 46), Random.Range(0, 46)));
                if(rng == 3)
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
                } else if (rng == 2)
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
        // get total connections
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
