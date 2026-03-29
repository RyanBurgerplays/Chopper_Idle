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
    public float ChopTime;
    public GameObject currentTree;
    public Vector3 Offset = new Vector3(0.6f, 0f, 0.6f);

    private void Start()
    {
        ChopTime = 15f; //REMEMBER TO CHANGE BACK TO 15
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
                    theTreescript = hit.collider.gameObject.transform.GetComponent<TreeScript>();
                    if(theTreescript.canbeused == true)
                    {
                        treePosition = hit.transform.position;
                        currentTree = hit.collider.gameObject;
                        //theTreescript = theTree.GetComponent<TreeScript>();
                        //Debug.Log(treePosition);
                        
                        
                        theTree = hit.collider.gameObject;
                        canChop = false;  // remmeber to reenable
                        theTreescript.canbeused = false;
                        GoToTree(treePosition);
                    }
                 
                }

            }
        }
    }
    public float speed = 100f;
    private void GoToTree(Vector3 treePosition)
    {

        player.transform.LookAt (theTree.transform.position);
        treePosition.y = 0f;
        player.transform.position = treePosition + Offset;
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
