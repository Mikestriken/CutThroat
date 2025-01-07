using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Mega script to handle all UI button logic and data
/// </summary>
// ToDo: Rename to MainMenu script
[RequireComponent(typeof(SceneManager))]
public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] readonly private GameObject _gameManagers;
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        _gameManagers.GetComponent<ScenarioManager>().SetServerScene(ScenarioManager.Scene.GAME);
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        _gameManagers.GetComponent<ScenarioManager>().SetServerScene(ScenarioManager.Scene.GAME);
    }

    // ToDo: find better way to set key mapping for clients that don't load the scene
    [SerializeField] readonly private SObj_InputReader _inputReader;
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_GAME);
    }

    public void QuitGame() => Application.Quit();
}
