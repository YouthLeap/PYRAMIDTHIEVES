using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PyramidDoor : MonoBehaviour
{
    private bool isActive = false;
    [SerializeField]
    private int activeValue = 0;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject border;

    [SerializeField]
    private GameObject roomPrefab;
    private RoomController roomController;

    void Start()
    {
        isActive = activeValue <= Attributes.highest_level && activeValue != LevelManager.unlockLevel;
        //isActive = true;
        if (isActive)
        {
            door.SetActive(false);
            border.SetActive(false);
        }
        if (activeValue == LevelManager.unlockLevel)
        {
            LevelManager.unlockLevel = -1;
            MapSelector.instance.UnlockLevel(this, activeValue);
        }
    }

    public void Open()
    {
        if (MapSelector.instance.isUnlocking || !isActive) return;
        if (roomPrefab != null && roomController == null)
        {
            GameObject room = Instantiate(roomPrefab) as GameObject;
            roomController = room.GetComponent<RoomController>();
        }
        if (roomController != null)
        {
            roomController.Open();
            return;
        }
    }

    public void StartFirstLevel()
    {
        if (!MapSelector.instance.isUnlocking && !isActive) return;
        door.SetActive(false);
        border.SetActive(false);
        LevelManager.PlayLevel(activeValue);
    }

    public void Unlock()
    {
        GetComponent<Animator>().enabled = true;
    }

    public void PlaySFX()
    {
        MapSelector.instance.PlayDoorSFX();
    }

    public void StopSFX()
    {
        MapSelector.instance.StopDoorSFX();
    }
}
