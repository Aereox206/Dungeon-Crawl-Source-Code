using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    public Camera ghostArea1Cam;
    public Camera ghostArea2Cam;
    public Camera mainCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ghostArea1Cam.enabled = false;
        ghostArea2Cam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GhostArea1") {
            mainCam.rect = new Rect(0.4f, 0.4f, 0.6f, 0.6f);
            ghostArea1Cam.rect = new Rect(0f, 0f, 0.4f, 0.4f);
            ghostArea1Cam.enabled = true;
        }
        else if (collision.tag == "GhostArea2")
        {
            mainCam.rect = new Rect(0f, 0.4f, 0.6f, 0.6f);
            ghostArea2Cam.rect = new Rect(0.6f, 0f, 0.4f, 0.4f);
            ghostArea2Cam.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GhostArea1")
        {
            mainCam.rect = new Rect(0f, 0f, 1f, 1f);
            ghostArea1Cam.enabled = false;
        }
        else if (collision.tag == "GhostArea2")
        {
            mainCam.rect = new Rect(0f, 0f, 1f, 1f);
            ghostArea2Cam.enabled = false;
        }
    }
}
