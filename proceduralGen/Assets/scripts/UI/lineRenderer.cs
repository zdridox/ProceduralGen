using UnityEngine;

public class lineRenderer : MonoBehaviour
{
    private LineRenderer lr;
    //set line points
    public void drawLine(Transform pos1, Transform pos2)
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, pos1.position);
        lr.SetPosition(1, pos2.position);
    }
}
