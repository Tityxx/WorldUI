using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Tityx.WorldUI
{
    /// <summary>
    /// Контейнер для вьюшек и их создание
    /// </summary>
    public class WorldUIContainer
    {
        public event Action<BaseWorldUIView> onViewAdded = delegate { };
        public List<BaseWorldUIView> ViewList { get; private set; } = new List<BaseWorldUIView>();
        public WorldUIRenderer Renderer { get; private set; }

        private DiContainer _container;

        private List<WorldUIRenderer> _renderers = new List<WorldUIRenderer>();

        public WorldUIContainer(DiContainer container)
        {
            _container = container;
        }

        public void AddRenderer(WorldUIRenderer renderer)
        {
            if (_renderers.Contains(renderer) || renderer.Id == string.Empty) return;

            _renderers.Add(renderer);
        }

        public WorldUIRenderer GetRenderer(string id)
        {
            return _renderers.FirstOrDefault(renderer => renderer.Id == id);
        }

        public void SetActiveRenderer(string id, bool active)
        {
            var renderer = GetRenderer(id);
            if (renderer)
            {
                renderer.SetActive(active);
            }
        }

        /// <summary>
        /// Создание вьюшки
        /// </summary>
        /// <param name="target"></param>
        /// <param name="prefab"></param>
        public void AddView(BaseWorldUITarget target, BaseWorldUIView prefab)
        {
            var view = _container.InstantiatePrefabForComponent<BaseWorldUIView>(prefab);
            view.Init(target);

            ViewList.Add(view);
            onViewAdded(view);
        }

        /// <summary>
        /// Проверка на то, существует ли объект, 
        /// к которому привязана вьюшка.
        /// Если нет, удаляем её
        /// </summary>
        public void CheckDeleted()
        {
            for (int i = 0; i < ViewList.Count; i++)
            {
                if (ViewList[i].IsDeleted())
                {
                    GameObject.Destroy(ViewList[i].gameObject);
                    ViewList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}