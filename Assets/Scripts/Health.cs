using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public interface IDamageListener
    {
        public void AfterDamageTaken();
    }

    [SerializeField] int maxHealth = 3;
    [SerializeField] public int currentHealth;
    [SerializeField] int regenPerSec = 0;

    [SerializeField] GameObject hurtSFX;

    private void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(Regeneration());
    }

    IEnumerator Regeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentHealth = (currentHealth + regenPerSec) % (maxHealth + 1);
        }
    }

    public bool TakeDamage(int damage)
    {
        if (hurtSFX) 
            Destroy(Instantiate(hurtSFX, gameObject.transform.position, Quaternion.identity), 3f);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroyer.Destroy(gameObject);
            return true;
        }
        var listener = GetComponent<IDamageListener>();
        if (listener != null) listener.AfterDamageTaken();

        gameObject.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo);
        //foreach (Material mat in GetComponentsInChildren<Material>())
        //{
        //    Shader shader = mat.shader;
        //    Debug.Log(shader.GetPropertyDefaultVectorValue(shader.FindPropertyIndex("Dark Color")));
        //    //mat.DOColor(Color.red, 0.3f).SetLoops(2, LoopType.Yoyo);
        //}

        return false;
    }

}
