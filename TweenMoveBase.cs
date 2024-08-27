using System;
using System.Collections;
using UnityEngine;

public class TweenMoveBase : MonoBehaviour
{
    public bool m_pingpong;
    
    private Coroutine m_coroutine;

    public void testMove()
    {
        move(gameObject, Vector3.zero,  10 * Vector3.up, 3, m_pingpong);
    }

    public void testStop()
    {
        StopCoroutine(m_coroutine);
    }
    
    public void move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        m_coroutine = StartCoroutine(startMove(gameObject, begin, end, time, pingpong));
    }
        
    public IEnumerator startMove(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        float curMoveTime = 0;
        float lerpValue = 0;
            
        while (true)
        {
            curMoveTime += Time.deltaTime;
            
            lerpValue = Mathf.Clamp01(curMoveTime / time);
            gameObject.transform.position = Vector3.Lerp(begin, end, lerpValue);
                
            if (curMoveTime > time)
            {
                if (pingpong)
                {
                    (begin, end) = (end, begin); // swap
                    curMoveTime = 0;
                }
                else
                    break;
            }
                
            yield return 0;
        }
    }
}
