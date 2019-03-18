using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubiksCubeController : MonoBehaviour
{
    public int minNumObjects;
    public int maxNumObjects;
    public Transform rubiksCubePrefab;

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
        bounds[0] -= ((2 * wallOffset) + 2);
        bounds[2] -= ((2 * wallOffset) + 2);

        Quaternion rotation = new Quaternion(0.3f, -0.1f, 0.3f, 0.9f);

        ArrayList cubePositions = new ArrayList();

        for (int count = 0; count < numObjects; count++)
        {
            int x = rng.Next((int) -bounds[0]/2, (int) bounds[0]/2);
            int z = rng.Next((int) -bounds[2]/2, (int) bounds[2]/2);
            Vector3 position = new Vector3((float) x, 0.8f, (float) z);

            if (!cubePositions.Contains(position))
            {
                Transform child = Instantiate(rubiksCubePrefab, position, rotation);
                child.SetParent(this.gameObject.transform);
            }
            else
            {
                count--;
            }
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
