
using UnityEngine;

public class Jiggle : MonoBehaviour
{
    [Range(0f, 4f)] public float multiplier = 1f;
    [Range(0f, 10f)] public float lerpTime;
    private float _posDiff = 0.125f, _scaleDiff = 0.25f;
    private Vector3 _startPos, _endPos, _pos;
    private Vector3 _startScale, _endScale;

    private float t = 0f;
    private bool flag = true;
    void Start()
    {
        _startPos = transform.position;
        _startScale = transform.localScale;

        float posY =  _posDiff * multiplier;
        float scaleY =  _scaleDiff * multiplier;

        _endPos = new Vector3(_startPos.x, _startPos.y + posY, _startPos.z);
        _endScale = new Vector3(_startScale.x, _startScale.y + scaleY, _startScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            transform.position = Vector3.Lerp(_startPos, _endPos , lerpTime);
            transform.localScale = Vector3.Lerp(_startScale, _endScale, lerpTime);
        }
        else
        {
            transform.position = Vector3.Lerp(_endPos, _startPos , lerpTime);
            transform.localScale = Vector3.Lerp(_endScale, _startScale, lerpTime);
        }        
        
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);

        if(t > .9f)
        {
            t = 0f;
            flag = !flag;
        }
    }

   
}
