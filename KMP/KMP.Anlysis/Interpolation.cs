using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace KMP.Anlysis
{
    
    public class Interpolation
    {

        Dictionary<int, Dictionary<int, double>> _originMap = new Dictionary<int, Dictionary<int, double>>();

        public Interpolation()
        {
            Dictionary<int, double> tmp4 = new Dictionary<int, double> { {220,0.0959 },
                {260, 8.84 }, { 300, 8.39}, { 400, 7.83}, { 500, 7.59}, { 700, 7.39},
                { 1000, 7.29}, { 3000, 7.20}, { 5000, 7.20} };
            Dictionary<int, double> tmp5 = new Dictionary<int, double> { {140,0.0929 },
                {160, 8.02 }, { 200, 6.58}, { 240, 5.86}, { 300, 5.32}, { 400, 4.94},
                { 500, 4.78}, { 700, 4.65}, { 1000, 4.59}, { 3000, 4.54}, { 5000, 4.53} };
            Dictionary<int, double> tmp6 = new Dictionary<int, double> { {120,0.0837 },
                {160, 5.84 }, { 200, 4.69}, { 240, 4.11}, { 300, 3.69}, { 400, 3.41},
                { 500, 3.29}, { 700, 3.20}, { 1000, 3.16}, { 3000, 3.12}, { 5000, 3.12} };
            Dictionary<int, double> tmp8 = new Dictionary<int, double> { {100,6.6 },
                {160, 3.72 }, { 200, 2.85}, { 240, 2.42}, { 300, 2.12}, { 400, 1.92},
                { 500, 1.84}, { 700, 1.79}, { 1000, 1.76}, { 2000, 1.74}, { 5000, 1.74} };
            Dictionary<int, double> tmp10 = new Dictionary<int, double> { {56,0.0964 },
                {70, 7.2 }, { 100, 4.63}, { 120, 3.71}, { 200, 2.01}, { 240, 1.65},
                { 300, 1.39}, { 400, 1.24}, { 500, 1.18}, { 700, 1.14}, { 1000, 1.12},
                { 1600, 1.11},{ 5000, 1.11} };
            Dictionary<int, double> tmp15 = new Dictionary<int, double> { {34,0.0968 },
                {40, 7.7 }, { 60, 4.53}, { 100, 2.44}, { 120, 1.97}, { 200, 1.09},
                { 240, 0.0089}, { 300, 6.91}, { 400, 5.73}, { 500, 5.34}, { 600, 5.16},
                { 1000, 4.97},{ 4000, 4.9}, { 5000, 4.9} };
            Dictionary<int, double> tmp20 = new Dictionary<int, double> { {24,0.0982 },
                {40, 4.77 }, { 60, 2.86}, { 80, 2.03}, { 100, 1.56}, { 120, 1.27},
                { 200, 0.0071}, { 300, 4.46}, {340,3.88 }, { 400, 3.42}, { 500, 3.08}, { 700, 2.87},
                { 1000, 2.80},{ 4000, 2.75}, { 5000, 2.75} };
            Dictionary<int, double> tmp25 = new Dictionary<int, double> { {20,0.0877 },
                {30, 4.84 }, { 50, 2.50}, { 80, 1.43}, { 100, 1.11}, { 120, 0.00902},
                { 200, 5.08}, { 300, 3.23}, {340,2.78 }, { 400, 2.35}, { 440, 2.19}, { 500, 2.04},
                { 600, 1.91},{ 700, 1.86},{ 1000, 1.80},{ 3000, 1.76}, { 5000, 1.76} };

            Dictionary<int, double> tmp30 = new Dictionary<int, double> { {16,0.0904 },
                {20, 6.35 }, { 30, 3.57}, { 40, 2.46},{ 60, 1.5},{ 80, 1.08}, { 100, 0.00838}, { 120, 6.83},
                { 200, 3.88}, { 300, 2.46}, {400,1.77 }, { 440, 1.61}, { 500, 1.47},
                { 600, 1.36},{ 700, 1.3},{ 1000, 1.25},{ 3000, 1.22}, { 5000, 1.22} };
            Dictionary<int, double> tmp40 = new Dictionary<int, double> { {12,0.0864 },
                {20, 3.85 }, { 30, 2.22}, { 40, 1.55},{ 60, 0.00958},{ 80, 6.91}, { 100, 5.39}, { 120, 4.41},
                { 200, 2.52},  {400,1.17 },  { 500, 0.000912},
                { 600, 8.04},{ 700, 7.56},{ 800, 7.31},{ 1000, 7.08},{ 1600, 6.92},{ 4000, 6.88}, { 5000, 6.88} };

            Dictionary<int, double> tmp50 = new Dictionary<int, double> { {9,0.093 },
                {10, 7.82 }, {20, 2.63 },{ 30, 1.54}, { 40, 1.08},{ 60, 0.00677},{ 80, 4.90}, { 100, 3.84}, { 200, 1.71},
                 { 400, 0.000842},{500, 6.52 },
                { 600, 5.48},{ 700, 5.02},{ 800, 4.78},{ 1000, 4.58},{ 1200, 4.49},{ 1600, 4.44},{ 4000, 4.4}, { 5000, 4.4} };

            Dictionary<int, double> tmp60 = new Dictionary<int, double> { {7,0.0954 },
                {10, 5.56 },{14, 3.23 }, { 20, 1.93}, { 40, 0.00812},{ 60, 5.1},{ 80, 3.71}, { 100, 2.91},
                { 200, 1.38},  {400,6.45 },  { 300, 0.000886},
                { 600, 4.09},{ 700, 3.64},{ 800, 3.41},{ 1000, 3.22},{ 1400, 3.1},{ 4000, 3.06}, { 5000, 3.06} };

            Dictionary<int, double> tmp80 = new Dictionary<int, double> { {5,0.099 },
                {7, 6.08 },{9, 3.91 },{10, 3.28 },{14, 1.96 },{20, 1.2 },{24, 0.0095 }, { 40, 5.16},{ 60, 3.28},{ 80, 2.39}, { 100, 1.88},
                {400,4.24 },  { 200, 0.000895},
                { 660, 2.41},{ 800, 2.05},{ 1000, 1.86},{ 1400, 1.76},{ 3000, 1.72}, { 5000, 1.72} };

            Dictionary<int, double> tmp100 = new Dictionary<int, double> { {5,0.0741 },
                {7, 3.98 },{10, 2.2 },{14, 1.33 },{20, 0.00831 }, { 40, 3.64},{ 50, 2.83},{ 80, 1.7}, { 100, 1.34},
                {400,3.05 },  { 200, 0.000641},
                { 600, 1.95},{ 800, 1.42},{ 1000, 1.24},{ 1400, 1.14},{ 2500, 1.1}, { 5000, 1.1} };
            //
            Dictionary<int, double> tmp125 = new Dictionary<int, double> { {5,0.048 },
                {6, 3.44 }, { 8, 2.10}, { 10, 1.48},{ 14, 0.00917},{ 20, 5.78}, { 40, 2.57}, { 60, 1.65},{ 80, 1.21},
                { 100, 0.000955},
                { 200, 4.59},  {400,2.2 }, 
                { 600, 1.41},{ 900, 0.0000904},{ 1000, 8.37},{ 1200, 7.7},{ 1400, 7.4},{ 2000, 7.13},{ 4000, 7.04}, { 5000, 7.04} };

            Dictionary<int, double> tmp150 = new Dictionary<int, double> { {5,0.0338 },
                {6, 2.44 }, { 8, 1.51}, { 10, 1.08},{ 12, 0.00833},{ 16, 5.69}, { 20, 4.31}, { 40, 1.94},{ 60, 1.25},{ 100, 0.000726},
                { 200, 3.49},  {400,1.68 }, 
                { 600, 1.08},{ 800, 0.0000787},{ 1000, 6.19},{ 1200, 5.53},{ 1600, 5.1},{ 2000, 4.98},{ 4000, 4.89}, { 5000, 4.89} };
            //
            Dictionary<int, double> tmp200 = new Dictionary<int, double> { {5,0.0196 },{6,1.43 },{8,0.00909 },
                {10, 6.59 }, { 14, 4.21},{ 20, 2.72}, { 30, 1.71},{ 50, 0.000976},{ 80, 5.92}, { 100, 4.69}, { 200, 2.27},
                {400,1.10 },  { 600, 0.0000711},
                { 800, 5.2},{ 1000, 4.03},{ 1200, 3.38},{ 1400, 3.09},{ 1600, 2.95},{ 2000, 2.83},{ 4000, 2.75}, { 5000, 2.75} };

            Dictionary<int, double> tmp250 = new Dictionary<int, double> { {5,0.0129 },{6,0.00955 },{8,6.17 },
                {10, 4.52 },{14, 2.93 }, { 20, 1.91}, { 40, 0.000881},{ 60,5.72},{ 80, 4.22}, { 100, 3.35}, 
                { 200, 1.63},  {400,0.0000789 },  
                { 600, 5.13},{ 800, 3.77},{ 1000, 2.93},{ 1200, 2.38},{ 1400, 2.1},{ 1600, 1.96},{ 2000, 1.84},{ 4000, 1.76}, { 5000, 1.76} };

            Dictionary<int, double> tmp300 = new Dictionary<int, double> { {5,0.00923 },{6,6.9 },{8,4.52 },
                {10, 3.34 },{12, 2.64 }, { 20, 1.43}, { 40, 0.000666},{ 60,4.33},{ 80, 3.21}, { 100, 2.54},
                { 200, 1.24},  {400,0.0000602 },
                { 600, 3.93},{ 800, 2.87},{ 1000, 2.25},{ 1400, 1.56},{ 1600, 1.42},{ 2000, 1.30},{ 4000, 1.23}, { 5000, 1.22} };

            Dictionary<int, double> tmp400 = new Dictionary<int, double> { {5,0.00549 },{6,4.17 },{8,2.78 },
                {10, 2.08 },{12, 1.66 },{16, 1.18 }, { 20, 0.000914}, { 40, 4.29},{ 60,2.8},{ 80, 2.07}, { 100, 1.65},
                { 400, 3.93},  {200,0.0000808 },
                { 600, 2.57},{ 800, 1.89},{ 1000, 1.48},{ 1400, 1.02},{ 1600, 0.00000882}};
            
            //
            Dictionary<int, double> tmp500 = new Dictionary<int, double> { {5,0.0037 },
                {6, 2.84 }, { 8, 1.92}, { 10, 1.45},{ 12, 1.16},{ 16, 0.00083}, { 20, 6.45}, { 40, 3.05},
                { 60, 1.99},  {80,1.48 },  { 100, 1.18},
                { 200,0.0000579},{ 400, 2.82},{ 600, 1.85},{ 800, 1.37},{ 1000, 1.07},{ 1200, 0.0000088} };
            Dictionary<int, double> tmp600 = new Dictionary<int, double> { {5,0.0027 },
                {6, 2.08 }, { 8, 1.42}, { 10, 1.08},{ 12, 0.000868},{ 16, 6.24}, { 20, 4.86}, { 40, 2.31},
                { 60, 1.51},  {80,1.12 },  { 100, 0.0000894},
                { 200, 4.39},{ 400, 2.16},{ 600, 1.41},{ 800, 1.04},{ 840, 0.00000988} };
            Dictionary<int, double> tmp800 = new Dictionary<int, double> { {5,0.00165 },
                {6, 1.29}, { 8, 0.000892}, { 10, 6.82},{ 12, 5.51},{ 16, 3.98}, { 20, 3.12}, { 40, 1.49},
                { 60, 0.000098},  {80,7.28 },  { 100, 5.8},
                { 200, 2.86},{ 400, 1.4},{ 500, 1.12},{ 560, 0.00000992}};
            Dictionary<int, double> tmp1000 = new Dictionary<int, double> { {5,0.00113 },
                {6, 0.000891 }, { 7, 7.33}, { 9, 5.41},{ 12, 3.88},{ 16, 2.82}, { 20, 2.21}, { 40, 1.06},
                { 70, 0.0000596},  {100,4.14 },  { 200, 2.04},
                { 400, 1.01},{ 420, 0.00000957} };
            this._originMap.Add(4, tmp4);
            this._originMap.Add(5, tmp5);
            this._originMap.Add(6, tmp6);
            this._originMap.Add(8, tmp8);
            this._originMap.Add(10, tmp10);
            this._originMap.Add(15, tmp15);
            this._originMap.Add(20, tmp20);
            this._originMap.Add(25, tmp25);
            this._originMap.Add(30, tmp30);
            this._originMap.Add(40, tmp40);
            this._originMap.Add(50, tmp50);
            this._originMap.Add(60, tmp60);
            this._originMap.Add(80, tmp80);
            this._originMap.Add(100, tmp100);
            this._originMap.Add(125, tmp125);
            this._originMap.Add(150, tmp150);
            this._originMap.Add(200, tmp200);
            this._originMap.Add(250, tmp250);
            this._originMap.Add(300, tmp300);
            this._originMap.Add(400, tmp400);
            this._originMap.Add(500, tmp500);
            this._originMap.Add(600, tmp600);
            this._originMap.Add(800, tmp800);
            this._originMap.Add(1000, tmp1000);
        }
        public double executed(int x, int y)
        {
            int[] keys = _originMap.Keys.ToArray();
            int x1 = 4, x2= 4;
            getRange(keys, x, ref x1, ref x2);
            int[] keys_x1 = _originMap[x1].Keys.ToArray();
            int y1_1 = 200, y1_2 = 200;
            getRange(keys_x1, y, ref y1_1,ref y1_2);
            
            int[] keys_x2 = _originMap[x2].Keys.ToArray();
            int y2_1 = 200, y2_2 = 200;
            getRange(keys_x2, y, ref y2_1, ref y2_2);
            //idw
            double d11 = Math.Sqrt(Math.Pow(x - x1, 2) + Math.Pow(y / 100.0 - y1_1 / 100.0, 2));
            double d12 = Math.Sqrt(Math.Pow(x - x1, 2) + Math.Pow(y / 100.0 - y1_2 / 100.0, 2));
            double d21 = Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y / 100.0 - y2_1 / 100.0, 2));
            double d22 = Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y / 100.0 - y2_2 / 100.0, 2));

            double sum_d = 1 / d11 + 1 / d12 + 1 / d21 + 1 / d22;
            double res = (1 / d11) / sum_d * _originMap[x1][y1_1] + (1 / d12) / sum_d * _originMap[x1][y1_2] + (1 / d21) / sum_d * _originMap[x2][y2_1] + (1 / d22) / sum_d * _originMap[x2][y2_2];
            return res;



        }

        private void getRange(int[] keys,int x, ref int x1, ref int x2)
        {
            x1 = keys[0];
            x2 = keys[0];
            for (int i = 0; i < keys.Length; i++)
            {
                if(x< keys[i])
                {
                    x1 = keys[i];
                }

                if (x > keys[i])
                {
                    x2 = keys[i];
                }
            }
        }

        
    }
}
