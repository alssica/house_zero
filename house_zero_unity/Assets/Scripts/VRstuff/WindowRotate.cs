using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Menu
{
    // This class 'pops' each of the menu items out
    // when the user looks at them.
    public class WindowRotate : MonoBehaviour
    {
        [SerializeField] private VRInteractiveItem m_Item;      // The VRInteractiveItem of whatever should pop out.
        [SerializeField] private float openHeight = 1.5f;         // The speed at which the item should pop out.
        [SerializeField] private float m_PopDistance = 0.5f;    // The distance the item should pop out.

        private bool windowOpen;
        public float openWidth = 1.5f;
        Vector3 originalPosition;

        void Start()
        {
            windowOpen = false;
        }

        void OnSelect()
        {
            if (!this.GetComponent<Rigidbody>())
            {
                var rigidbody = this.gameObject.AddComponent<Rigidbody>();
                rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
        }

        void OnOpen()
        {
            var rigidbody = this.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.transform.Translate(0, openWidth, 0);
            }
            this.transform.localPosition = originalPosition;
        }

        void OnClose()
        {
            var rigidbody = this.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.transform.Translate(0, -openWidth, 0);
            }
            this.transform.localPosition = originalPosition;
        }

        /*
        void OnShow()
        {
            //var rigidbody = this.GetComponent<Rigidbody>();
            var UI = GameObject.Find("UI");
            UI.SetActive(true);
        }

        void OnHide()
        {
            //var rigidbody = this.GetComponent<Rigidbody>();
            var UI = GameObject.Find("UI");
            UI.SetActive(false);
        }
        */

        /*
         void Update()
        {

            if (m_Item.IsOver == true && windowOpen == false)
            {
                //m_Item.transform.RotateAround(m_Item.transform.position, m_Item.transform.right,Time.deltaTime*30f);
                m_Item.transform.Translate(0, openHeight, 0, m_Item.transform);
                    //(m_Item.transform.position, m_Item.transform.right, Time.deltaTime * 30f);
                windowOpen = true;
            }
            /*
            else if (m_Item.IsOver == true && Input.GetMouseButtonDown(0))
            {
                m_Item.transform.Translate(0, -openHeight, 0, m_Item.transform);
                windowOpen = false;
            }*/
        //}

    }
}