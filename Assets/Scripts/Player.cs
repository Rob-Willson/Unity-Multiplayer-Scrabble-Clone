using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerScore))]
public class Player : NetworkBehaviour
{
    private PlayerScore PlayerScore;

    [Client]
    private void Update()
    {
        if(!hasAuthority)
        {
            return;
        }

        if(Input.anyKeyDown)
        {
            CmdDealTile(1);
        }
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize ()
    {
        PlayerScore = GetComponent<PlayerScore>();
        if(PlayerScore == null)
        {
            Debug.LogError("FAIL: '" + this.GetType().ToString() + "' could not find required component: 'PlayerScore'");
        }
    }

    [Command]
    private void CmdDealTile(int count)
    {

    }

    [Command]
    public void CmdAddToScore (int count)
    {
        // TODO: Validate...
        // ...

        PlayerScore.AddToScore(count);
    }

}
