using System;
using System.Collections.Generic;
using SegmentType = Segment.SegmentType;
using LocalDirection = Direction.LocalDirection;
using GlobalDirection = Direction.GlobalDirection;
using DirectionConversion = Direction.DirectionConversion;
using SegmentExit = Segment.SegmentExit;
using Debug = UnityEngine.Debug;
using UnityEngine;

namespace Segment {
    public abstract class Segment {
        private SegmentType _type;
        protected List<SegmentExit> _exits;
        protected int _entryX;
        protected int _entryZ;
        protected GlobalDirection _gDirection;

        private Segment _parent;

        private List<GameObject> _instantiated;

        public Segment(SegmentType type, int entryX, int entryZ, GlobalDirection gDirection, Segment parent) {
            _type = type;
            _entryX = entryX;
            _entryZ = entryZ;
            _gDirection = gDirection;
            _parent = parent;
            _instantiated = new List<GameObject>();
        }

        public SegmentType Type {
            get {
                return _type;
            }
        }

        public List<SegmentExit> Exits {
            get {
                return _exits;
            }
            set {
                _exits = value;
            }
        }

        public int X {
            get {
                return _entryX;
            }
        }

        public int Z {
            get {
                return _entryZ;
            }
        }

        public GlobalDirection GlobalDirection {
            get {
                return _gDirection;
            }
        }

        public Segment Parent {
            get {
                return _parent;
            }
        }

        public List<GameObject> Instantiated {
            get {
                return _instantiated;
            } 
            set {
                _instantiated = value;
            }
        }

