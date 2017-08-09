using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonConnection : MonoBehaviour
{
  public GameObject startObject;
  public GameObject endObject;
  public float yDistanceFromPlayer = 0.6f;

  //line renderer variables
  private LineRenderer lineRenderer;
  private Vector3 startPosition;  // moon
  private Vector3 endPosition;    // character
  private int segmentCount;       // number of line segments

  private Animator animator;
  private bool isConnected;
  private Vector3[] segments;   // holds the positions of all segments

  private float segmentMoveAmount = 1f;
  private float playerMoveAmount = 0f;
  private float lastXPosition = 0f;

  void Start()
  {

    lineRenderer = GetComponent<LineRenderer>();
    animator = GetComponent<Animator>();

    segmentCount = lineRenderer.positionCount;
    segments = new Vector3[segmentCount];

    lineRenderer.GetPositions(segments);

    if (startObject == null || endObject == null)
      Debug.Log("Please add a GameObject for the start and end position of the line");
    else
    {
      // get start position - fixed position, give position to line Renderer
      startPosition = startObject.transform.position;
      lineRenderer.SetPosition(0, startPosition);

      lastXPosition = endPosition.x;
    }
  }

  void Update()
  {

    // get the end coordinates - variable position -> Update
    endPosition = endObject.transform.position;

    playerMoveAmount = endPosition.x - lastXPosition;
    
    if(playerMoveAmount != 0) // character moved
    {

      // give the position to the lineRenderer. in Update() because the endobject moves
      lineRenderer.SetPosition(segmentCount - 1, new Vector3(endPosition.x, endPosition.y + yDistanceFromPlayer, endPosition.z));  //segmentCount-1: segment, that is attached to the player ( 0 is the moon )

      // move all segments when endobject moves
      for (int i = 1; i < segmentCount - 1; i++)
      {
        lineRenderer.SetPosition(i, new Vector3(segments[i].x + playerMoveAmount, segments[i].y, segments[i].z));
        //print("Nr: " + i + " ,Pos.: " + segments[i]);
      }

      //lastXPosition = endPosition.x;
    }

    ////segments should always move a bit
    //for (int i = 0; i < segmentCount - 1; i++)
    //{
    //  lineRenderer.SetPosition(i, new Vector3(segments[i].x + segmentMoveAmount, segments[i].y, segments[i].z));
    //  // nicht auf end.x setzen sondern addieren
    //}

  }

}
