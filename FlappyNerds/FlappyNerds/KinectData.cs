using Microsoft.Kinect;

public class KinectData
{
    // Make a global variable for the kinect sensor so that all parts of the program can use it
    public static KinectSensor kinect = null;

    public static Skeleton skeletonData = new Skeleton;

    private int leftHandY;
    private int rightHandY;
    private int headY;
    private int hipCentreY;

    void StartKinect()
    {
        kinect = KinectSensor.KinectSensors.FirstOrDefault(s => s.Status == KinectStatus.Connected); // Get first Kinect Sensor
        kinect.SkeletonStream.Enable(); // Enable skeletal tracking

        skeletonData = new Skeleton[kinect.SkeletonStream.FrameSkeletonArrayLength]; // Allocate ST data

        kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady); // Get Ready for Skeleton Ready Events

        kinect.Start(); // Start Kinect sensor
    }

    public static void StopKinect() // Stops the kinect sensor
    {
        if (kinect != null)
        {
            kinect.Stop();
            kinect.AudioSource.Stop();
        }
    }

    private void UpdateSkeleton(object sender, SkeletonFrameReadyEventArgs e)
    {
        using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame()) // Open the Skeleton frame
        {
            if (skeletonFrame != null && this.skeletonData != null) // check that a frame is available
            {
              skeletonFrame.CopySkeletonDataTo(this.skeletonData); // get the skeletal information in this frame
            }
        }
    }

    public static int GetLeftHandY()
    {
        return leftHandY;
    }

    public static int GetRightHandY()
    {
        return rightHandY;
    }

    public static int GetHeadY()
    {
        return headY;
    }

    public static int GetHipY()
    {
        return hipCentreY;
    }

    
}