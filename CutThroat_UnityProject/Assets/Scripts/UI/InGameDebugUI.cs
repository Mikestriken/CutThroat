using TMPro;
using UnityEngine;

public class InGameDebugUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentMapTextbox;
    [SerializeField] private SObj_InputReader inputReader;

    // Update is called once per frame
    void Update()
    {
        _currentMapTextbox.text = inputReader.currentMap.ToString();
    }

    [ContextMenu("MAP/UI")]
    void SetUIMap() => inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_UI);
    [ContextMenu("MAP/InGame")]
    void SetInGameMap() => inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_GAME);
    [ContextMenu("MAP/NONE")]
    void SetNONEMap() => inputReader.SetInputMap(SObj_InputReader.InputMaps.NONE);
}
