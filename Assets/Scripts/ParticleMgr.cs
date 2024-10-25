using UnityEngine;

public class ParticleMgr : MonoBehaviour
{
    private static ParticleMgr m_Instance;
    public static ParticleMgr Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = FindObjectOfType<ParticleMgr>();
            return m_Instance;
        }
    }

    public enum EffectType
    {
        Click,
        BtnClick
    }

    public ParticleSystem touchClick;
    public ParticleSystem clickBtn;

    public void PlayHitEffect(Vector3 pos, Vector3 normal, Transform parent = null, EffectType effectType = EffectType.Click)
    {
        var targetPrefab = touchClick;

        if (effectType == EffectType.BtnClick)
        {
            targetPrefab = clickBtn;
        }

        var effect = Instantiate(targetPrefab, pos, Quaternion.LookRotation(normal));

        if (parent != null) effect.transform.SetParent(parent);

        effect.Play();
    }
}
