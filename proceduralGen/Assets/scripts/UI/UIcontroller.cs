using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{

    public static GameObject worldGO;
    [SerializeField] private TMP_Text name, takenOver, numOfConn, numOfEnem;
    void Update()
    {
            // get mouse click input      
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //change hover shader
            if(Physics.Raycast(ray, out hit))
            {
            if (hit.transform.tag == "world" && Input.GetMouseButtonDown(0))
                {
                worldGO = hit.transform.gameObject;
                worldGO.transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 1);
                    worldGO.transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 1);                
                    playerManager.selectedWorld = worldGO;
                
            } 
            } else
        {
            if (worldGO != null && worldGO != playerManager.selectedWorld)
            {
                worldGO.transform.Find("c1").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 0);
                worldGO.transform.Find("c2").gameObject.GetComponent<Renderer>().materials[1].SetInt("_on_off", 0);
                worldGO = null;
            }
        } 


        // set mainUI variables
        if(playerManager.selectedWorld != null)
        {
            name.text = "name: " + playerManager.selectedWorld.GetComponent<World>().name;
            takenOver.text = "isTakenOver: " + playerManager.selectedWorld.gameObject.GetComponent<World>().isCaptured.ToString();
            numOfConn.text = "number of connections: " + playerManager.selectedWorld.gameObject.GetComponent<World>().connectionsCount.ToString();
        }
    }


    // take over button 
    public static void Button1()
    {
        if(playerManager.selectedWorld != null && playerManager.currentWorld.transform.gameObject.GetComponent<World>().connectedWorlds.Contains(playerManager.selectedWorld))
        {
            playerManager.selectedWorld.gameObject.GetComponent<World>().isCaptured = true;
        }
    }

    // go to world button
    public static void Button2()
    {
        if(playerManager.selectedWorld.gameObject.GetComponent<World>().isCaptured)
        {
            playerManager.currentWorld = playerManager.selectedWorld;
        }
    }
}
