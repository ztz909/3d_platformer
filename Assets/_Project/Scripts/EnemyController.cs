using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace boing
{
    public class EnemyController : MonoBehaviour
    {
        Transform target;
        [SerializeField] float speed;
        [SerializeField] float minRange;
        [SerializeField] float maxRange;
        // Start is called before the first frame update
        void Start()
        {
            target = FindObjectOfType<PlayerController>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, target.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                followPlayer();
            }
        }
        public void followPlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.LookAt(target.transform);
        }
    }
}
