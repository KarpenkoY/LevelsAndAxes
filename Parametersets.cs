using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace LevelsAndAxesGridWithDimensions
{
    public partial class ExternalCommand
    {
        public void InitializeParameters(
                                         SetParametersForm form,
                                         ref GridParameters gridParameters, 
                                         ref LevelsParameters levelsParameters,
                                         DisplayUnitType unit)
        {
            /*
             * This method parse user input from form, convert to internal units, 
             * and place values in parameters sets
             */

            /// Parsing user input in numbers fields
            gridParameters.numVert = Int32.Parse(form.verticalAxesNumber);
            gridParameters.numHoriz = Int32.Parse(form.horizontalAxesNumber);
            levelsParameters.numLevel = Int32.Parse(form.levelsNumber);

            /// Parsing user input in distance fields and convert to internal units
            gridParameters.distVert = UnitUtils.ConvertToInternalUnits(
                Double.Parse(form.verticalAxesDistance), unit);

            gridParameters.distHoriz = UnitUtils.ConvertToInternalUnits(
                Double.Parse(form.horizontalAxesDistance), unit);

            levelsParameters.distLevel = UnitUtils.ConvertToInternalUnits(
                Double.Parse(form.levelsDistance), unit);
        }


        public struct LevelsParameters
        {
            /*
             * This structure presenting levels parameters
             */

            // Number of levels
            public int numLevel;

            // Distance between levels
            public double distLevel;

            // Whether levels will be created
            public bool CreateALevels
            {
                get 
                { 
                    return (numLevel > 0) && (distLevel > 0); 
                }
            }
        }

        public struct GridParameters
        {
            /*
             * This structure presenting axes grid parameters;
             * contain methods for calculate values based on this parameters
             */

            // Number of vertical axes
            public int numVert;

            // Distance between vertical axes
            public double distVert;

            // Number of horizontal axes
            public int numHoriz;

            // Distance between horizontal axes
            public double distHoriz;

            // Whether grid will be created
            
            public List<Orientation> GetOrientations()
            {
                List<Orientation> orientations = new List<Orientation>();

                if (CreateAHorizontals)
                {
                    orientations.Add(Orientation.Horizontal);
                }

                if (CreateAVerticals)
                {
                    orientations.Add(Orientation.Vertical);
                }

                return orientations;
            }
            
            public bool CreateAGrid
            {
                get
                {
                    return CreateAVerticals || CreateAHorizontals;
                }
            }
            
            // Whether verticals will be created
            public bool CreateAVerticals
            {
                get
                {
                    return (numVert > 0) && (distVert > 0);
                }
            }

            // Whether horizontals will be created
            public bool CreateAHorizontals
            {
                get
                {
                    return (numHoriz > 0) && (distHoriz > 0);
                }
            }

            public Orientation GetPerpendicularOrientation(Orientation orientation)
            {
                /*
                 * This method return the orientation, perpendicular to the presented
                 */
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
                /*
                 * This method return number, based on orientation
                 */
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
                        number = numHoriz;
                        distance = distHoriz;
                        alternativeDistance = distVert;
                        break;

                    case Orientation.Horizontal:
                        number = numVert;
                        distance = distVert;
                        alternativeDistance = distHoriz;
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

        public enum Orientation
        {
            Horizontal,
            Vertical
        }
    }
}
