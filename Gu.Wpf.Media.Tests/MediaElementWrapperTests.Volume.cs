﻿// ReSharper disable SuppressSetMutable
namespace Gu.Wpf.Media.Tests
{
    using System.Threading;
    using System.Windows.Input;

    using NUnit.Framework;

    public class MediaElementWrapperTests
    {
        [Apartment(ApartmentState.STA)]
        public class Volume
        {
            [Test]
            public void SetVolumeToZeroSetsIsMutedToTrue()
            {
                var wrapper = new MediaElementWrapper();
                Assert.AreEqual(false, wrapper.IsMuted);
                wrapper.Volume = 0;
                Assert.AreEqual(true, wrapper.IsMuted);
                wrapper.Volume = 0.5;
                Assert.AreEqual(false, wrapper.IsMuted);
            }

            [Test]
            public void IsMutedCoercesWhenVolumeIsZero()
            {
                var wrapper = new MediaElementWrapper();
                Assert.AreEqual(false, wrapper.IsMuted);
                wrapper.Volume = 0.0;
                Assert.AreEqual(true, wrapper.IsMuted);
                wrapper.IsMuted = false;
                Assert.AreEqual(true, wrapper.IsMuted);
                wrapper.Volume = 0.5;
                Assert.AreEqual(false, wrapper.IsMuted);
            }

            [TestCase(0.1, 0.5)]
            [TestCase(-0.1, 0.4)]
            [TestCase("", 0.50)]
            [TestCase(null, 0.55)]
            public void IncreaseVolumeWithParameter(object increment, double expected)
            {
                var wrapper = new MediaElementWrapper();
                Assert.AreEqual(0.5, wrapper.Volume);
                Assert.AreEqual(0.05, wrapper.VolumeIncrement);
                MediaCommands.IncreaseVolume.Execute(increment, wrapper);
                Assert.AreEqual(expected, wrapper.Volume);
            }

            [TestCase(0.1, 0.4)]
            [TestCase(-0.1, 0.6)]
            [TestCase("", 0.45)]
            [TestCase(null, 0.45)]
            public void DecreaseVolumeWithParameter(object increment, double expected)
            {
                var wrapper = new MediaElementWrapper();
                Assert.AreEqual(0.5, wrapper.Volume);
                Assert.AreEqual(0.05, wrapper.VolumeIncrement);
                MediaCommands.DecreaseVolume.Execute(increment, wrapper);
                Assert.AreEqual(expected, wrapper.Volume);
            }
        }
    }
}
