using UnityEditor;
using UnityEngine;

public class ResetMySlot : MonoBehaviour
{
    private SlotController slotController;

    private void Start()
    {
        slotController = GetComponent<SlotController>();
    }

    public void ResetSlot()
    {
        slotController.slotGame.costSlot = 80;
        slotController.slotGame.slotLevel = 1;
        slotController.slotGame.production = 1;
        slotController.slotGame.multiplier = 1;
        slotController.slotGame.reducerTimeProduction = 1;
        slotController.slotGame.isPurchased = true;
        slotController.slotGame.totalEvolutions = 0;
        slotController.slotGame.evolutions = 0;
        slotController.slotGame.isAutoProduction = false;
        slotController.slotGame.isMaxLevel = false;
        slotController.slotGame.InitializeSlotGame();

        Debug.Log("RESET");
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(ResetMySlot))]
public class ResetSlotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(10f);
        ResetMySlot controller = (ResetMySlot)target;
        if (GUILayout.Button("Reset My Slot"))
        {
            controller.ResetSlot();
        }
    }
}
#endif

