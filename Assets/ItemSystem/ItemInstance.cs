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

    bool beingCollected;

    void Start()
    {
        beingCollected = false;
        modelRoot.DORotate(new Vector3(0, 360, 0), 4, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        modelRoot.DOLocalMoveY(0.2f, 5).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemCollector collector = other.gameObject.GetComponent<ItemCollector>();
        if (collector != null && collector.CanCollectItem(itemId) && !beingCollected)
        {
            beingCollected = true;
            transform.DOMove(collector.GetCollectPosition(), 10).SetEase(Ease.InCubic).SetSpeedBased(true).OnComplete(
                () =>
                {
                    if (collector.AttemptCollect(itemId))
                    {
                        transform.DOScale(0, 0.25f).SetEase(Ease.InBounce).OnComplete(
                            () =>
                            {
                                Destroy(gameObject);
                            }
                        );
                    }
                    else
                    {
                        beingCollected = false;
                    }
                }
            );
        }
    }

    // For saving the item instance in the world for future games
    //void Save(DataWriter writer)
    //{

    //}

    //void Load(DataReader reader)
    //{

    //}
}
