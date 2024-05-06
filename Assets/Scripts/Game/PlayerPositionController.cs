using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionController : MonoBehaviour
{
    [SerializeField]
    private PositionType type;

    private Button button;
    private PlayerController player;

    private void Start()
    {
        button = GetComponent<Button>();
        player = FindObjectOfType<PlayerController>();

        button.onClick.AddListener(ReplacePlayerToCurrentPosition);
    }

    private void ReplacePlayerToCurrentPosition()
    {
        player.transform.position = transform.position;

        player.Flip(type == PositionType.Cook);

        Debug.Log("Replace");
    }
}

public enum PositionType
{
    Knight = 0,
    Cook = 1,
    Client = 2
}