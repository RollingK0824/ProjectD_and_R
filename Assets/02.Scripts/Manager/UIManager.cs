using Unity.AppUI.UI;
using UnityEngine;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    public TMPro.TMP_InputField xPosInput;
    public TMPro.TMP_InputField yPosInput;
    public GameObject playerPrefab;

    public List<ICharacterCore> players;

    protected void Awake()
    {
        base.Awake();
        players = new List<ICharacterCore>();
    }

    public void OnSpawnBtnClick()
    {
        GameObject player = Instantiate(playerPrefab);

        int xPos = int.Parse(xPosInput.text);
        int yPos = int.Parse(yPosInput.text);   

        Vector3 pos = GridManager.Instance.GridToWorldPos(new Vector2Int(xPos, yPos));

        ICharacterCore characterCore = player.GetComponent<ICharacterCore>();
        
        characterCore.DeployableComponent.Deploy(pos,Quaternion.identity);

        players.Add(characterCore);
    }

    public void OnAttackBtnClick()
    {
        foreach(ICharacterCore characterCore in players)
        {
            characterCore.AttackerComponent.TryAttack();
        }
    }
}
