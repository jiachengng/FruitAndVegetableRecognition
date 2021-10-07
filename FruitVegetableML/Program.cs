using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Emgu.CV;

namespace FruitVegetableML
{
    public static class Program
    {
        /// <summary>
        /// SCENARIO
        /// Camera will only start and detect object when an item is placed on the weighing scale.
        /// weighingScaleStatus is to mimic the status of the scale. (0 no item is placed, 1 item is placed)
        /// 1. Item placed on weighing scale
        /// 2. Camera starts up and capture the image
        /// 3. Saving the captured image into desired folder*
        /// 4. Machine learning (item detection and prediction starts)
        /// 5. Display the probability of the image to the different number of categories.
        /// 6. System should only display the category with the highest probability. (NOT DONE YET)
        /// 
        /// *Images captured and saved in the folder can be used for further training.
        /// </summary>
        
        public static void Main()
        {
            //Assuming if an item is placed on the weighing scale
            int weighingScaleStatus = 1;
            int i = 0;

            //While an item is on the scale
            while (weighingScaleStatus == 1)
            {
                VideoCapture capture = new VideoCapture(); //create a camera captue
                Bitmap image = capture.QueryFrame().ToBitmap(); //take a picture

                //Saving photos into folder
                string filename = "file" + i;
                image.Save(filename);
                string FileName = System.IO.Path.Combine(@"C:\Users\jiacheng\Downloads", DateTime.Now.ToString("yyy-MM-dd-hh-mm-ss"));
                image.Save(FileName + ".jpg");
                string imageFilePath = filename;

                //Predicting item on the scale
                MakePredictionRequest(imageFilePath).Wait();

                //Stopping condition, if 1, scan and predict another item, if 0, stop the loop
                Console.Write("Another image? (1 or 0): ");
                weighingScaleStatus = Convert.ToInt32(Console.ReadLine());
                i++;
            }

            Console.WriteLine("\n\nHit ENTER to exit...");
            Console.ReadLine();

        }

        public static async Task MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid Prediction-Key.
            client.DefaultRequestHeaders.Add("Prediction-Key", "6e8dc9f09f5d4ffd9dafb9aaa33a8da2");

            // Prediction URL - replace this example URL with your valid Prediction URL.
            string url = "https://customvisionjc-prediction.cognitiveservices.azure.com/customvision/v3.0/Prediction/54e90267-0a30-4895-aabf-9e2b4cef2e9d/classify/iterations/Iteration3/image";

            HttpResponseMessage response;

            // Request body. Try this sample with a locally stored image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}