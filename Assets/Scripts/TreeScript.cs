using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class TreeScript : MonoBehaviour
{
    public BoxCollider boxCollider;
    public GameObject treeManager;
    public float growTime = 15;
    public bool IsUsed;
    public TreeManager theTreeManagerScript;
    public GameManager gameManager;
    public int woodMult=1;

    public void Awake()
    {
        treeManager = transform.parent.gameObject;
        theTreeManagerScript = treeManager.GetComponent<TreeManager>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Chopped()
    {
        transform.position = new Vector3(0,0, -100f);
        IsUsed = false;
        int woodRandom = Random.Range(1, 4);
        Debug.Log(+woodRandom);
        gameManager.currentWood += (woodRandom * woodMult);  //calculates how much wood the tree is giving
        theTreeManagerScript.amountActive--;
        theTreeManagerScript.HowManyTrees();
    }

    public IEnumerator Grow()
    {
        IsUsed = true;
        boxCollider.enabled = false;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Vector3 initialScale = transform.localScale;
        Vector3 finalScale = initialScale * 1000f;
        float elapsed = 0f;
        while (elapsed < growTime)
        {
            transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsed / growTime);  //constantly grows the tree
            elapsed += Time.deltaTime;
            yield return null;
        }
        boxCollider.enabled = true;
        transform.localScale = finalScale;
    }
}


