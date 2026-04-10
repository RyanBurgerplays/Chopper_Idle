using System.Collections;
using UnityEngine;

public class NeighborScript : MonoBehaviour
{
    public float neighborChopSpeed;
    public GameObject theTree;
    public TreeScript theTreescript;
    public GameManager gameManager;
    private TreeManager treeManagerScript;
    public GameObject TreeManager;
    public GameObject[] TreeCollection;
    public Vector3 treePosition;
    private bool IsFound = false;
    public Vector3 Offset = new Vector3(0.6f, 0f, 0.6f);
    private void Awake()
    {
        gameObject.SetActive(false);

    }
    private void Start()
    {

        treeManagerScript = TreeManager.GetComponent<TreeManager>();
        neighborChopSpeed = 10f;
    }
    public void FirstBought()
    {
        StartCoroutine(FindTree());
    }

    public void CheckArray()
    {
        TreeCollection = treeManagerScript.treeCollections;
    }
    IEnumerator FindTree()
    {
        while (IsFound == false)
        {
            CheckArray();
            int randomNumber = Random.Range(0, TreeCollection.Length);
            theTree = TreeCollection[randomNumber];
            theTreescript = theTree.GetComponent<TreeScript>();
            if (theTreescript.IsUsed == true && theTreescript.canbeused == true)
            {
                Debug.Log("works");
                IsFound = true;
                theTreescript.canbeused = false;
                treePosition = theTree.transform.position;
                
                GoToTree();
            }
            else
            {
                Debug.Log("fail");
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        yield break;

    }    
    public void GoToTree()
    {
        this.transform.LookAt(treePosition);
        this.transform.position = treePosition + Offset;
        StartCoroutine(ChopTree());
    }
    IEnumerator ChopTree()
    {
        yield return new WaitForSeconds(neighborChopSpeed);
        theTreescript.Chopped();
        theTreescript.canbeused = true;
        IsFound = false;
        yield return new WaitForSeconds(5f);
        StartCoroutine(FindTree());
    }
}
