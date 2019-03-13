using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance = null;

    [SerializeField]
    private GameObject Float_Parent;

    [SerializeField]
    private List<Powerups> _powerUps = new List<Powerups>();

    [SerializeField]
    private List<Debuffs> _debuffs = new List<Debuffs>();

    public List<Powerups> GetPowerUps()
    {
        return _powerUps;
    }

    public List<Debuffs> GetDebuffs()
    {
        return _debuffs;
    }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public float DropRate(float Rate)
    {
        if (Random.value * 100 <= Rate)
        {
            for (int i = 0; i < _powerUps.Count; i++)
            {
                if (Random.value * 100 <= _powerUps[i].GetPercent)
                {
                    Instantiate(_powerUps[i]);
                }
            }
        }

        return Rate;
    }

    public IEnumerator IndestructibleArmorCount()
    {
        yield return new WaitForSeconds(5.0f);
        Character.Instance.GetIndestructibleArmor = false;
    }

    public IEnumerator CanDropBombCount()
    {
        yield return new WaitForSeconds(4.0f);
        Character.Instance.GetCanDropBmb = true;
    }
}
