using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.CloudSpeechAPI.v1beta1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
using System.Text;
using System.Threading;

namespace Namesearch
{
    class transcribe
    {
        const string TRANSCRIBED_TEXT = @"C:\Private\Data\FE\Opgave\transcribed_text.txt";

        // [START authenticating]
        static public CloudSpeechAPIService CreateAuthorizedClient()
        {
            GoogleCredential credential =
                GoogleCredential.GetApplicationDefaultAsync().Result;
            // Inject the Cloud Storage scope if required.
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    CloudSpeechAPIService.Scope.CloudPlatform
                });
            }
            return new CloudSpeechAPIService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DotNet Google Cloud Platform Speech Sample",
            });
        }
        // [END authenticating]

        // [START run_application]
        static public void audio2text()
        {
            //if (args.Count() < 1)
            //{
            //    Console.WriteLine("Usage:\nTranscribe audio_file");
            //    return;
            //}
            var service = CreateAuthorizedClient();
            //string audio_file_path = args[0];
            // [END run_application]
            // [START construct_request]
            var request = new Google.Apis.CloudSpeechAPI.v1beta1.Data.AsyncRecognizeRequest()
            {
                Config = new Google.Apis.CloudSpeechAPI.v1beta1.Data.RecognitionConfig()
                {
                    Encoding = "LINEAR16",
                    SampleRate = 44100,
                    LanguageCode = "da-DK"
                },
                Audio = new Google.Apis.CloudSpeechAPI.v1beta1.Data.RecognitionAudio()
                {
                    //Content = Convert.ToBase64String(File.ReadAllBytes(audio_file_path))                  
                    Uri = "gs://tvtranscribe/de_sorte_spejdere_ep1_2min_mono.flac"
                }
            };
            // [END construct_request]
            // [START send_request]
            var asyncResponse = service.Speech.Asyncrecognize(request).Execute();
            var name = asyncResponse.Name;
            Google.Apis.CloudSpeechAPI.v1beta1.Data.Operation op;
            do
            {
                Console.WriteLine("Waiting for server processing...");
                Thread.Sleep(1000);
                op = service.Operations.Get(name).Execute();
            } while (!(op.Done.HasValue && op.Done.Value));
            dynamic results = op.Response["results"];
            foreach (var result in results)
            {
                foreach (var alternative in result.alternatives)
                    Console.WriteLine(alternative.transcript);
            }

            if ((!File.Exists(TRANSCRIBED_TEXT))) //Checking if 
            {
                FileStream fs = File.Create(TRANSCRIBED_TEXT); //Creates Scores.txt
                fs.Close(); //Closes file stream
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(TRANSCRIBED_TEXT))
            {
                foreach (var result in results)
                {
                    foreach (var alternative in result.alternatives)
                        file.WriteLine(alternative.transcript);
                }              
            }
            // [END send_request]
        }
    }
}
