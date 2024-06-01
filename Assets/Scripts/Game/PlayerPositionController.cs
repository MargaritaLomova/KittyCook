using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerPositionController : MonoBehaviour
{
    [SerializeField]
    private PositionType type;
    [SerializeField]
    private UnityEvent afterReplaceEvent;

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

        afterReplaceEvent?.Invoke();
    }
}

public enum PositionType
{
    Knight = 0,
    Cook = 1,
    Client = 2,
    ChooseProducts = 3
}