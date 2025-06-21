using Unity.AppUI.UI;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMPro.TMP_InputField xPosInput;
    public TMPro.TMP_InputField yPosInput;
    public GameObject playerPrefab;

    public void OnBtnClick()
    {
        GameObject player = Instantiate(playerPrefab);

        int xPos = int.Parse(xPosInput.text);
        int yPos = int.Parse(yPosInput.text);   

        Vector3 pos = GridManager.Instance.GridToWorldPos(new Vector2Int(xPos, yPos));

        ICharacterCore characterCore = player.GetComponent<ICharacterCore>();

        characterCore.DeployableComponent.Deploy(pos,Quaternion.identity);
    }
}
