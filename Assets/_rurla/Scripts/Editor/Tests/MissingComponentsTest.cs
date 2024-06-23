using _rurla.Scripts.Editor.Utils;
using _rurla.Scripts.Editor.Validation;
using FluentAssertions;
using NUnit.Framework;

namespace _rurla.Scripts.Editor.Tests
{
    public class MissingComponentsTest
    {
        [TestCaseSource(typeof(SceneUtils), nameof(SceneUtils.AllProjectScenePaths))]
        public void AllGameObjectsShouldNotHaveMissingComponents(string scenePath)
        {
            MissingComponentsValidator
                .FindAllMissingComponents(scenePath)
                .Should()
                .BeEmpty();
        }
    }
}