        public abstract List<(int, int)> GetTiles();
        public abstract List<(int, int)> NeededSpace();
        public virtual List<Segment> GetAddOnSegments() {
            return new List<Segment>();
        }
    }
    public class StraightSegment : Segment {
        public StraightSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.Straight, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 1, 0, LocalDirection.Straight));
        }

        public override List<(int, int)> GetTiles()
        {
            List<(int, int)> tiles = new List<(int, int)>();
            tiles.Add((_entryX, _entryZ));
            return tiles;
        }

        /**
            Return needed spaces in relation to start (0, 0))
        */
        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            space.Add((1, -1));
            space.Add((1, 0));
            space.Add((1, 1));
            space.Add((2, -1));
            space.Add((2, 0));
            space.Add((2, 1));
            //space.Add((0, -1));
            //space.Add((0, 1));
            return space;
        }
    }

    public class StraightNoCheckSegment : Segment {
        public StraightNoCheckSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.StraightNoCheck, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 1, 0, LocalDirection.Straight));
        }

        public override List<(int, int)> GetTiles()
        {
            List<(int, int)> tiles = new List<(int, int)>();
            tiles.Add((_entryX, _entryZ));
            return tiles;
        }

        /**
            Return needed spaces in relation to start (0, 0))
        */
        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            return space;
        }
    }
    
    public class StopSegment : Segment {
        public StopSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.Stop, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
        }

        public override List<(int, int)> GetTiles()
        {
            var tiles = new List<(int, int)>();
            return tiles;
        }
        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            return space;
        }
    }

    public class RightSegment : Segment {
        public RightSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.Right, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 0, 1, LocalDirection.Right));
        }

        public override List<(int, int)> GetTiles()
        {
            var tiles = new List<(int, int)>();
            tiles.Add((_entryX, _entryZ));
            return tiles;
        }

        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            space.Add((1, 1));
            space.Add((0, 1));
            space.Add((-1, 1));
            space.Add((1, 2));
            space.Add((0, 2));
            space.Add((-1,2));
            //space.Add((1, 0));
            return space;
        }
    }

    public class LeftSegment : Segment {
        public LeftSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.Left, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 0, -1, LocalDirection.Left));
        }

        public override List<(int, int)> GetTiles()
        {
            var tiles = new List<(int, int)>();
            tiles.Add((_entryX, _entryZ));
            return tiles;
        }

        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            space.Add((1, -1));
            space.Add((0, -1));
            space.Add((-1, -1));
            space.Add((1, -2));
            space.Add((0, -2));
            space.Add((-1, -2));
            //space.Add((1, 0));
            return space;
        }
    }
    public class LeftRightSegment : Segment {
        public LeftRightSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.LeftRight, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 0, -1, LocalDirection.Left));
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 0, 1, LocalDirection.Right));
        }

        public override List<(int, int)> GetTiles()
        {
            var tiles = new List<(int, int)>();
            tiles.Add((_entryX, _entryZ));
            return tiles;
        }

        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            space.Add((1, -1));
            space.Add((0, -1));
            space.Add((-1, -1));
            space.Add((1, -2));
            space.Add((0, -2));
            space.Add((-1, -2));
            space.Add((1, 1));
            space.Add((0, 1));
            space.Add((-1, 1));
            space.Add((1, 2));
            space.Add((0, 2));
            space.Add((-1,2));
            return space;
        }
    }
    public class StraightRightSegment : Segment {
        private List<(int, int)> _tiles;
        public StraightRightSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.StraightRight, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 1, 0, LocalDirection.Straight));
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 0, 1, LocalDirection.Right));
            var localTiles = new List<(int, int)>();
            localTiles.Add((0, 0));
            //localTiles.Add((1, 0));
            //localTiles.Add((0, 1));
            _tiles = DirectionConversion.GetGlobalCoordinatesFromLocal(localTiles, _entryX, _entryZ, gDirection);
        }

        public override List<(int, int)> GetTiles() {
            return _tiles;
        }

        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            space.Add((3, -1));
            space.Add((3, 0));
            space.Add((3, 1));
            space.Add((2, -1));
            space.Add((2, 0));
            space.Add((2, 1));
            space.Add((1, 2));
            space.Add((0, 2));
            space.Add((-1, 2));
            space.Add((1, 3));
            space.Add((0, 3));
            space.Add((-1, 3));
            return space;
        }
        override public List<Segment> GetAddOnSegments() {
            var addOnSegments = new List<Segment>();
            foreach(SegmentExit exit in _exits) {
                var segment = new StraightNoCheckSegment(exit.X, exit.Z, exit.Direction, this);
                addOnSegments.Add(segment);
            }
            return addOnSegments;
        }
    }

    public class StraightLeftSegment : Segment {
        private List<(int, int)> _tiles;
        public StraightLeftSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.StraightLeft, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 1, 0, LocalDirection.Straight));
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 0, -1, LocalDirection.Left));
            var localTiles = new List<(int, int)>();
            localTiles.Add((0, 0));
            _tiles = DirectionConversion.GetGlobalCoordinatesFromLocal(localTiles, _entryX, _entryZ, gDirection);
        }

        public override List<(int, int)> GetTiles() {
            return _tiles;
        }

        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            space.Add((3, -1));
            space.Add((3, 0));
            space.Add((3, 1));
            space.Add((2, -1));
            space.Add((2, 0));
            space.Add((2, 1));
            space.Add((1, -2));
            space.Add((0, -2));
            space.Add((-1, -2));
            space.Add((1, -3));
            space.Add((0, -3));
            space.Add((-1, -3));
            return space;
        }

        override public List<Segment> GetAddOnSegments() {
            var addOnSegments = new List<Segment>();
            foreach(SegmentExit exit in _exits) {
                var segment = new StraightNoCheckSegment(exit.X, exit.Z, exit.Direction, this);
                addOnSegments.Add(segment);
            }
            return addOnSegments;
        }
    }

    public class LeftStraightRightSegment : Segment {
        private List<(int, int)> _tiles;

        public LeftStraightRightSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.Left, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 0, -1, LocalDirection.Left));
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 0, 1, LocalDirection.Right));
            _exits.Add(new SegmentExit(_entryX, _entryZ, gDirection, 1, 0, LocalDirection.Straight));
            var localTiles = new List<(int, int)>();
            localTiles.Add((0, 0));
            _tiles = DirectionConversion.GetGlobalCoordinatesFromLocal(localTiles, _entryX, _entryZ, gDirection);
        }

        public override List<(int, int)> GetTiles()
        {
            return _tiles;
        }

        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            //Left
            space.Add((1, -2));
            space.Add((0, -2));
            space.Add((-1, -2));
            space.Add((1, -3));
            space.Add((0, -3));
            space.Add((-1, -3));
            //Straight(2, -1));
            space.Add((2, 0));
            space.Add((2, 1));
            space.Add((2, -1));
            space.Add((3, -1));
            space.Add((3, 0));
            space.Add((3, 1));
            //Right(1, 2));
            space.Add((0, 2));
            space.Add((1, 2));
            space.Add((-1, 2));
            space.Add((1, 3));
            space.Add((0, 3));
            space.Add((-1,3));
            return space;
        }

        override public List<Segment> GetAddOnSegments() {
            var addOnSegments = new List<Segment>();
            foreach(SegmentExit exit in _exits) {
                var segment = new StraightNoCheckSegment(exit.X, exit.Z, exit.Direction, this);
                addOnSegments.Add(segment);
            }
            return addOnSegments;
        }
    }
    public class DoubleStraightSegment : Segment {
        private List<(int, int)> _tiles;
        public DoubleStraightSegment(int x, int z, GlobalDirection gDirection, Segment parent) : base(SegmentType.DoubleStraight, x, z, gDirection, parent) {
            _exits = new List<SegmentExit>();
            var localTiles = new List<(int, int)>();
            localTiles.Add((0, 0));
            localTiles.Add((1, 0));
            localTiles.Add((2, 0));
            _tiles = DirectionConversion.GetGlobalCoordinatesFromLocal(localTiles, _entryX, _entryZ, gDirection);
        }

        public override List<(int, int)> GetTiles() {
            return _tiles;
        }

        /**
            Return needed spaces in relation to start (0, 0))
        */
        override public List<(int, int)> NeededSpace() {
            var space = new List<(int, int)>();
            space.Add((0, 0));
            space.Add((1, 0));
            space.Add((1, -1));
            space.Add((1, 1));
            return space;
        }
    }
}
