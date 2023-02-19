using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    public GameObject pathObject;
    public KeyCode screenshotButton;

    private void Update()
    {
        if (Input.GetKeyUp(screenshotButton))
        {
            /*
            string path = AssetDatabase.GetAssetPath(pathObject);
            string folderPath = path.Replace(pathObject.name + ".prefab", "");
            int randomNumber = Random.Range(1,1000);
            string fileName = Path.Combine(folderPath, "revoid-" + randomNumber + ".png");
            ScreenCapture.CaptureScreenshot(fileName);
            Debug.Log("Screenshot taken: " + fileName);
            */
        }
    }
}
