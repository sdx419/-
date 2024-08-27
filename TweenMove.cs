using System;
using System.Collections;
using UnityEngine;

public class TweenMove : MonoBehaviour
{
    public bool m_pingpong;
    
    public enum MoveType
    {
        Linear,
        EasyIn,
        EasyOut,
        EasyInOut,
    }

    public MoveType moveType;
    
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
        float normalizeTime;
            
        while (true)
        {
            curMoveTime += Time.deltaTime;
            normalizeTime = curMoveTime / time;
            
            switch (moveType)
            {
                case MoveType.Linear:
                    lerpValue = normalizeTime;
                    break;
                case MoveType.EasyIn:
                    lerpValue = normalizeTime * normalizeTime; // 抛物线 y = t * t
                    break;
                case MoveType.EasyOut:
                    lerpValue = -normalizeTime * normalizeTime + 2 * normalizeTime; // 抛物线 y = - t * t + 2 * t
                    break;
                case MoveType.EasyInOut:
                    lerpValue = normalizeTime < 0.5
                        ? 2 * normalizeTime * normalizeTime   //分段函数前半部分， y = 2 * t * t
                        : -2 * normalizeTime * normalizeTime + 4 * normalizeTime - 1; // 分段函数后半部分，y = -2 * t * t + 4 * t - 1
                    break;
            }
            
            lerpValue = Mathf.Clamp01(lerpValue);
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
