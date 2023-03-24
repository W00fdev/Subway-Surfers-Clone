using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subway
{
    public class FloatOverflow : MonoBehaviour
    {
        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            transform.position = new Vector3(0f, 0f, Mathf.Pow(2, 40));
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
