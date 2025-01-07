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
    [SerializeField] private GameObject _gameManagers;
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

    // ToDo: find better way to call `SetInputMap(SObj_InputReader.InputMaps.IN_GAME)` for clients that don't load the scene
    [SerializeField] private SObj_InputReader _inputReader;
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_GAME);
    }

    public void QuitGame() => Application.Quit();

    public void QuitToMenu() => _gameManagers.GetComponent<ScenarioManager>().SetClientScene(ScenarioManager.Scene.MENU);
}
