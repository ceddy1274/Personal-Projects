using UnityEngine;
using System.Collections;

public class RatController : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private bool isMoving = true;
    private int x;
    private int y;

    void Start()
    {
        startPosition = GraphToWorld(Vector3.zero);
        transform.position = startPosition;
        MoveToRandomPosition();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
                StartCoroutine(WaitForInput());
            }
        }
    }

    private void MoveToRandomPosition()
    {
        // Get a random number for x and y
        x = UnityEngine.Random.Range(0, 11);
        y = UnityEngine.Random.Range(0, 11);

        // Convert graph coordinates to world coordinates
        targetPosition = GraphToWorld(new Vector2(x, y));

    }

    private Vector3 GraphToWorld(Vector3 graphPosition)
    {
        float graphMinX = 0f, graphMaxX = 10f;
        float graphMinY = 0f, graphMaxY = 10f;
        float worldMinX = -5.9f, worldMaxX = 7f;
        float worldMinY = -4f, worldMaxY = 4.9f;

        float scaleX = (worldMaxX - worldMinX) / (graphMaxX - graphMinX);
        float scaleY = (worldMaxY - worldMinY) / (graphMaxY - graphMinY);

        return new Vector3(
            worldMinX + (graphPosition.x - graphMinX) * scaleX,
            worldMinY + (graphPosition.y - graphMinY) * scaleY,
            -1
        );
    }

    private Vector3 WorldToGraph(Vector3 worldPosition)
    {
        float graphMinX = 0f, graphMaxX = 10f;
        float graphMinY = 0f, graphMaxY = 10f;
        float worldMinX = -5.9f, worldMaxX = 7f;
        float worldMinY = -4f, worldMaxY = 4.9f;

        float scaleX = (graphMaxX - graphMinX) / (worldMaxX - worldMinX);
        float scaleY = (graphMaxY - graphMinY) / (worldMaxY - worldMinY);

        return new Vector3(
            graphMinX + (worldPosition.x - worldMinX) * scaleX,
            graphMinY + (worldPosition.y - worldMinY) * scaleY,
            -1
        );
    }

    public float GetMagnitude()
    {
        Vector3 graphPosition = WorldToGraph(targetPosition);
        return Mathf.Round(Mathf.Sqrt(
            graphPosition.x * graphPosition.x +
            graphPosition.y * graphPosition.y
        ));
    }

    public Vector3 GetGraphPosition()
    {
        return new Vector3(x, y, -1);
    }
    private IEnumerator WaitForInput()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.RatStopped();
    }

    public void ResetRat()
    {
        transform.position = startPosition;
        MoveToRandomPosition();
        isMoving = true;
    }
}
