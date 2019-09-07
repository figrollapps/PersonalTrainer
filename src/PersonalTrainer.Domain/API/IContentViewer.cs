using System;
using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.Content;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IContentViewer
    {
        void Load(IGallery gallery);
        void Load(IEnumerable<Picture> pictures);

        void Display(Picture picture);
        void Display(Picture picture, int thenPause);
        void Display(int pauseThen, Picture picture);

        void Clear();
        void Clear(int thenPause);

        void PlaySlideShow(int displaySeconds);
        void PlaySlideShow(IGallery gallery, int displaySeconds);
        void PlaySlideShow(IEnumerable<Picture> slideShowPictures, int displaySeconds);
        void PlaySlideShow(Func<int> calculateDisplaySeconds);
        void PlaySlideShow(IGallery gallery, Func<int> calculateDisplaySeconds);
        void PlaySlideShow(IEnumerable<Picture> slideShowPictures, Func<int> calculateDisplaySeconds);

        void WaitUntilComplete();

        event EventHandler SlideShowStarted;
        event EventHandler<PictureEventArgs> PictureChanged;
        event EventHandler SlideShowComplete;
    }
}