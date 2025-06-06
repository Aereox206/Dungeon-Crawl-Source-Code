using UnityEngine;

public class LadderMove : MonoBehaviour
{
    public GameObject ladder;
    public Lever lever;
    public Vector3 targetPosition;

    private void Update()
    {
        if (lever.on)
        {
            ladder.transform.position = Vector3.MoveTowards(ladder.transform.position, targetPosition, 2f * Time.deltaTime);
        }
    }
}
