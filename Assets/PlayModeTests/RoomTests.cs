using System.Collections;
using System.Collections.Generic;
using Direction;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using util;
using GlobalDirection = Direction.GlobalDirection;
using Room3x3Segment = Segment.Room3x3Segment;
using SegmentType = Segment.SegmentType;

public class RoomTests
{
    [Test]
    public void Room3x3Test() {
        var room3x3 = new Room3x3Segment(0, 0, GlobalDirection.North, 1, null);
        var expectedTiles = new List<(int, int)>{
            (0, -1),
            (0, 0),
            (0, 1),
            (1, -1),
            (1, 0),
            (1, 1),
            (2, -1),
            (2, 0),
            (2, 1)        
        };
        Assert.That(room3x3.GetTiles(), Is.EquivalentTo(expectedTiles));
        
        var expectedSpace = new List<(int, int)>{
            (-1, -2), 
            (-1, -1), 
            (-1, 1), 
            (-1, 2), 
            (0, -2), 
            (0, -1), 
            (0, 0), 
            (0, 1), 
            (0, 2), 
            (1, -2), 
            (1, -1), 
            (1, 0), 
            (1, 1), 
            (1, 2), 
            (2, -2), 
            (2, -1), 
            (2, 0), 
            (2, 1), 
            (2, 2), 
            (3, -2), 
            (3, -1), 
            (3, 0), 
            (3, 1), 
            (3, 2), 
            (4, -2), 
            (4, -1), 
            (4, 0), 
            (4, 1), 
            (4, 2)      
        };
        Debug.Log("expected: " + expectedSpace.Count);
        Debug.Log("neededSpace: " + room3x3.NeededSpace().Count);
        //Assert.That(room3x3.NeededSpace(), Is.EquivalentTo(expectedSpace));
        Assert.That(room3x3.NeededSpace(), Is.EquivalentTo(expectedSpace));
    }

    [Test]
    public void RemoveSurplusExits1Test() {
        var room = new TestRoom(0, 0, GlobalDirection.North, null, new MockedRandom(0));
        var potentialLocalExits = new List<(int, int, LocalDirection)>(){
            (1, -2, LocalDirection.Left),
            (2, 2, LocalDirection.Right),
            (4, 0, LocalDirection.Straight)
        };
        room.SurplusExits(potentialLocalExits, 1);
        Assert.AreEqual(potentialLocalExits.Count, 1);
    }

    [Test]
    public void RemoveSurplusExits0Test() {
        var room = new TestRoom(0, 0, GlobalDirection.North, null, new MockedRandom(0));
        var potentialLocalExits = new List<(int, int, LocalDirection)>(){
            (1, -2, LocalDirection.Left),
            (2, 2, LocalDirection.Right),
            (4, 0, LocalDirection.Straight)
        };
        room.SurplusExits(potentialLocalExits, 0);
        Assert.AreEqual(potentialLocalExits.Count, 0);
    }

    [Test]
    public void RemoveSurplusExits30Test() {
        var room = new TestRoom(0, 0, GlobalDirection.North, null, new MockedRandom(0));
        var potentialLocalExits = new List<(int, int, LocalDirection)>(){
            (1, -2, LocalDirection.Left),
            (2, 2, LocalDirection.Right),
            (4, 0, LocalDirection.Straight)
        };
        room.SurplusExits(potentialLocalExits, 3);
        Assert.AreEqual(potentialLocalExits.Count, 3);
    }

    [Test]
    public void GetNumberOfExits0Test() {
        var room = new TestRoom(0, 0, GlobalDirection.North, null, new MockedRandom(0));
        var percentageOfExits = new List<(int, int)>() {
            (0, 40),
            (1, 30),
            (2, 20),
            (3, 10)
        };
        Assert.AreEqual(0, room.NoOfExits(percentageOfExits));
    }

    [Test]
    public void GetNumberOfExits40Test() {
        var room = new TestRoom(0, 0, GlobalDirection.North, null, new MockedRandom(40));
        var percentageOfExits = new List<(int, int)>() {
            (0, 40),
            (1, 30),
            (2, 20),
            (3, 10)
        };
        Assert.AreEqual(0, room.NoOfExits(percentageOfExits));
    }

    [Test]
    public void GetNumberOfExits41Test() {
        var room = new TestRoom(0, 0, GlobalDirection.North, null, new MockedRandom(41));
        var percentageOfExits = new List<(int, int)>() {
            (0, 40),
            (1, 30),
            (2, 20),
            (3, 10)
        };
        Assert.AreEqual(1, room.NoOfExits(percentageOfExits));
    }

    [Test]
    public void GetNumberOfExits100Test() {
        var room = new TestRoom(0, 0, GlobalDirection.North, null, new MockedRandom(100));
        var percentageOfExits = new List<(int, int)>() {
            (0, 40),
            (1, 30),
            (2, 20),
            (3, 10)
        };
        Assert.AreEqual(3, room.NoOfExits(percentageOfExits));
    }
}

public class TestRoom: Segment.Room {
    public TestRoom(int x, int z, GlobalDirection gDirection, Segment.Segment parent, RandomGenerator randomGenerator) : base(SegmentType.Room3x4, x, z, gDirection, parent, randomGenerator) {
    }
    public void SurplusExits(List<(int, int, LocalDirection)> potentialLocalExits, int noOfExits) {
        RemoveSurplusExits(potentialLocalExits, noOfExits);
    }
    public int NoOfExits(List<(int, int)> percentageOfExits) {
        return GetNumberOfExits(percentageOfExits);
    }
}
