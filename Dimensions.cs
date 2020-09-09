using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace LevelsAndAxesGridWithDimensions
{
    public partial class ExternalCommand
    {
        private List<Dimension> CreateDimensions(
                Document doc,
                GridParameters p,
                List<Grid> grid)
        {
            /*
             * This method create dimensions between grid axes,
             * for all orientations, one dimension line by one orientation
             */

            List<Dimension> dimensions = new List<Dimension>();

            // The view in which the dimensions will be drawn
            View view = GetView(doc);

            Orientation[] orientations = p.GetOrientations().ToArray();

            Dictionary<Orientation, List<Grid>> gridsByOrientations
                = DivideGridByOrientation(p, grid, orientations.ToList());

            foreach (Orientation orientation in orientations)
            {
                if (p.GetNumber(orientation) > 1)
                {
                    CreateDimensionForOrientation(
                        doc,
                        gridsByOrientations,
                        view,
                        p,
                        orientation);
                }
            }

            return dimensions;
        }

        private Dictionary<Orientation, List<Grid>> DivideGridByOrientation(
                GridParameters p, 
                List<Grid> grid, 
                List<Orientation> orientations)
        {
            Dictionary<Orientation, List<Grid>> gridsByOrientation 
                = new Dictionary<Orientation, List<Grid>>();

            int count = default;

            foreach (Orientation orientation in orientations)
            {
                int linesByOrientation = p.GetNumber(orientation);
                List<Grid> oneOrientationGrid = new List<Grid>();

                for (int i = 0, n = linesByOrientation; i < n; i++)
                {
                    oneOrientationGrid.Add(grid[count + i]);
                }

                count += linesByOrientation;

                gridsByOrientation.Add(orientation, oneOrientationGrid);
            }

            return gridsByOrientation;
        }

        private void CreateDimensionForOrientation(
                Document doc,
                Dictionary<Orientation, List<Grid>> grids,
                View view,
                GridParameters p,
                Orientation orientation)
        {
            List<Grid> gridByOrientation = grids[orientation];

            // Originale last points
            XYZ oFirst = gridByOrientation.First().Curve.GetEndPoint(0);
            XYZ oLast = gridByOrientation.Last().Curve.GetEndPoint(0);

            // Last points with offset
            XYZ endPointFirst = default;
            XYZ endPointLast = default;

            // Offset size as half of distance
            double offset = p.GetDistance(
                p.GetPerpendicularOrientation(orientation)) / 2;

            switch (orientation)
            {
                case Orientation.Vertical:
                    endPointFirst = new XYZ(oFirst.X, oFirst.Y + offset, oFirst.Z);
                    endPointLast = new XYZ(oLast.X, oLast.Y + offset, oLast.Z);
                    break;

                case Orientation.Horizontal:
                    endPointFirst = new XYZ(oFirst.X + offset, oFirst.Y, oFirst.Z);
                    endPointLast = new XYZ(oLast.X + offset, oLast.Y, oLast.Z);
                    break;
            }

            Line dimensionLine = Line.CreateBound(endPointFirst, endPointLast);

            ReferenceArray axesReferences = new ReferenceArray();

            foreach (Grid grid in gridByOrientation)
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
                        axesReferences.Append(line.Reference);
                    }
                }
            }

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
    }

}
