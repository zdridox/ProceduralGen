using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Generation : MonoBehaviour
{
    private bool isStart;
    [SerializeField] private GameObject segmentPrefab;
    public static List<GameObject> segments = new List<GameObject>();
    public static int segmentsNum = 6;

    void Start()
    {
     
    }

    void Update()
    {
        // spawn segments
        if (segments.Count <= segmentsNum)
        {
            GameObject nextSegment = Instantiate(segmentPrefab, this.transform);
            segments.Add(nextSegment);
            nextSegment.transform.localPosition = new Vector3(0, 0, -5 * segments.Count);
            nextSegment.transform.GetComponent<Segment>().segmentIndex = segments.IndexOf(nextSegment);
        }
    }
}
