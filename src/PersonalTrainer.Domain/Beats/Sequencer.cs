using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain.Beats
{
    public class Sequencer : Metronome, ISequencer
    {
        private readonly Dictionary<int, bool> pattern = new Dictionary<int, bool>();
        private int beatCursor;

        public void SetPattern(string pattern)
        {
            // "1+3+"
            // "1234"
            // "+3+"
            // "1+2+3+4+"
            this.pattern.Clear();
            this.pattern.Add(1, false);
            this.pattern.Add(2, false);
            this.pattern.Add(3, false);
            this.pattern.Add(4, false);
            this.pattern.Add(5, false);
            this.pattern.Add(6, false);
            this.pattern.Add(7, false);
            this.pattern.Add(8, false);

            if (pattern.Contains("1"))
            {
                this.pattern[1] = true;
            }
            if (pattern.Contains("2"))
            {
                this.pattern[3] = true;
            }
            if (pattern.Contains("3"))
            {
                this.pattern[5] = true;
            }
            if (pattern.Contains("4"))
            {
                this.pattern[7] = true;
            }

            if (pattern.Contains("1+") || pattern.Contains("+2"))
            {
                this.pattern[2] = true;
            }
            if (pattern.Contains("2+") || pattern.Contains("+3"))
            {
                this.pattern[4] = true;
            }
            if (pattern.Contains("3+") || pattern.Contains("+4"))
            {
                this.pattern[6] = true;
            }
            if (pattern.Contains("4+"))
            {
                this.pattern[8] = true;
            }
        }

        protected override void DoPlay()
        {
            beatCursor = 0;
            var millisecondsPerBeat = (int) (1000.0 / (BPM * 2 / 60.0));
            BeatTimer.Change(millisecondsPerBeat, millisecondsPerBeat);
        }

        protected override void OnTick(object state)
        {
            beatCursor++;

            var lastBeatInBar = beatCursor == 8;
            var beatShouldPlay = pattern[beatCursor];

            if (lastBeatInBar && !beatShouldPlay)
            {
                EndBar();
            }
            else if (lastBeatInBar)
            {
                PlayBeat(true);
            }
            else if (beatShouldPlay)
            {
                PlayBeat(false);
            }

            if (lastBeatInBar)
            {
                beatCursor = 0;
            }
        }
    }
}