using System;
using System.Collections.Generic;
using System.Text;

namespace Databaser
{
    public class Realm
    {
        public Map Map1(Map map)
        {
            map.Gen(new int[,] {
            {0,0,0,0,0,0,0,0 },
            {0,2,2,2,2,2,2,0 },
            {0,1,1,1,1,1,1,0 },
            {0,1,1,1,1,1,1,0 },
            }, 64);
            return map;
        }

        public Map Map2(Map map)
        {
            map.Gen(new int[,] {
            {0,0,0,0,0,0,0,0 },
            {0,2,2,2,2,0,2,0 },
            {0,1,1,1,1,1,1,0 },
            {0,1,1,1,1,1,1,0 },
            }, 64);
            return map;
        }

        public Map Map3(Map map)
        {
            map.Gen(new int[,] {
            {0,0,0,0,0,0,0,0 },
            {0,2,2,2,2,2,2,0 },
            {0,1,1,1,1,1,1,0 },
            {0,1,1,0,1,1,1,0 },
            }, 64);
            return map;
        }
    }
}