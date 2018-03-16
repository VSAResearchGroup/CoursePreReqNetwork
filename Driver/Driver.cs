#region Author Information
/*
 * Andrue Cashman
 * University of Washington Bothell
 * Undergraduate Research Winter 2018
 * Advisor: Dr. Erika Parsons
 * Project: Virtual Student Advisor
*/
#endregion

#region Driver Information
/*
 * Purpose: Showcases how to Create, Use, and Access information of the CourseNetwork Class of Objects
*/
#endregion

#region Libraries and Namespaces
using System;
using System.Collections.Generic;
using System.IO;
using VSA_2; //The library necessary to use the CourseNetwork
#endregion

namespace Driver
{
    class Driver
    {
        static void Main(string[] args)
        {
            #region Mandatory JSON Files (Can be updated with SQL Queries
            string rawcourses = File.ReadAllText("../../AllCourses.json");
            #region ALLCourses SQL query
            //  select CourseID from Course for JSON AUTO
            #endregion
            string rawpreqs = File.ReadAllText("../../PrereqNetwork.json");
            #region PrereqNetwork SQL Query
            //  select CourseID, GroupID, PrerequisiteID from Prerequisite for JSON AUTO;
            #endregion
            #endregion

            #region Building the CourseNetwork
            CourseNetwork courses = new CourseNetwork(rawcourses, rawpreqs);
            courses.BuildNetwork();
            #endregion

            #region Viewing the Entire Course Network
            courses.ShowNetwork();
            #endregion

            #region Viewing/Accessing Prerequisites
            List<CourseNode> targetCourse = new List<CourseNode>();
            targetCourse = courses.FindShortPath(199);
            courses.ShowShortList(targetCourse);
            Console.WriteLine("");
            #endregion
            
            #region Viewing/Accessing Prerequisites of Prerequisites
            List<CourseNode> prereq = new List<CourseNode>();
            for (int i = 1; i < targetCourse.Count; i++)
            {
                for (int j = 0; j < targetCourse[i].prereqs.Count; j++)
                {
                    Console.WriteLine("Group Path " + i + " Course #" + (j+ 1));
                    prereq = courses.FindShortPath(targetCourse[i].prereqs[j].prerequisiteID);
                    courses.ShowShortList(prereq);
                }
            }
            Console.WriteLine("");
            #endregion

            #region Viewing/Accessing Prerequisites of Prerequisites of Prerequisites
            List<CourseNode> prereqPreReq = new List<CourseNode>();
            for (int i = 1; i < targetCourse.Count; i++)
            {
                for (int j = 0; j < targetCourse[i].prereqs.Count; j++)
                {
                    Console.WriteLine("Group Path " + i + " Course #" + (j +1));
                    prereq = courses.FindShortPath(targetCourse[i].prereqs[j].prerequisiteID);
                    courses.ShowShortList(prereq); 
                    for (int x = 1; x < prereq.Count; x++)
                    {
                        for (int y = 0; y < prereq[x].prereqs.Count; y++)
                        {
                            Console.WriteLine("Subgroup Path " + x + " Course #" + (y + 1));

                            prereqPreReq = courses.FindShortPath(prereq[x].prereqs[y].prerequisiteID);
                            courses.ShowShortList(prereqPreReq);
                        }
                    }
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("");
            #endregion

            #region Viewing/Accessing Prerequisites of Prerequisites of Prerequisites of Prerequisites
            List<CourseNode> prereqThird = new List<CourseNode>();
            for (int i = 1; i < targetCourse.Count; i++)
            {
                for (int j = 0; j < targetCourse[i].prereqs.Count; j++)
                {
                    Console.WriteLine("Group Path " + i + " Course #" + (j + 1));
                    prereq = courses.FindShortPath(targetCourse[i].prereqs[j].prerequisiteID);
                    courses.ShowShortList(prereq);
                    for (int x = 1; x < prereq.Count; x++)
                    {
                        for (int y = 0; y < prereq[x].prereqs.Count; y++)
                        {
                            Console.WriteLine("Subgroup Path " + x + " Course #" + (y + 1));
                            prereqPreReq = courses.FindShortPath(prereq[x].prereqs[y].prerequisiteID);
                            courses.ShowShortList(prereqPreReq);
                            for (int w = 1; w < prereqPreReq.Count; w++)
                            {
                                for (int v = 0; v < prereqPreReq[w].prereqs.Count; v++)
                                {
                                    Console.WriteLine("Sub-Subgroup Path " + w + " Course #" + (v + 1));
                                    prereqThird = courses.FindShortPath(prereqPreReq[w].prereqs[v].prerequisiteID);
                                    courses.ShowShortList(prereqThird);
                                }
                            }
                            Console.WriteLine("");
                        }
                    }
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("");
            #endregion

            #region Review on List<CourseNode> Structure and Accessing/Manipulating Data
            if (targetCourse != null)
            {
                Console.WriteLine("Course Information");
                Console.WriteLine("\tCourse ID: " + targetCourse[0].courseID);
                #region trash attributes
                Console.WriteLine("\tGroup ID: " + targetCourse[0].groupID); //DO NOT USE (JUNK!)
                Console.WriteLine("\tPrerequisite ID: " + targetCourse[0].prerequisiteID); //DO NOT USE (JUNK!)
                if (targetCourse[0].prereqs == null) //DO NOT USE INVALID GROUP ID (JUNK!)
                {
                    Console.WriteLine("\tPrereqs List is Empty");
                }
                Console.WriteLine("");
                #endregion
                #region Accesing Information by Group ID
                if (targetCourse.Count > 1)
                {
                    for (int i = 1; i < targetCourse.Count; i++) //i is Group ID
                    {
                        #region information per Group Index
                        Console.WriteLine("Course ID: " + targetCourse[i].courseID); //usable for error checking
                        Console.WriteLine("Group ID: " + targetCourse[i].groupID); //IDENTIFIES CURRENT GROUP
                        Console.WriteLine("Prerequisite ID: " + targetCourse[i].prerequisiteID); //DO NOT USE (JUNK!)
                        #endregion

                        #region Prerequisite Information for Each Group
                        if (targetCourse[i].prereqs != null)
                        {
                            for (int j = 0; j < targetCourse[i].prereqs.Count; j++)
                            {
                                #region Prerequisite Data
                                Console.WriteLine("\tCourse ID: " + targetCourse[i].prereqs[j].courseID); //usable for error checking
                                Console.WriteLine("\tGroup ID: " + targetCourse[i].prereqs[j].groupID); //usable for path checking
                                Console.WriteLine("\tPrerequisite ID: " + targetCourse[i].prereqs[j].prerequisiteID); //Prerequisite ID
                                #endregion

                                #region Notes on Prerequisites of Prerequisites
                                if (targetCourse[i].prereqs[j].prereqs == null)
                                {
                                    Console.WriteLine("\tPrereqs List is Empty"); //Intentionally left blank
                                    #region On Manipulation of Structure for Prerequisites of Prerequisites
                                    //NOTE: If found necessary, can be used to store the prerequisites of the prerequisites, in which case
                                    //      this structure would then go a second level.
                                    //      See Above in "Viewing/Accessing Prerequsites of Prerequisites" for Structure Creation/Access
                                    //      More Specifically, the function call below: 
                                    //      targetCourse[i].prereqs[j].prereqs = courses.FindShortPath(targetCourse[i].prereqs[j].prerequisiteID)
                                    //      Would allow for the assignment of the prerequisites of the prerequisites to the original
                                    //      prerequisite string.
                                    //
                                    //      Also, Since the FindShortPath is an O(N) operation each level of prerequisites added to the structure
                                    //      is an additional O(N) process. Assigning the prerequisites of the prerequsites is therefore an
                                    //      O(N^2) operation. Going further to assigning the prerequisites of the prerequisites of the 
                                    //      prerequisites increases the complexity to O(N^3) operation. And So on and so on.
                                    #endregion
                                }
                                #endregion

                                Console.WriteLine("");
                            }
                        }
                        #endregion
                        Console.WriteLine("");
                    }
                    #endregion
                }
            }
            #endregion

            Console.ReadLine(); //Used like a System("Pause") to allow review of Console Output
        }
    }
}
