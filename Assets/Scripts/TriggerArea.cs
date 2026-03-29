using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public TreeScript theTreeScript;
    void OnTriggerEnter(Collider other)
    {
        theTreeScript = other.GetComponent<TreeScript>();
            theTreeScript.canbeused = true;
    }
}
