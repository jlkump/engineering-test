using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    // This is set by the Item Factory. ItemId uint.MaxValue is reserved for the "Empty" item type
    [HideInInspector]
    private uint m_itemId = int.MaxValue;
    public uint itemId
    {
        get
        {
            return m_itemId;
        }
        set
        {
            if (itemId == int.MaxValue && value != int.MaxValue) // Set is only allowed if the itemId has not been set yet
            {
                m_itemId = value;
            }
            else
            {
                Debug.LogError("Not allowed to change itemId after it has already been set.");
            }
        }
    }


    [SerializeField]
    string itemName;
    public uint maxStacks = uint.MaxValue;
    // 2D Display sprite for inventory UI
    public Sprite displaySprite;

    [SerializeField]
    Transform modelRoot;

    bool beingCollected = false, pickupDelayActive = false;

    ItemCollector lastCollector;

    void Start()
    {
        modelRoot.DORotate(new Vector3(0, 360, 0), 4, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        modelRoot.DOLocalMoveY(0.2f, 5).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemCollector collector = other.gameObject.GetComponent<ItemCollector>();
        if (pickupDelayActive)
        {
            PickupItem(collector);
        }
        else
        {
            lastCollector = collector;
        }

    }

    public void SetPickupDelay(float delay)
    {
        StartCoroutine(PickupDelay(delay));
    }

    IEnumerator PickupDelay(float delay)
    {
        pickupDelayActive = true;
        yield return new WaitForSeconds(delay);
        pickupDelayActive = false;
        if (lastCollector != null)
        {
            PickupItem(lastCollector);
        }
    }

    public void PickupItem(ItemCollector collector)
    {
        if (pickupDelayActive)
        {
            lastCollector = collector;
        }
        else if (collector != null && collector.CanCollectItem(itemId) && !beingCollected)
        {
            beingCollected = true;
            transform.DOMove(collector.GetCollectPosition(), 10).SetEase(Ease.InCubic).SetSpeedBased(true).OnComplete(
                () =>
                {
                    beingCollected = false;
                    if (collector.AttemptCollect(itemId))
                    {
                        transform.DOScale(0, 0.25f).SetEase(Ease.InBounce).OnComplete(
                            () =>
                            {
                                Destroy(gameObject);
                            }
                        );
                    }
                }
            );
        }
    }

    // For saving the item instance in the world for future games
    public void Save(System.IO.BinaryWriter writer)
    {

        writer.Write(transform.position.x);
        writer.Write(transform.position.y);
        writer.Write(transform.position.z);
        print("[Save] Saving items... Item Position (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")");
    }

    public void Load(System.IO.BinaryReader reader)
    {
        float xPos = reader.ReadSingle();
        float yPos = reader.ReadSingle();
        float zPos = reader.ReadSingle();
        print("[Load] Loading items... Item Save Position (" + xPos + ", " + yPos + ", " + zPos + ")");
        transform.position = new Vector3(xPos, yPos, zPos);
    }
}
