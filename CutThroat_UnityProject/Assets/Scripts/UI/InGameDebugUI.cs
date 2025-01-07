using TMPro;
using UnityEngine;

public class InGameDebugUI : MonoBehaviour
{
    [SerializeField] readonly private TMP_Text _currentMapTextbox;
    [SerializeField] readonly private SObj_InputReader _inputReader;

    // Update is called once per frame
    void Update()
    {
        _currentMapTextbox.text = _inputReader.currentMap.ToString();
    }

    [ContextMenu("MAP/UI")]
    void SetUIMap() => _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_UI);
    [ContextMenu("MAP/InGame")]
    void SetInGameMap() => _inputReader.SetInputMap(SObj_InputReader.InputMaps.IN_GAME);
    [ContextMenu("MAP/NONE")]
    void SetNONEMap() => _inputReader.SetInputMap(SObj_InputReader.InputMaps.NONE);
}
