using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles switching between scenes
/// </summary>
public class ScenarioManager : MonoBehaviour {
    // ==================================================================================
    //                                  Scene Identification
    // ==================================================================================
    public enum Scene
    {
        MENU,
        GAME
    }
    [SerializeField] private const string _MENU_SCENE_NAME = "Scenes/Main Menu"; 
    [SerializeField] private const string _GAME_SCENE_NAME = "Scenes/Game"; 


    // ==================================================================================
    //                                  API to change scene
    // ==================================================================================
    /// <summary>
    /// Changes scene for all clients connected to host.
    /// </summary>
    /// <param name="newScene">New Scene to load.</param>
    public void SetServerScene(Scene newScene) {
        string newSceneName = GetSceneNameFromEnum(newScene);
        SetNewInputMapRelativeToScene(newScene);
        NetworkManager.Singleton.SceneManager.LoadScene(newSceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Changes scene for client only.
    /// </summary>
    /// <param name="newScene">New Scene to load.</param>
    public void SetClientScene(Scene newScene) {
        string newSceneName = GetSceneNameFromEnum(newScene);
        SetNewInputMapRelativeToScene(newScene);
        SceneManager.LoadScene(newSceneName);
    }

    /// <summary>
    /// Converts the Scene enum to Scene's actual name
    /// </summary>
    /// <param name="scene">Scene literal to convert to string name.</param>
    /// <returns></returns>
    private string GetSceneNameFromEnum(Scene scene) {
        switch (scene)
        {
            case Scene.MENU:
                return _MENU_SCENE_NAME;

            case Scene.GAME:
                return _GAME_SCENE_NAME;

            default:
                return "";
        }
    }

    [SerializeField] readonly private SObj_InputReader _inputReader;
    /// <summary>
    /// Adjust the currently set Input Map to the to the appropriate setting for each scene.
    /// </summary>
    /// <param name="newScene">Scene to set Input Map relative to.</param>
    private void SetNewInputMapRelativeToScene(Scene newScene){
        switch (newScene)
        {
            case Scene.MENU:
                _inputReader.SetInputMap(SObj_InputReader.InputMaps.NONE);
                break;

            case Scene.GAME:
                _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_GAME);
                break;
        }
    }
}