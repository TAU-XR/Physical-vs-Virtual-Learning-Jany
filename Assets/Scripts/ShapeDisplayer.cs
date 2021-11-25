using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// display shapes evenly speard along surface.
public class ShapeDisplayer : MonoBehaviour
{
    [SerializeField] float shapeGap = 0.15f;            // how far should display positions be from each other
    float shapeZ = 0.01f;                               // local scale on Z value of shape GO

    // those save the GO of shapes. Original GO are those on the Table in the scene.
    [SerializeField] GameObject rectangleShape;
    [SerializeField] GameObject circleShape;
    [SerializeField] GameObject triangleShape;
    [SerializeField] GameObject starShape;


    Transform[] displayPositions;                       // all the positions on which shapes are going to be displayed.
    GameObject[] displayedShapes;                       // the GO references of displayed shapes.

    // display shapes evenly speard on surface (also creates displayPositions for each)
    public void DisplayShapes(Shape[] shapes)
    {
        cleanCurrentShapes();
        displayedShapes = new GameObject[shapes.Length];
        SetDisplayPositions(shapes.Length);
        for (int i = 0; i < shapes.Length; i++)
        {
            displayedShapes[i] = spawnShape(shapes[i], displayPositions[i].position, displayPositions[i].rotation);
            displayedShapes[i].GetComponent<MeshRenderer>().enabled = true;
        }
        SetShapesVisibility(true);
    }

    // sets the display positions according to the sequence length.
    public void SetDisplayPositions(int seqLength)
    {
        cleanCurrentDisplayers();
        displayPositions = new Transform[seqLength];
        float totalSeqLength = seqLength * shapeZ * 10f + (seqLength - 1) * (shapeGap - shapeZ * 10f);

        // place first shape to be first from the left
        float firstPos = (totalSeqLength / 2f) - shapeZ * 10f / 2f;

        //place the rest of the displayers to its right
        for (int i = 0; i < seqLength; i++)
        {
            spawnNewDisplayer(i);
            displayPositions[i].localPosition = new Vector3(-0.01f, 0, firstPos - i * shapeGap);
        }
    }

    public void DeleteShape(int i)
    {
        Destroy(displayedShapes[i]);
    }

    public void SubmitShape(Shape shape, int i)
    {
        displayedShapes[i] = spawnShape(shape, displayPositions[i].position, displayPositions[i].rotation);
    }

    public void ResetShapesArray(int seqLength)
    {
        cleanCurrentShapes();
        displayedShapes = new GameObject[seqLength];
    }

    public void SetShapesVisibility(bool visible)
    {
        foreach (GameObject go in displayedShapes)
        {
            if(go!=null)
                if(go.TryGetComponent<MeshRenderer>(out MeshRenderer mr))
                    mr.enabled = visible;
        }
    }

    // gets a Shape and spawns a new game object with that shape and return it
    GameObject spawnShape(Shape shape, Vector3 pos, Quaternion rot)
    {
        GameObject go;

        switch (shape)
        {
            case Shape.rectange:
                go = rectangleShape;
                break;
            case Shape.circle:
                go = circleShape;
                break;
            case Shape.triangle:
                go = triangleShape;
                break;
            case Shape.star:
                go = starShape;
                break;
            default:
                go = null;
                break;
        }
        return Instantiate(go, pos, rot);
    }

    void spawnNewDisplayer(int i)
    {
        displayPositions[i] = new GameObject("displayPosition " + i).transform;
        displayPositions[i].parent = transform;
        displayPositions[i].localPosition = Vector3.zero;
        displayPositions[i].localEulerAngles = new Vector3(0, 0, 90);
    }

    // removes all currently displayed shapes
    void cleanCurrentShapes()
    {
        if (displayedShapes == null)
            return;
        foreach (GameObject g in displayedShapes)
            Destroy(g);
    }

    void cleanCurrentDisplayers()
    {
        if (displayPositions != null)
            foreach (Transform t in displayPositions)
                Destroy(t.gameObject);
    }





}
