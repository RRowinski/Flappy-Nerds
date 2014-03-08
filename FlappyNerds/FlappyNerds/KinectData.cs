using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;

public class KinectData
{
    // Make a global variable for the kinect sensor so that all parts of the program can use it
    public static KinectSensor kinect = null;

    public static Skeleton[] skeletonData;

    private static float leftHandY;
    private static float rightHandY;
    private static float headY;
    private static float hipCentreY;
    private static bool playerFound;

    public KinectData()
    {
        kinect = null;
        leftHandY = 0;
        rightHandY = 0;
        headY = 0;
        hipCentreY = 0;
        playerFound = false;
    }

    public void StartKinect()
    {
        kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected); // Get first Kinect Sensor
        kinect.SkeletonStream.Enable(); // Enable skeletal tracking

        skeletonData = new Skeleton[6]; // Allocate ST data

        kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(Kinect_AllFramesReady); // Get Ready for Skeleton Ready Events

        kinect.Start(); // Start Kinect sensor
    }

    public void StopKinect() // Stops the kinect sensor
    {
        if (kinect != null)
        {
            kinect.Stop();
            kinect.AudioSource.Stop();
        }
    }

    public void Kinect_AllFramesReady(object sender, AllFramesReadyEventArgs e)
    {
        #region //Get a skeleton

        Skeleton first = GetFirstSkeleton(e);

        if (first != null)
        {
            leftHandY = first.Joints[JointType.HandLeft].Position.Y;
            rightHandY = first.Joints[JointType.HandRight].Position.Y;
            headY = first.Joints[JointType.Head].Position.Y;
            hipCentreY = first.Joints[JointType.HipCenter].Position.Y;
            playerFound = true;
            Console.WriteLine(leftHandY);
        }
        else
        {
            playerFound = false;
        }
        #endregion
    }

    private Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
    {
        using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
        {
            if (skeletonFrameData == null)
            {
                return null;
            }
            skeletonFrameData.CopySkeletonDataTo(KinectData.skeletonData);

            //get the first tracked skeleton
            Skeleton first = (from s in KinectData.skeletonData
                              where s.TrackingState == SkeletonTrackingState.Tracked
                              select s).FirstOrDefault();
            return first;
        }
    }

    /*
    public void Kinect_AllFramesReady(object sender, AllFramesReadyEventArgs e)
    {
        using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame()) // Open the Skeleton frame
        {
            if (skeletonFrame != null && skeletonData != null) // check that a frame is available
            {
                skeletonFrame.CopySkeletonDataTo(skeletonData); // get the skeletal information in this frame
                Console.WriteLine("I get here!");
                leftHandY = skeletonData[0].Joints[JointType.HandLeft].Position.Y;
                rightHandY = skeletonData[0].Joints[JointType.HandRight].Position.Y;
                headY = skeletonData[0].Joints[JointType.Head].Position.Y;
                hipCentreY = skeletonData[0].Joints[JointType.HipCenter].Position.Y;
            }
        }
    }*/

    public float GetLeftHandY()
    {
        return leftHandY;
    }

    public float GetRightHandY()
    {
        return rightHandY;
    }

    public float GetHeadY()
    {
        return headY;
    }

    public float GetHipY()
    {
        return hipCentreY;
    }

    public bool IsPlayerFound()
    {
        return playerFound;
    }
}