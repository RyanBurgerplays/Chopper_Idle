using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeManager : MonoBehaviour
{
    public int numberofTrees;
    public GameObject[] treeCollections;
    public GameObject theTree;
    public TreeScript theTreescript;
    private Vector3 treeLocation;
    public int amountActive = 0;
    public int RareChanceUpgrade;
    public void Start()
    {
        RareChanceUpgrade = 0;
        numberofTrees = 4;
        HowManyTrees();
    }

    public void HowManyTrees()
    {
        for (amountActive = 0; amountActive < numberofTrees; amountActive++)    //makes sure the max amount of trees is equal to the amount of trees that can spawn
        {
            
            float randomX = Random.Range(-8f, 2.6f);
            float randomZ = Random.Range(-11f, 2.6f);
            int randomNumber = Random.Range(0, 100);

            theTree = treeCollections[amountActive];
            theTreescript = theTree.GetComponent<TreeScript>();
            if (theTreescript.IsUsed == false)
            {
                theTree.SetActive(true);
                if(RareChanceUpgrade >= randomNumber) { theTreescript.IsRare = true; }
                theTree.transform.position = new Vector3(randomX, 0f, randomZ);
                theTreescript.StartCoroutine(theTreescript.Grow());
                amountActive++;

            }
            
        }
    }
   

}
