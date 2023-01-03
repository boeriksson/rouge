using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Dunegon;
using GlobalDirection = Direction.GlobalDirection;
using StraightSegment = Segment.StraightSegment;
using LevelMap = level.LevelMap;
using SegmentType = Segment.SegmentType;

public class HelperTests {

    private DunegonHelper helper = new Dunegon.DunegonHelper();
    private List<(int, int)> space1 = new List<(int, int)>();
    private List<(int, int)> space2 = new List<(int, int)>();
    private List<(int, int)> space3 = new List<(int, int)>();

    private List<(int, int)> globalSpaceNeeded = new List<(int, int)>();

    private List<Segment.Segment> segmentList = new List<Segment.Segment>();

    private LevelMap levelMap = new LevelMap();

    Logger logger = new Logger("./Logs/dunegon.log");


    [SetUp]
    public void SetUp() {
        space1.Add((5, -1));
        space1.Add((5, 0));
        space1.Add((5, 1));
        space2.Add((6, 0));
        space2.Add((5, 0));
        space2.Add((4, 0));
        space3.Add((6, -1));
        space3.Add((6, 0));
        space3.Add((6, 1));
    }
    
    [Test]
    public void checkIfSpaceIsAvailiable1Test() {
        globalSpaceNeeded.Add((1,0));
        var segment = new StraightSegment(0, 0, GlobalDirection.North, null);
        levelMap.AddCooridnates(segment.GetTiles(), 1);
        segmentList.Add(segment);
        Assert.IsTrue(helper.checkIfSpaceIsAvailiable(globalSpaceNeeded, levelMap, logger, SegmentType.Straight));
    }

    [Test]
    public void checkIfSpaceIsAvailiable2Test() { //Hook
        globalSpaceNeeded.Add((1,-2));
        globalSpaceNeeded.Add((1,-1));
        globalSpaceNeeded.Add((1,0));
        var segment1 = new StraightSegment(0, 0, GlobalDirection.North, null);
        var segment2 = new StraightSegment(1, 0, GlobalDirection.North, null);
        var segment3 = new StraightSegment(2, 0, GlobalDirection.North, null);
        var segment4 = new StraightSegment(2, -1, GlobalDirection.West, null);
        segmentList.Add(segment1);
        segmentList.Add(segment2);
        segmentList.Add(segment3);
        segmentList.Add(segment4);
        levelMap.AddCooridnates(segment1.GetTiles(), 1);
        levelMap.AddCooridnates(segment2.GetTiles(), 1);
        levelMap.AddCooridnates(segment3.GetTiles(), 1);
        levelMap.AddCooridnates(segment4.GetTiles(), 1);
        Assert.IsFalse(helper.checkIfSpaceIsAvailiable(globalSpaceNeeded, levelMap, logger, SegmentType.LeftStraightRight));
    }
}
