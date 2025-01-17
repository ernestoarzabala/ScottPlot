﻿namespace ScottPlotCookbook.Recipes.PlotTypes;

public class SignalXY : ICategory
{
    public string Chapter => "Plot Types";
    public string CategoryName => "SignalXY Plot";
    public string CategoryDescription => "SignalXY are a high performance plot type " +
        "optimized for X/Y pairs where the X values are always ascending. " +
        "For large datasets SignalXY plots are much more performant than " +
        "Scatter plots (which allow unordered X points) but not as performant " +
        "as Signal plots (which require fixed spacing between X points).";

    public class SignalXYQuickstart : RecipeBase
    {
        public override string Name => "SignalXY Quickstart";
        public override string Description => "SignalXY plots are a high performance plot type " +
            "for X/Y data where the X values are always ascending.";

        [Test]
        public override void Execute()
        {
            // generate sample data with gaps
            List<double> xList = new();
            List<double> yList = new();
            for (int i = 0; i < 5; i++)
            {
                xList.AddRange(Generate.Consecutive(1000, first: 2000 * i));
                yList.AddRange(Generate.RandomSample(1000));
            }
            double[] xs = xList.ToArray();
            double[] ys = yList.ToArray();

            // add a SignalXY plot
            myPlot.Add.SignalXY(xs, ys);
        }
    }

    public class SignalXYGeneric : RecipeBase
    {
        public override string Name => "SignalXY Generic";
        public override string Description => "SignalXY plots support generic data types, " +
            "although double is typically the most performant.";

        [Test]
        public override void Execute()
        {
            // generate sample data with gaps
            List<int> xList = new();
            List<float> yList = new();
            for (int i = 0; i < 5; i++)
            {
                xList.AddRange(Generate.Consecutive(1000, first: 2000 * i).Select(x => (int)x));
                yList.AddRange(Generate.RandomSample(1000).Select(x => (float)x));
            }
            int[] xs = xList.ToArray();
            float[] ys = yList.ToArray();

            // add a SignalXY plot
            myPlot.Add.SignalXY(xs, ys);
        }
    }

    public class SignalXYRenderIndexes : RecipeBase
    {
        public override string Name => "Partial SignalXY Rendering";
        public override string Description => "Even if a SignalXY plot references a " +
            "large array of data, rendering can be limited to a range of values. If set," +
            "only the range of data between the minimum and maximum render indexes will be displayed.";

        [Test]
        public override void Execute()
        {
            double[] xs = Generate.Consecutive(1000);
            double[] ys = Generate.RandomWalk(1000);

            var sigAll = myPlot.Add.SignalXY(xs, ys);
            sigAll.Label = "Full";
            sigAll.Data.YOffset = 80;

            var sigLeft = myPlot.Add.SignalXY(xs, ys);
            sigLeft.Label = "Left";
            sigLeft.Data.YOffset = 60;
            sigLeft.Data.MaximumIndex = 700;

            var sigRight = myPlot.Add.SignalXY(xs, ys);
            sigRight.Label = "Right";
            sigRight.Data.YOffset = 40;
            sigRight.Data.MinimumIndex = 300;

            var sigMid = myPlot.Add.SignalXY(xs, ys);
            sigMid.Label = "Mid";
            sigMid.Data.YOffset = 20;
            sigMid.Data.MinimumIndex = 300;
            sigMid.Data.MaximumIndex = 700;

            myPlot.ShowLegend(Alignment.UpperRight);
            myPlot.Axes.Margins(top: .5);
        }
    }

    public class SignalXYOffset : RecipeBase
    {
        public override string Name => "SignalXY Offset";
        public override string Description => "A fixed offset can be applied to SignalXY plots.";

        [Test]
        public override void Execute()
        {
            double[] xs = Generate.Consecutive(1000);
            double[] ys = Generate.Sin(1000);

            var sig1 = myPlot.Add.SignalXY(xs, ys);

            var sig2 = myPlot.Add.SignalXY(xs, ys);
            sig2.Data.XOffset = 250;
            sig2.Data.YOffset = .5;
        }
    }

    public class VerticalSignalXY : RecipeBase
    {
        public override string Name => "Vertical SignalXY";
        public override string Description => "Although SignalXY plots typically " +
            "display data left-to-right, it is possible to use this plot type to " +
            "display data bottom-to-top.";

        [Test]
        public override void Execute()
        {
            double[] xs = Generate.Consecutive(1000);
            double[] ys = Generate.RandomWalk(1000);

            var sig1 = myPlot.Add.SignalXY(xs, ys);
            sig1.Data.Rotated = true;
        }
    }
}
