using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Debug = UnityEngine.Debug;

using Dunegon;
using GlobalDirection = Direction.GlobalDirection;
using StraightSegment = Segment.StraightSegment;
using LeftStraightRightSegment = Segment.LeftStraightRightSegment;
using LevelMap = level.LevelMap;
using util;

public class HelperTest2 {

        private LevelMap levelMap = new LevelMap();
        Logger logger = new Logger("./Logs/dunegon.log");

    [Test]
    public void DecideOnNextSegment1() {
        List<Segment.Segment> segmentList = new List<Segment.Segment>();
        segmentList.Add(new StraightSegment(0, 0, GlobalDirection.North, null));
        segmentList.Add(new StraightSegment(1, 0, GlobalDirection.North, null));
        segmentList.Add(new StraightSegment(2, 0, GlobalDirection.North, null));
        segmentList.Add(new LeftStraightRightSegment(3, 0, GlobalDirection.North, null));

        //Assert.IsFalse(helper.checkIfSpaceIsAvailiable(globalSpaceNeeded, segmentList));
        DunegonHelper helper1 = new Dunegon.DunegonHelper(new MockedRandom(90));
        Segment.Segment nextSegment = helper1.DecideNextSegment(3, -1, GlobalDirection.West, levelMap, logger, 3, null);
        Debug.Log("Segment type: " + nextSegment.Type);
        Assert.IsTrue(nextSegment.Type == Segment.SegmentType.Straight);
    }
}
