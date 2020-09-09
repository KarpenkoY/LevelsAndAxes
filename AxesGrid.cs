using System;
using System.Windows;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using System.Data;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using System.Diagnostics;

namespace LevelsAndAxesGridWithDimensions
{
    public partial class ExternalCommand
    {

        List<Grid> CreateAxesGrid(
            Document doc, 
            GridParameters p)
        {
            List<Grid> list = new List<Grid>();

            foreach (Orientation orientation in p.GetOrientations())
            {
                list.AddRange(CreateAxes(doc, p, orientation));
            }

            return list;
        }

        private List<Grid> CreateAxes(Document doc, GridParameters p, Orientation orientation)
        {
            List<Grid> list = new List<Grid>();
            Line line = default;
            Grid gridLine = default;

            double perpendicularSize = p.GetSize(p.GetPerpendicularOrientation(orientation));

            double startPoint = p.GetStartPoint(orientation);
            double endPoint = p.GetEndPoint(orientation);

            int number = p.GetNumber(orientation);
            double distance = p.GetDistance(orientation);

            using (Transaction transaction = new Transaction(doc))
            {
                try
                {

                    // Iteration start with one (1), not zero (0)!
                    for (int i = 1, n = number; i <= n; i++)
                    {
                        string name = default;
                        double offset = default;

                        if (distance > 0)
                        {
                            offset = (distance * i) - (perpendicularSize / 2);
                        }

                        double x0 = default;
                        double x1 = default;

                        double y0 = default;
                        double y1 = default;

                        switch (orientation)
                        {
                            case Orientation.Vertical:
                                x0 = x1 = offset;
                                y0 = startPoint;
                                y1 = endPoint;
                                name = "1";
                                break;
                            case Orientation.Horizontal:
                                x0 = startPoint;
                                x1 = endPoint;
                                y0 = y1 = offset;
                                name = "A";
                                break;
                        }
                        
                        transaction.Start($"Create {orientation.ToString().ToLower()} axis");

                        line = Line.CreateBound(
                            new XYZ(x0, y0, 0),
                            new XYZ(x1, y1, 0));

                        gridLine = Grid.Create(doc, line);

                        // Initialize grid axes elevation
                        double indent = GetElevationIndent();
                        double elevation = GetHighestElevation(doc);
                        gridLine.SetVerticalExtents(0 - indent, indent + elevation);

                        // Set name for first axis in orientation
                        if (i == 1)
                        {
                            ElementCategoryFilter gridsFilter = new ElementCategoryFilter(BuiltInCategory.OST_Grids);
                            FilteredElementCollector existingGrids = new FilteredElementCollector(doc)
                                .WherePasses(gridsFilter)
                                .WhereElementIsNotElementType();

                            List<string> existingGridsName = new List<string>();

                            foreach (Grid grid in existingGrids)
                            {
                                existingGridsName.Add(grid.Name);
                            }

                            if (existingGridsName.Contains(name))
                            {
                                string prefixName;

                                do
                                {
                                    SetPrefixForm setPrefixForm = new SetPrefixForm(orientation.ToString()) ;
                                    setPrefixForm.ShowDialog();

                                    if (setPrefixForm.DialogResult == DialogResult.Cancel)
                                    {
                                        throw new OperationCanceledException();
                                    }

                                    prefixName = $"{setPrefixForm.prefix} {name}";
                                }
                                while (existingGridsName.Contains(prefixName));

                                gridLine.Name = prefixName;
                            }
                            else
                            {
                                gridLine.Name = name;
                            }
                        }
                        
                        transaction.Commit();

                        list.Add(gridLine);
                    }
                }
                catch (ArgumentException)
                {
                    System.Windows.Forms.MessageBox.Show("The axes can't have same name");
                }
            }

            return list;
        }
    }
}
