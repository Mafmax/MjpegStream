using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MjpegStreamReciever.Model
{
    public class FolderPictureDataProvider : DataProvider
    {
        private Random rnd = new Random();

        public override byte[] Provide(string source)
        {
            string randomPictureName = GetRandomPictureName(source);

            using(var stream = File.OpenRead(randomPictureName))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return data;
            }
        }
        private string GetRandomPictureName(string folder)
        {
            List<string> allNames = new List<string>(GetAllPicturesNamesInFolder(folder));
            return allNames[rnd.Next(0, allNames.Count)];
        }
        private IEnumerable<string> GetAllPicturesNamesInFolder(string folder)
        {
            foreach (var file in Directory.GetFiles(folder))
            {
                switch (Path.GetExtension(file))
                {
                    case ".png":
                    case ".jpeg":
                    case ".jpg":
                        yield return file; continue;
                    default: continue;
                }
            }
        }
    }
}
