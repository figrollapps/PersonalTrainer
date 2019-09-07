using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.Content;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IGallery
    {
        string Name { get; }
        IEnumerable<Picture> Pictures { get; }
    }
}