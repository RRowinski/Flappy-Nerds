public class KinectData
{
    // Make a global variable for the kinect sensor so that all parts of the program can use it
    public static KinectSensor Kinect;

    const int skeletonCount = 1;
    public static Skeleton[] allSkeletons = new Skeleton[skeletonCount];



    public static void StopKinect() // Stops the kinect sensor
    {
        if (Kinect != null)
        {
            Kinect.Stop();
            Kinect.AudioSource.Stop();
        }
    }

    
}