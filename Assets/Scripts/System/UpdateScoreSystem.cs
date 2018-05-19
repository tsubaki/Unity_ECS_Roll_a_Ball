using Unity.Collections;
using Unity.Entities;
using UnityEngine.UI;

public sealed class UpdateScoreSystem : ComponentSystem
{
    struct LabelGroup
    {
        internal ComponentArray<Text> label;
    }

    struct ItemGroup
    {
        internal int Length;
        [ReadOnly] ComponentDataArray<ItemData> items;
    }

    [Inject] LabelGroup group;
    [Inject] ItemGroup itemGroup;

    int preItemCount = 0;

    protected override void OnUpdate()
    {
        if(preItemCount != itemGroup.Length)
        {
            group.label[0].text = string.Format("{0:00}", itemGroup.Length);
            preItemCount = itemGroup.Length;
        }
    }
}