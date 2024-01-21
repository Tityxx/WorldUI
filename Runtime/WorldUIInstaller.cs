using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Tityx.WorldUI
{
    [CreateAssetMenu(menuName = "ToolsAndMechanics/World UI/Installer")]
    public class WorldUIInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WorldUIContainer>().FromInstance(new WorldUIContainer(Container)).AsSingle();
        }
    }
}