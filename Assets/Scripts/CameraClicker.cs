using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class CameraClicker : MonoBehaviour
{

    
    RaycastHit hit;
    public Vector3 treePosition;
    public GameObject player;
    private Vector3 playerPosition;
    private bool canChop = true;
    public GameObject theTree;
    public TreeScript theTreescript;
    public GameManager gameManager;
    public float ChopTime = 15f;
    public GameObject currentTree;

    private void Start()
    {
        ChopTime = 15f;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ClickTime();
        }
        playerPosition=player.transform.position;

    } 
    private void ClickTime()
    {
        if (canChop == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.red);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("Tree"))    //if it hits a tree
                {
                    treePosition = hit.transform.position;
                    currentTree = hit.collider.gameObject;
                    //Debug.Log(treePosition);
                    treePosition.y = 0f;
                    GoToTree(treePosition);
                    theTree = hit.collider.gameObject;
                    canChop = false;  // remmeber to reenable
                    theTreescript=theTree.GetComponent<TreeScript>();
                }

            }
        }
    }
    public float speed = 100f;
    private void GoToTree(Vector3 treePosition)
    {
        player.transform.LookAt(treePosition);  //looks at tree
        if (treePosition.x > playerPosition.x) {
            treePosition.x = treePosition.x - 0.5f;
        }                                               //makes sure player doesnt go inside the tree
        else { treePosition.x = treePosition.x + 0.5f; }
            treePosition.y = 0f;       
        treePosition.z = treePosition.z-0.5f;
        Debug.Log(""+treePosition);
        player.transform.LookAt (treePosition, Vector3.up);
        player.transform.position = treePosition;
            //player.transform.position = Vector3.MoveTowards(playerPosition, treePosition, speed);   //teleports the player to the spot. maybe figure out how to have the player walk there?
        StartCoroutine(TreeChopping());
    }
    IEnumerator TreeChopping()
    {
        //play animation
        yield return new WaitForSeconds(ChopTime);
        //stop animation
        theTreescript.Chopped();    //activate chop function for the tree
        canChop = true;
        yield break;
    }
}
