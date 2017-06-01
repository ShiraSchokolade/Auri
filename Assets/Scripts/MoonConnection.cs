using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonConnection : MonoBehaviour {


  public GameObject startObject;
  public GameObject endObject;

  //line renderer variables
  private LineRenderer lineRenderer;
  private Vector3 startPosition;
  private Vector3 endPosition;
  private int segmentCount;       // number of line segments

  private Animator animator;
  private bool isConnected;
  private Vector3[] segments;   // holds the positions of all segments

	void Start () {

    lineRenderer = GetComponent<LineRenderer>();
    animator = GetComponent<Animator>();
    segmentCount = lineRenderer.positionCount;
    segments = new Vector3[segmentCount];

    lineRenderer.GetPositions(segments);
  }
	
	void Update () {

    // get the start and end coordinates
    startPosition = startObject.transform.position;
    endPosition = endObject.transform.position;

    // give the positions to the lineRenderer. in Update() for the case that the start and or endobject move
    lineRenderer.SetPosition(0, startPosition);
    lineRenderer.SetPosition(segmentCount-1, endPosition);

    for (int i = 0; i < segmentCount-1; i++)
    {
      lineRenderer.SetPosition(i, new Vector3(segments[i].x +endPosition.x, segments[i].y, segments[i].z));
      // nicht auf end.x setzen sondern addieren
    }

    
		
	}

}
