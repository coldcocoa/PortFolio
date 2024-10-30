using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class wealthMgr : MonoBehaviour
{
    [SerializeField] private float gold;
    [SerializeField] private float crystal;
    public TextMeshProUGUI Cry;
    public TextMeshProUGUI Gold;
    // Start is called before the first frame update
    void Start()
    {
        gold = 0;
        crystal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CryStal();
        GOLD();
    }

    public void CryStal()
    {
        Cry.text = crystal.ToString();
    }
    public void GOLD()
    {
        Gold.text = gold.ToString();
    }
    public void crystal1000()
    {
        crystal += 1000;
    }
    public void crystal2000()
    {
        crystal += 2000;
    }
    public void crystal3000()
    {
        crystal += 3000;
    }
    public void crystal4000()
    {
        crystal += 4000;
    }
    public void crystal5000()
    {
        crystal += 5000;
    }
    public void crystal6000()
    {
        crystal += 6000;
    }
    public void Gold10000()
    {
        gold += 10000;
    }
    public void Gold20000()
    {
        gold += 20000;
    }
    public void Gold30000()
    {
        gold += 30000;
    }
    public void Gold40000()
    {
        gold += 40000;
    }
    public void Gold50000()
    {
        gold += 50000;
    }
    public void Gold60000()
    {
        gold += 60000;
    }
}
