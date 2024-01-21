using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Tityx.WorldUI
{
    /// <summary>
    /// Source: https://gitlab.com/syhodyb99/tools-and-mechanics
    /// Отрисовка вьюшек и следование за таргетом
    /// </summary>
    public class WorldUIRenderer : MonoBehaviour
    {
        [field: SerializeField] public string Id { get; private set; }

        [Inject] protected WorldUIContainer _container;

        protected Camera _cam;

        protected virtual void Awake()
        {
            _cam = Camera.main;
            _container.AddRenderer(this);
        }

        protected virtual void LateUpdate()
        {
            _container.CheckDeleted();
            UpdatePositions();
        }

        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        protected virtual void UpdatePositions()
        {
            foreach (BaseWorldUIView view in _container.ViewList)
            {
                CheckParent(view);
                view.UpdateEnabled();
                view.UpdatePosition(_cam);
            }
        }

        protected virtual void CheckParent(BaseWorldUIView view)
        {
            if (view.transform.parent == null || (view.Target.RenderersId.Contains(Id) && view.transform.parent != transform))
                view.transform.SetParent(transform);
        }
    }
}