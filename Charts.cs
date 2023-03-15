﻿using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;
using SplitAndMerge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCSCS
{
    public class Charts
    {
        public void Init(CSCS_GUI gui)
        {
            Gui = gui;
            Interpreter interpreter = gui.Interpreter;

            interpreter.RegisterFunction(Constants.CHART, new ChartFunction());
            

        }

        CSCS_GUI Gui { get; set; }

        class ChartFunction : ParserFunction
        {
            static Dictionary<string, string> chartsTypes = new Dictionary<string, string>();
            protected override Variable Evaluate(ParsingScript script)
            {
                List<Variable> args = script.GetFunctionArgs();
                Utils.CheckArgs(args.Count, 2, m_name);

                var gui = CSCS_GUI.GetInstance(script);

                var widgetName = Utils.GetSafeString(args, 0).ToLower();
                var optionString = Utils.GetSafeString(args, 1).ToLower();
                var valueVariable = Utils.GetSafeVariable(args, 2);
                var value2Variable = Utils.GetSafeVariable(args, 3);
                if (value2Variable == null)
                    value2Variable = new Variable();
                var value3Variable = Utils.GetSafeVariable(args, 4);
                if (value3Variable == null)
                    value3Variable = new Variable();

                var widget = gui.GetWidget(widgetName);
                if (widget is CartesianChart)
                {
                    var cartesianWidget = widget as CartesianChart;

                    if (optionString == "seriestype")
                    {
                        chartsTypes[widgetName] = valueVariable.String.ToLower();
                    }
                    else if (optionString == "init")
                    {
                        cartesianWidget.Series = new ISeries[] { };
                    }
                    else if (optionString == "values")
                    {
                        if (valueVariable.Tuple.Count > 0)
                        {
                            List<double> newList = new List<double>();

                            foreach (var item in valueVariable.Tuple)
                            {
                                newList.Add(item.Value);
                            }

                            var temp = cartesianWidget.Series.ToList();
                            if (chartsTypes[widgetName] == "columnseries")
                            {
                                temp.Add(new ColumnSeries<double>() { Values = newList });
                            }
                            else if (chartsTypes[widgetName] == "lineseries")
                            {
                                temp.Add(new LineSeries<double>()
                                {
                                    Values = newList,
                                    //TooltipLabelFormatter = (chartPoint) => $"{newList[(int)chartPoint.Context.Series.Name.SecondaryValue]}" + $": chartPoint.Context.Series.Name}: {chartPoint.PrimaryValue:C0}"
                                    //TooltipLabelFormatter = (chartPoint) => $"{chartPoint.Context.Series.Name} - {} - {newList[(int)chartPoint.SecondaryValue]}"
                                    TooltipLabelFormatter = (chartPoint) => $"{newList[(int)chartPoint.SecondaryValue].ToString("N")}",
                                    //Fill = new SolidColorPaint(SKColors.Transparent)
                                    Fill = null,
                                    //GeometryFill = null,
                                    //GeometryStroke = null,
                                    GeometrySize = 7

                                });
                            }
                            if (!string.IsNullOrEmpty(value2Variable.String))
                            {
                                temp.Last().Name = value2Variable.String;
                            }

                            cartesianWidget.Series = temp;
                        }
                    }
                    else if (optionString == "xaxisname")
                    {
                        cartesianWidget.XAxes.First().Name = valueVariable.String;
                    }
                    else if (optionString == "yaxisname")
                    {
                        cartesianWidget.YAxes.First().Name = valueVariable.String;
                    }
                    else if (optionString == "labels")
                    {
                        if(valueVariable.String?.ToLower() == "x")
                        {
                            cartesianWidget.XAxes.First().Labels = value3Variable.Tuple.Select(p => p.String).ToList();
                            cartesianWidget.XAxes.First().TextSize = value2Variable.Value != 0 ? value2Variable.Value : 15;
                        }
                        else if (valueVariable.String?.ToLower() == "y")
                        {
                            cartesianWidget.YAxes.First().TextSize = value2Variable.Value != 0 ? value2Variable.Value : 15;
                        }
                        
                    }
                    else if (optionString == "xlabelsrotation")
                    {
                        cartesianWidget.XAxes.First().LabelsRotation = valueVariable.Value;
                    }
                    else if (optionString == "ylabelsrotation")
                    {
                        cartesianWidget.YAxes.First().LabelsRotation = valueVariable.Value;
                    }
                    else if (optionString == "title")
                    {
                        cartesianWidget.Title = new LabelVisual()
                        {
                            Text = valueVariable.String,
                            TextSize = value2Variable.Value != 0 ? value2Variable.Value : 20,
                            Padding = new LiveChartsCore.Drawing.Padding(15),
                            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
                        };
                    }
                    else if(optionString == "separatorstep")
                    {
                        var firstXAxis = cartesianWidget.XAxes.FirstOrDefault();
                        if (firstXAxis != null)
                        {
                            firstXAxis.MinStep = valueVariable.Value;
                            firstXAxis.ForceStepToMin = true;
                        }
                    }
                    else if(optionString == "margins")
                    {
                        cartesianWidget.DrawMargin = new LiveChartsCore.Measure.Margin((float)valueVariable.Tuple[0].Value, (float)valueVariable.Tuple[1].Value, (float)valueVariable.Tuple[2].Value, (float)valueVariable.Tuple[3].Value);
                    }
                    else if(optionString == "tooltipdecimalplaces")
                    {
                        var aljksd = cartesianWidget.ToolTip;

                        foreach (var series in cartesianWidget.Series)
                        {
                            if (chartsTypes[widgetName] == "columnseries")
                            {
                                (series as ColumnSeries<double>).TooltipLabelFormatter = (chartPoint) => $"{chartPoint.PrimaryValue.ToString($"N{valueVariable.Value}")}";
                            }
                            else if (chartsTypes[widgetName] == "lineseries")
                            {
                                (series as LineSeries<double>).TooltipLabelFormatter = (chartPoint) => $"{chartPoint.PrimaryValue.ToString($"N{valueVariable.Value}")}";
                            }
                        }

                    }
                    
                }

                return Variable.EmptyInstance;
            }
        }
    }
}
