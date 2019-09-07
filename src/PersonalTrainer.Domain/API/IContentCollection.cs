using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.Content;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IContentCollection
    {
        IEnumerable<Picture> Pictures { get; }
        IEnumerable<IGallery> Galleries { get; }
        IEnumerable<string> GalleryNames { get; }

        void Load(string mediaDirectory);
        void Load(string mediaDirectory, string[] extensions);

        IGallery Gallery(string name);
        Picture GetPicture(string name);
        Picture GetPicture(string gallery, string name);
    }
}