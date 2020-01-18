using UnityEngine;
using UnityEngine.AI;

public class HideAgent : MonoBehaviour
{
    private CapsuleCollider _collider;
    private SkinnedMeshRenderer[] _sRenderers;
    private MeshRenderer[] _mRenderers;
    private NavMeshAgent _nav;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _sRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        _mRenderers = GetComponentsInChildren<MeshRenderer>(true);
        _nav = GetComponent<NavMeshAgent>();
    }

    public void SetAgentVisibility(bool visible)
    {
        _collider.enabled = visible;
        //_nav.enabled = visible;   // TODO: right now this makes Navigator throw an error in Stop()

        foreach (var r in _sRenderers)
        {
            r.enabled = visible;
        }
        foreach (var r in _mRenderers)
        {
            r.enabled = visible;
        }
    }
}
