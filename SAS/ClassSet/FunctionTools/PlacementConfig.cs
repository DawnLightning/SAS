using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAS.ClassSet.FunctionTools
{
    class PlacementConfig
    {
        public PlacementConfig()
        {
        }
        public PlacementConfig(int week, int day, int classweek, int max, int min, int proportion)
        {
            this.cbegin_week = week;
            this.cbegin_day = day;
            this.cnumclass_week = classweek;
            this.cnumpeo_max = max;
            this.cnumpeo_min = min;
            this.Proportion = proportion;
        }
        private int cbegin_week;//开始周   

        public int Cbegin_week
        {
            get { return cbegin_week; }
            set { cbegin_week = value; }
        }
        private int cbegin_day;//开始天

        public int Cbegin_day
        {
            get { return cbegin_day; }
            set { cbegin_day = value; }
        }
        private int cnumclass_week;//每周安排的次数

        public int Cnumclass_week
        {
            get { return cnumclass_week; }
            set { cnumclass_week = value; }
        }
        private int cnumpeo_max;//最大人数

        public int Cnumpeo_max
        {
            get { return cnumpeo_max; }
            set { cnumpeo_max = value; }
        }
        private int cnumpeo_min;//最小人数

        public int Cnumpeo_min
        {
            get { return cnumpeo_min; }
            set { cnumpeo_min = value; }
        }
        private int proportion;//课程比例
        public int Proportion
        {
            get { return proportion; }
            set { proportion = value; }
        }
    }
}
