using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Reloads current scene when user-chosen key is pressed
/// </summary>
public class Restart : MonoBehaviour
{
    public KeyCode RestartKey = KeyCode.F7;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(RestartKey))
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
        }
    }
}
