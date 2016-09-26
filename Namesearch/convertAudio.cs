/* Takes a path to an audio source in FLAC format and
 * encodes it to base64 format and stores it at Google
 * Cloud Storage.
 * 
 * Display list of available buckets
 * Asks user which to use 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Namesearch
{    
    class convertAudio
    {
        const string BUCKET_NAME = "tvtranscribe";

        public static void convertToBase64(string audio_file_path, string audio_file_name)
        {
            string content = Convert.ToBase64String(File.ReadAllBytes(audio_file_path + audio_file_name));

            StorageSample.UploadStream(BUCKET_NAME, audio_file_name, content);
            // Upload to Google Cloud Storage

        }
    }
}
