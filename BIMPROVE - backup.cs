using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System.Globalization;
using System.Diagnostics;

namespace Levels_and_Axis
{
    // Setting transaction and regeneration in manual mode
    [Transaction(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]

    public class ExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get application, document and units objects
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            Units units = doc.GetUnits();

            // Get length unit, using in project
            DisplayUnitType lengthUnit = units
                .GetFormatOptions(UnitType.UT_Length).DisplayUnits;

            // Show form for get data from user
            SetParametersForm form = new SetParametersForm(units);
            form.ShowDialog();


            // Parsing user input from form, convert to internal units, and place values in parameters sets
            /// Defining parameters sets
            GridParameters gridParameters = new GridParameters();
            LevelsParameters levelsParameters = new LevelsParameters();

            try
            {
                /// Parsing user input in numbers fields
                gridParameters.numVert = Int32.Parse(form.verticalAxesNumber);
                gridParameters.numHoriz = Int32.Parse(form.horizontalAxesNumber);
                levelsParameters.numLevel = Int32.Parse(form.levelsNumber);

                /// Parsing user input in distance fields and convert to internal units
                gridParameters.distVert = UnitUtils.ConvertToInternalUnits(
                    Double.Parse(form.verticalAxesDistance), lengthUnit);
                gridParameters.distHoriz = UnitUtils.ConvertToInternalUnits(
                    Double.Parse(form.horizontalAxesDistance), lengthUnit);
                levelsParameters.distLevel = UnitUtils.ConvertToInternalUnits(
                    Double.Parse(form.levelsDistance), lengthUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Create and start transaction group
            using (TransactionGroup transactionGroup = new TransactionGroup(doc))
            {
                transactionGroup.Start("Create levels, and axes grid with dimensions");

                // Get from document highest level elevation
                double highestElevation = GetHighestElevation(doc);

                if (levelsParameters.numLevel > 0 && levelsParameters.distLevel > 0)
                {
                    // Creating levels with user entered parameters 
                    CreateLevels(doc, levelsParameters, highestElevation);
                }

                
                // Creating axes grid with user entered parameters
                CreateAxesGrid(doc, gridParameters);
                    
                // Creating dimensions
                CreateDimensions(doc, gridParameters);

                // End transaction and apply changes
                transactionGroup.Assimilate();
            }

            // Notyfy app, that external command complete succeeded
            return Result.Succeeded;
        }

        // User data, presenting levels parameters
        public struct LevelsParameters
        {
            // Number of levels
            public int numLevel;

            // Distance between levels
            public double distLevel;
        }

        public enum Orientation
        {
            Horizontal,
            Vertical
        }

        // User data, presenting grid parameters
        public struct GridParameters
        {
            // Number of vertical axes
            public int numVert;

            // Distance between vertical axes
            public double distVert;

            // Number of horizontal axes
            public int numHoriz;

            // Distance between horizontal axes
            public double distHoriz;

            public Orientation GetPerpendicularOrientation(Orientation orientation)
            {
                if (orientation == Orientation.Vertical)
                {
                    return Orientation.Horizontal;
                }
                else
                {
                    return Orientation.Vertical;
                }
            }

            public int GetNumber(Orientation orientation)
            {
                switch (orientation)
                {
                    case Orientation.Vertical:
                        return numVert;

                    case Orientation.Horizontal:
                        return numHoriz;

                    default:
                        return default;
                }
            }

            public double GetDistance(Orientation orientation)
            {
                /*
                 * This method return distance, based on orientation
                 */

                switch (orientation)
                {
                    case Orientation.Vertical:
                        return distVert;

                    case Orientation.Horizontal:
                        return distHoriz;

                    default:
                        return default;
                }
            }

            public double GetSize(Orientation orientation)
            {
                /*
                 * This method calculate and return lenth for grids axis,
                 * vertical or horizontal
                 */

                int number = default;
                double distance = default;
                double alternativeDistance = default;
                
                // Initialize variables according to orientation
                switch (orientation)
                {
                    case Orientation.Vertical:
                        number =                numHoriz;
                        distance =              distHoriz;
                        alternativeDistance =   distVert;
                        break;

                    case Orientation.Horizontal:
                        number =                numVert;
                        distance =              distVert;
                        alternativeDistance =   distHoriz;
                        break;
                }


                // Length for grid axis based on perpendicular orientation values
                if (distance > 0)
                {
                    return (number * distance) + distance;
                }
                // Length for grid axes based on this orientation value
                else 
                {
                    return alternativeDistance * 2;
                }
            }

            public double GetStartPoint(Orientation orientation)
            {
                /*
                 * This method calculate start point for grids line, based on orientation
                 */

                double size = this.GetSize(orientation);
                return 0 - size / 2;
            }
            
            public double GetEndPoint(Orientation orientation)
            {
                /*
                 * This method calculate end point for grids line, based on orientation
                 */

                double size = this.GetSize(orientation);
                return size / 2;
            }
        }
        
        private List<Dimension> CreateDimensions(Document doc, GridParameters parameters)
        {
            /*
             * This method create dimensions between grid axes,
             * for all orientations, one dimension line by one orientation
             */

            List<Dimension> dimensions = new List<Dimension>();

            View view = doc.ActiveView;

            //Select all grids by filter
            ElementCategoryFilter gridsFilter = new ElementCategoryFilter(BuiltInCategory.OST_Grids);
            FilteredElementCollector existingGrids = new FilteredElementCollector(doc)
                .WherePasses(gridsFilter)
                .WhereElementIsNotElementType();

            //Variables to split grids into vertical and horizontal
            ReferenceArray verticalGrids = new ReferenceArray();
            ReferenceArray horizontalGrids = new ReferenceArray();
            List<Grid> gridsV = new List<Grid>();
            List<Grid> gridsH = new List<Grid>();

            //Check grids name and split in groups
            foreach (Grid grid in existingGrids)
            {
                Options options = new Options()
                {
                    ComputeReferences = true,
                    IncludeNonVisibleObjects = false,
                    View = view
                };

                foreach (GeometryObject geometry in grid.get_Geometry(options))
                {
                    if (geometry is Line)
                    {
                        Line line = geometry as Line;

                        string gridName = grid.Name;
                        if (gridName.All<char>(c => Char.IsDigit(c)))
                        {
                            verticalGrids.Append(line.Reference);
                            gridsV.Add(grid);
                        }
                        else
                        {
                            horizontalGrids.Append(line.Reference);
                            gridsH.Add(grid);
                        }
                    }
                }
            }

            if (parameters.numVert > 1)
            {
                CreateDimensionForOrientation(doc, gridsV, verticalGrids, view, Orientation.Vertical);
                /*
                // Retrieve vertical axes grid endpoints
                XYZ endPointV0 = gridsV.First().Curve.GetEndPoint(0);
                XYZ endPointV1 = gridsV.Last().Curve.GetEndPoint(0);

                // Create new line for place distance between vertical axes grid
                Line lineV = Line.CreateBound(endPointV0, endPointV1);

                // Do transaction
                using (Transaction transaction = new Transaction(doc))
                {
                    try
                    {
                        transaction.Start("Create dimensions between verticals");

                        //Create dimension
                        doc.Create.NewDimension(view, lineV, verticalGrids);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                */
            }

            if (parameters.numHoriz > 1)
            {
                CreateDimensionForOrientation(doc, gridsH, horizontalGrids, view, Orientation.Horizontal);
                /*
                // Retrieve horizontal axes grid endpoints
                XYZ endPointH0 = gridsH.First().Curve.GetEndPoint(0);
                XYZ endPointH1 = gridsH.Last().Curve.GetEndPoint(0);

                // Create new line for place distance between horizontal axes grid
                Line lineH = Line.CreateBound(endPointH0, endPointH1);
                
                // Do transaction
                using (Transaction transaction = new Transaction(doc))
                {
                    try
                    {
                        transaction.Start("Create dimensions between horizontals");

                        //Create dimension
                        doc.Create.NewDimension(view, lineH, horizontalGrids);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                */
            }
            return dimensions;
        }

        private void CreateDimensionForOrientation(
            Document doc, 
            List<Grid> axesOneOrientation, 
            ReferenceArray axesReferences, 
            View view, 
            Orientation orientation)
        {
            XYZ endPointFirst = axesOneOrientation.First().Curve.GetEndPoint(0);
            XYZ endPointLast = axesOneOrientation.Last().Curve.GetEndPoint(0);

            Line dimensionLine = Line.CreateBound(endPointFirst, endPointLast);

            using (Transaction transaction = new Transaction(doc))
            {
                try
                {
                    transaction.Start(
                        $"Create dimensions between axes in " +
                        $"{orientation.ToString().ToLower()} orientation"); ;

                    doc.Create.NewDimension(view, dimensionLine, axesReferences);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }


        List<Grid> CreateAxesGrid(Document doc, GridParameters param)
        {
            List<Grid> list = new List<Grid>();

            list.AddRange(CreateAxes(doc, param, Orientation.Vertical));
            list.AddRange(CreateAxes(doc, param, Orientation.Horizontal));

            return list;
        }

        private List<Grid> CreateAxes(Document doc, GridParameters p, Orientation orientation)
        {
            List<Grid> list = new List<Grid>();
            Line line;
            Grid gridLine;

            double perpendicularSize =  p.GetSize(p.GetPerpendicularOrientation(orientation));

            double startPoint =         p.GetStartPoint(orientation);
            double endPoint =           p.GetEndPoint(orientation);

            int number =                p.GetNumber(orientation);
            double distance =           p.GetDistance(orientation);

            using (Transaction transaction = new Transaction(doc))
            {
                try
                {
                    transaction.Start($"Create {orientation.ToString().ToLower()} axes");

                    // Iteration start with one (1), not zero (0)!
                    for (int i = 1, n = number; i <= n; i++)
                    {
                        string name =   default; 
                        double offset = (distance * i) - (perpendicularSize / 2);

                        double x0 =     default;
                        double x1 =     default;

                        double y0 =     default;
                        double y1 =     default;

                        switch (orientation)
                        {
                            case Orientation.Vertical:
                                x0 = x1 =   offset;
                                y0 =        startPoint;
                                y1 =        endPoint;
                                name =      "1";
                                break;
                            case Orientation.Horizontal:
                                x0 =        startPoint;
                                x1 =        endPoint;
                                y0 = y1 =   offset;
                                name =      "A";
                                break;
                        }

                        line = Line.CreateBound(
                            new XYZ(x0, y0, 0),
                            new XYZ(x1, y1, 0));

                        gridLine = Grid.Create(doc, line);

                        if (i == 1)
                        {
                            gridLine.Name = name;
                        }

                        list.Add(gridLine);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return list;
        }

        /*
        private List<Grid> CreateVerticalAxes(Document doc, GridParameters p)
        {
            List<Grid> list = new List<Grid>();
            Line line;
            Grid verticalGridLine;

            double verticalSize = p.GetSize(Orientation.Vertical);
            double horizontalSize = p.GetSize(Orientation.Horizontal);
            double startPoint = 0 - verticalSize / 2;
            double endPoint = verticalSize / 2;

            using (Transaction transaction = new Transaction(doc))
            {
                try
                {
                    transaction.Start("Create vertical axes");

                    // Iteration start with one (1), not zero (0)!
                    for (int i = 1, n = p.numVert; i <= n; i++)
                    {
                        double offset = (p.distVert * i) - (horizontalSize / 2);

                        line = Line.CreateBound(
                            new XYZ(offset, startPoint, 0), 
                            new XYZ(offset, endPoint, 0));
                
                        verticalGridLine = Grid.Create(doc, line);
                
                        if (i == 1)
                        {
                            verticalGridLine.Name = "1";
                        }

                        list.Add(verticalGridLine);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return list;
        }

        private List<Grid> CreateHorizontalAxes(Document doc, GridParameters p)
        {
            List<Grid> list = new List<Grid>();
            Line line;
            Grid horizontalGridLine;

            double verticalSize = p.GetSize(Orientation.Vertical);
            double horizontalSize = p.GetSize(Orientation.Horizontal);
            double startPoint = 0 - horizontalSize / 2;
            double endPoint = horizontalSize / 2;

            using (Transaction transaction = new Transaction(doc))
            {
                try
                {
                    transaction.Start("Create horizontal axes");

                    // Iteration start with one (1), not zero (0)!
                    for (int i = 1, n = p.numHoriz; i <= n; i++)
                    {
                        double offset = (p.distHoriz * i) - (verticalSize / 2);

                        line = Line.CreateBound(
                            new XYZ(startPoint, offset, 0),
                            new XYZ(endPoint, offset, 0));

                        horizontalGridLine = Grid.Create(doc, line);

                        if (i == 1)
                        {
                            horizontalGridLine.Name = "A";
                        }

                        list.Add(horizontalGridLine);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return list;
        }
        */
        
        List<Level> CreateLevels(Document doc, LevelsParameters p, double elevation)
        {
            List<Level> list = new List<Level>();
            Level level;

            using (Transaction transaction = new Transaction(doc))
            {
                try
                {
                    transaction.Start("Create levels");

                    for (int i = 1, n = p.numLevel; i <= n; i++)
                    {
                        level = Level.Create(doc, elevation + (p.distLevel * i));
                        list.Add(level);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

            return list;
        }
        double GetHighestElevation(Document doc)
        {
            FilteredElementCollector existingLevels = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.INVALID)
                .OfClass(typeof(Level));

            List<double> levelElevations = new List<double>();
            foreach (Level level in existingLevels)
            { 
                levelElevations.Add(level.Elevation);
            }

            return levelElevations.ToArray().Max();
        }
    }
}
