using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Tityx.WorldUI
{
    /// <summary>
    /// Таргет для вьюшек. Наследуемые от него классы 
    /// могут хранить ссылки на какие-нибудь компоненты для вьюшек
    /// </summary>
    public class BaseWorldUITarget : MonoBehaviour
    {
        public List<string> RenderersId => _renderersId;

        [Tooltip("Init on Awake or on Enable")]
        [SerializeField] protected bool _initOnAwake = true;
        [SerializeField] protected List<string> _renderersId;
        [SerializeField] protected BaseWorldUIView _prefab;

        [Inject] protected WorldUIContainer _worldUIContainer;

        protected virtual void Awake()
        {
            if (_initOnAwake)
                Init();
        }

        protected virtual void OnEnable()
        {
            if (!_initOnAwake)
                Init();
        }

        protected virtual void Init()
        {
            _worldUIContainer.AddView(this, _prefab);
        }

        public virtual Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}