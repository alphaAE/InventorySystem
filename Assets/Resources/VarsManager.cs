using UnityEngine;

[CreateAssetMenu]
public class VarsManager : ScriptableObject {
    public static VarsManager GetVarsManager() {
        return Resources.Load<VarsManager>("VarsManager");
    }

    public GameObject itemUIPre;
}