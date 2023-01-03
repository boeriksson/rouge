using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Direction;
using DirectionConversion = Direction.DirectionConversion;
using GlobalDirection = Direction.GlobalDirection;
using LocalDirection = Direction.LocalDirection;

public class DirectionTests
{
    [Test]
    public void DirectionConversionNorthStraight() {
        var gd = GlobalDirection.North; 
        var ld = LocalDirection.Straight;
        Assert.That(DirectionConversion.GetDirection(gd, ld), Is.EqualTo(GlobalDirection.North));
    }

    [Test]
    public void DirectionConversionNorthLeft() {
        var gd = GlobalDirection.North; 
        var ld = LocalDirection.Left;
        Assert.That(DirectionConversion.GetDirection(gd, ld), Is.EqualTo(GlobalDirection.West));
    }

    [Test]
    public void DirectionConversionWestRight() {
        var gd = GlobalDirection.West; 
        var ld = LocalDirection.Right;
        Assert.That(DirectionConversion.GetDirection(gd, ld), Is.EqualTo(GlobalDirection.North));
    }

    [Test]
    public void DirectionConversionSouthRight() {
        var gd = GlobalDirection.South; 
        var ld = LocalDirection.Right;
        Assert.That(DirectionConversion.GetDirection(gd, ld), Is.EqualTo(GlobalDirection.West));
    }

    [Test]
    public void DirectionConversionSouthStraight() {
        var gd = GlobalDirection.South; 
        var ld = LocalDirection.Straight;
        Assert.That(DirectionConversion.GetDirection(gd, ld), Is.EqualTo(GlobalDirection.South));
    }

    [Test]
    public void DirectionConversionSouthLeft() {
        var gd = GlobalDirection.South; 
        var ld = LocalDirection.Left;
        Assert.That(DirectionConversion.GetDirection(gd, ld), Is.EqualTo(GlobalDirection.East));
    }

    [Test]
    public void GlobalFromLocal1() {
        var startX = 0;
        var startZ = 0;
        var localCoord = new List<(int, int)>();
        localCoord.Add((5, -1));
        localCoord.Add((5, 0));
        localCoord.Add((5, 1));

        var globalCoord = DirectionConversion.GetGlobalCoordinatesFromLocal(
            localCoord, 
            startX, 
            startZ, 
            GlobalDirection.North
        );

        Assert.That(globalCoord[0], Is.EqualTo((5, -1)));
        Assert.That(globalCoord[1], Is.EqualTo((5, 0)));
        Assert.That(globalCoord[2], Is.EqualTo((5, 1)));
    }

    [Test]
    public void GlobalFromLocal2() {
        var startX = 5;
        var startZ = 0;
        var localCoord = new List<(int, int)>();
        localCoord.Add((5, -1));
        localCoord.Add((5, 0));
        localCoord.Add((5, 1));

        var globalCoord = DirectionConversion.GetGlobalCoordinatesFromLocal(
            localCoord, 
            startX, 
            startZ, 
            GlobalDirection.South
        );

        Assert.That(globalCoord[0], Is.EqualTo((0, 1)));
        Assert.That(globalCoord[1], Is.EqualTo((0, 0)));
        Assert.That(globalCoord[2], Is.EqualTo((0, -1)));
    }

    [Test]
    public void GlobalFromLocal3() {
        var startX = 0;
        var startZ = 0;
        var localCoord = new List<(int, int)>();
        localCoord.Add((5, -1));
        localCoord.Add((5, 0));
        localCoord.Add((5, 1));

        var globalCoord = DirectionConversion.GetGlobalCoordinatesFromLocal(
            localCoord, 
            startX, 
            startZ, 
            GlobalDirection.West
        );

        Assert.That(globalCoord[0], Is.EqualTo((-1, -5)));
        Assert.That(globalCoord[1], Is.EqualTo((0, -5)));
        Assert.That(globalCoord[2], Is.EqualTo((1, -5)));
    }

    [Test]
    public void GlobalFromLocal4() {
        var startX = 0;
        var startZ = 0;
        var localCoord = new List<(int, int)>();
        localCoord.Add((5, -1));
        localCoord.Add((5, 0));
        localCoord.Add((5, 1));

        var globalCoord = DirectionConversion.GetGlobalCoordinatesFromLocal(
            localCoord, 
            startX, 
            startZ, 
            GlobalDirection.East
        );

        Assert.That(globalCoord[0], Is.EqualTo((1, 5)));
        Assert.That(globalCoord[1], Is.EqualTo((0, 5)));
        Assert.That(globalCoord[2], Is.EqualTo((-1, 5)));
    }
}
