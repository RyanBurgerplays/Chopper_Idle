using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeManager : MonoBehaviour
{
    public int numberofTrees= 4;
    public GameObject[] treeCollections;
    public GameObject theTree;
    public TreeScript theTreescript;
    private Vector3 treeLocation;
    public int amountActive = 0;
    private void Start()
    {
        HowManyTrees();
    }

    public void HowManyTrees()
    {
        for (amountActive = 0; amountActive < numberofTrees; amountActive++)    //makes sure the max amount of trees is equal to the amount of trees that can spawn
        {
            float randomX = Random.Range(-8f, 2.6f);
            float randomZ = Random.Range(-11f, 2.6f);
            int randomNumber = Random.Range(0, 5);
            theTree = treeCollections[amountActive];
            theTreescript = theTree.GetComponent<TreeScript>();
            if (theTreescript.IsUsed == false)
            {
                //Debug.Log("BLAH BLAH BLAH");
                theTree.SetActive(true);

                theTree.transform.position = new Vector3(randomX, 0f, randomZ);
                theTreescript.StartCoroutine(theTreescript.Grow());
                amountActive++;

            }
            //else()
        }
    }
   

}
