using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tityx.WorldUI
{
    /// <summary>
    /// Вью для чего-либо (хп бар/таймер над объектом и т.д.)
    /// </summary>
    public class BaseWorldUIView : MonoBehaviour
    {
        public BaseWorldUITarget Target => _target;

        private BaseWorldUITarget _target;

        public void Init(BaseWorldUITarget target)
        {
            _target = target;
            Init();
        }

        protected virtual void Init() { }

        /// <summary>
        /// Нужно ли включать вьюшку
        /// </summary>
        /// <returns></returns>
        public virtual bool IsEnabled()
        {
            return Target != null && Target.gameObject.activeInHierarchy && Target.gameObject.activeSelf;
        }

        /// <summary>
        /// Всё ещё существует ли объект, 
        /// информацию о котором отображает вьюшка
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDeleted()
        {
            return Target == null;
        }

        public virtual void UpdateEnabled()
        {
            gameObject.SetActive(IsEnabled());
        }

        public virtual void UpdatePosition(Camera camera)
        {
            if (!IsEnabled()) return;

            transform.position = camera.WorldToScreenPoint(Target.GetPosition());
        }
    }
}