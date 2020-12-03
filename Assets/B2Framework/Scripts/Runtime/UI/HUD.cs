using System.Collections.Generic;
using UnityEngine;

namespace B2Framework
{
    public class HUD : MonoBehaviour
    {
        private static HUD m_instance;
        public static HUD Instance
        {
            get { return m_instance; }
        }
        public Canvas m_Canvas;
        public GameObject hudPrefab;
        public GameObject textPrefab;
        public bool useCache = false;

        private Camera m_Camera = null;
        private Camera mCamera
        {
            get
            {
                if (m_Camera == null)
                {
                    m_Camera = (Camera.main != null) ? Camera.main : Camera.current;
                }

                return m_Camera;
            }
        }
        private Queue<HUDText> cachedTexts = new Queue<HUDText>();
        private Queue<HUDObject> cachedHuds = new Queue<HUDObject>();
        void Awake()
        {
            m_instance = this;
        }
        /// <summary>
        /// 显示抬头信息
        /// </summary>
        /// <param name="position"></param>
        public HUDObject ShowHUD(Vector3 position)
        {
            var hud = GetHUD();
            var pos = WorldToCanvasPoint(position);
            hud.transform.localPosition = pos;
            // hud.GetComponent<RectTransform>().anchoredPosition = pos;
            return hud;
        }
        /// <summary>
        /// 显示文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="stateName"></param>
        public HUDText ShowText(string text, Vector3 position, string stateName = null)
        {
            var item = m_instance.GetHudText();
            item.text = text;
            item.gameObject.SetActive(true);
            if (!string.IsNullOrEmpty(stateName)) item.Play(stateName);
            var pos = B2Framework.GameUtility.WorldToCanvasPoint(m_Canvas, position);
            item.transform.localPosition = pos;

            return item;
        }
        /// <summary>
        /// 获取HUD
        /// </summary>
        /// <returns></returns>
        public HUDObject GetHUD()
        {
            HUDObject hud = cachedHuds.Count > 0 ? cachedHuds.Dequeue() : null;
            if (hud == null)
            {
                var go = Instantiate(hudPrefab) as GameObject;
                go.name = hudPrefab.name;
                hud = go.GetComponent<HUDObject>();
                if (hud == null) hud = go.AddComponent<HUDObject>();
                go.transform.SetParent(m_Canvas.transform, false);
                go.SetActive(false);
            }
            return hud;
        }
        /// <summary>
        /// 获取HUDText
        /// </summary>
        /// <returns></returns>
        public HUDText GetHudText()
        {
            HUDText item = cachedTexts.Count > 0 ? cachedTexts.Dequeue() : null;
            if (item == null)
            {
                var go = Instantiate(textPrefab) as GameObject;
                go.name = textPrefab.name;
                item = go.GetComponent<HUDText>();
                go.transform.SetParent(m_Canvas.transform, false);
                go.SetActive(false);
            }
            return item;
        }
        /// <summary>
        /// 世界坐标转换成Canvas坐标
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector3 WorldToCanvasPoint(Vector3 position)
        {
            return B2Framework.GameUtility.WorldToCanvasPoint(m_Canvas, position);
        }
        public void Cache(HUDObject hud)
        {
            if (hud is HUDText)
                cachedTexts.Enqueue(hud as HUDText);
            else
                cachedHuds.Enqueue(hud);
        }
        /// <summary>
        /// 回收Hud
        /// </summary>
        /// <param name="hud"></param>
        public void Recycle(HUDObject hud)
        {
            if (useCache)
            {
                hud.gameObject.SetActive(false);
                Cache(hud);
            }
            else
            {
                GameObject.Destroy(hud.gameObject);
            }
        }
        /// <summary>
        /// 销毁所有
        /// </summary>
        public void Clear()
        {
            HUDObject hud;
            while (cachedHuds.Count > 0)
            {
                hud = cachedHuds.Dequeue();
                if (hud != null) GameObject.Destroy(hud);
            }
            HUDText text;
            while (cachedTexts.Count > 0)
            {
                text = cachedTexts.Dequeue();
                if (text != null) GameObject.Destroy(text.gameObject);
            }
            cachedHuds.Clear();
            cachedTexts.Clear();
        }
        void OnDestroy()
        {
            Clear();
            m_instance = null;
        }
    }
}