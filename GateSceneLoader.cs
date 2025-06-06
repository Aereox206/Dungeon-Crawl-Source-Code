using UnityEngine;
using UnityEngine.SceneManagement;

public class GateSceneLoader : MonoBehaviour
{
    public FadeController fadeController;
    private bool triggered = false;

    private void Update()
    {
        if (fadeController != null && fadeController.PlayersEnteredGate() && !triggered)
        {
            triggered = true;

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;

            if (currentSceneIndex + 1 < totalScenes)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }
    }
}
