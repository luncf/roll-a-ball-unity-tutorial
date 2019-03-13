using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    public int minNumObjects;
    public int maxNumObjects;
    public Transform pickupPrefab;

    public GameObject ground;
    public float wallOffset;

    public Text remainingPickupText;

    private int numObjects;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rng = new System.Random();

        numObjects = rng.Next(minNumObjects, maxNumObjects);
        Vector3 bounds = ground.GetComponent<Collider>().bounds.size;
        bounds[0] -= ((2 * wallOffset) - 2);
        bounds[2] -= ((2 * wallOffset) - 2);

        for (int count = 0; count < numObjects; count++)
        {
            float x = (float) (bounds[0] * rng.NextDouble() - bounds[0]/2);
            float z = (float)(bounds[2] * rng.NextDouble() - bounds[2]/2);
            Transform child = Instantiate(pickupPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
            child.SetParent(this.gameObject.transform);
        }

        UpdateUIText();
    }

    public void UpdateUIText()
    {
        remainingPickupText.text = "Remaining objects: " + numObjects;
    }

    public void DestroyObject()
    {
        numObjects--;
        UpdateUIText();
    }

    public bool HasRemainingObject()
    {
        return (numObjects > 0);
    }
}
