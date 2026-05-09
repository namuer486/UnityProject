using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            EventCenter.Instance.Add(this, "PlayerManagerInit", Init);
        }
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    public Player player;
    public PlayerAttack attack;
    public PlayerMove move;
    public PlayerRotation rotation;
    public Transform BirthPos;
    public void Init()
    {
        //attack = GetComponent<PlayerAttack>();
        //move = GetComponent<PlayerMove>();
        //rotation = GetComponent<PlayerRotation>();
        EventCenter.Instance.Add(this, "PlayerDie", PlayerDie);
        EventCenter.Instance.Add(this, "PlayerBirth", PlayerBirth);

    }
    private void PlayerDie()
    {

    }
    private void PlayerBirth()
    {
        player.Init();
        //attack.Init();
        //move.Init();
        //rotation.Init();
        EventCenter.Instance.OnTriggerEven("LoadPlayerPos", player.transform);
    }
}
