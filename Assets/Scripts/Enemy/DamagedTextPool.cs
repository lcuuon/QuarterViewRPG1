using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DamagedTextPool : MonoBehaviour
{
    public static DamagedTextPool instance;

    [SerializeField] private GameObject DamagedText;
    [SerializeField] private GameObject ExpText;

    Queue<DamageTxt> DamagedTextQueue = new Queue<DamageTxt>();
    Queue<GetExpText> ExpTextQueue = new Queue<GetExpText>();

    private void Awake()
    {
        instance = this;
    }

    //Damaged Pool -----------------------------------------------------------------------
    private DamageTxt CreateNewDamaged()
    {
        var newObj = Instantiate(DamagedText, this.transform).GetComponent<DamageTxt>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(this.transform);
        newObj.parentSet(this.transform);
        return newObj;
    }
    public static DamageTxt GetDamaged()
    {
        if (instance.DamagedTextQueue.Count > 0)
        {
            var obj = instance.DamagedTextQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreateNewDamaged();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    public static void ReturnDamaged(DamageTxt obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.DamagedTextQueue.Enqueue(obj);
    }
    //Damged Pool End --------------------------------------------------------------------

    //Exp Pool ---------------------------------------------------------------------------
    private GetExpText CreateNewExp()
    {
        var newObj = Instantiate(ExpText, this.transform).GetComponent<GetExpText>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(this.transform);
        newObj.parentSet(this.transform);
        return newObj;
    }
    public static GetExpText GetExp()
    {
        if (instance.ExpTextQueue.Count > 0)
        {
            var obj = instance.ExpTextQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreateNewExp();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    public static void ReturnExp(GetExpText obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.ExpTextQueue.Enqueue(obj);
    }
    //Exp Pool End -----------------------------------------------------------------------


}
