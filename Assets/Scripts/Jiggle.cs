
using UnityEngine;

public class Jiggle : MonoBehaviour
{
    // Start is called before the first frame update
    private float _startPos, _endPos, _pos;
    private float _startScale, _endScale;
    void Start()
    {
        _startPos = transform.position.y;
        _startScale = transform.localScale.y;

        _endPos = _startPos + 0.125f;
        _endScale = _endScale + 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        _pos = Mathf.Lerp(_startPos, _endPos, 1f * Time.deltaTime );
        Debug.Log( "Position: " + _pos );
    }
}
