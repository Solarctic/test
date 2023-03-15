using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelEnd : MonoBehaviour
{
    [Space]

    [Tooltip("This is the next scene that will be loaded when the player touches this object.")]
    public UnityEditor.SceneAsset nextScene;

    [Tooltip("If this is enabled, then the level end collider will be invisible while playing.")]
    public bool makeInvisibleOnStart = true;

    private void Start() {
        // Set whether the renderer should be enabled for this object
        GetComponent<Renderer>().enabled = !makeInvisibleOnStart;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // If an object collides, check if it has the tag player and load the scene if it is
        if (collision.transform.CompareTag("Player")) {
            SceneManager.LoadScene(nextScene.name);
        }
    }
}
