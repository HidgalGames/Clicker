using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    public Location CurrentLocation;
    private Entity currentEntity;

    void Start()
    {
        CurrentLocation.onEntitySpawn += SetCurrentEntity;
    }

    private void SetCurrentEntity(GameObject entity)
    {
        currentEntity = CurrentLocation.GetCurrentEntity().GetComponent<Entity>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentEntity)
            {
                currentEntity.DoOnTouchAction(Input.mousePosition);
            }
        }
    }
}
