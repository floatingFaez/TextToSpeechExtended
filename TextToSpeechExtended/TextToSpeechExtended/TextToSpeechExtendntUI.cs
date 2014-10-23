using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;

namespace TextToSpeechExtended
{
    public partial class TextToSpeechExtendntUI : Form
    {
        private SpeechSynthesizer aSynthesizer;
        public TextToSpeechExtendntUI()
        {
            InitializeComponent();
            aSynthesizer = new SpeechSynthesizer();
            pauseButton.Enabled = false;
            resumeButton.Enabled = false;
            stopButton.Enabled = false;
            speechTextBox.ScrollBars = ScrollBars.Both;
        }

        private void speakButton_Click(object sender, EventArgs e)
        {
            aSynthesizer.Dispose();
            if (speechTextBox.Text!="")
            {
                aSynthesizer = new SpeechSynthesizer();
                aSynthesizer.SpeakAsync(speechTextBox.Text);
                aSynthesizer.Volume = 100;
                aSynthesizer.Rate = -5;
                statusLabel.Text = @"Speaking";
                pauseButton.Enabled = true;
                stopButton.Enabled = true;
                aSynthesizer.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(aSynthesizer_SpeakCompleted);
            }
            else
            {
                MessageBox.Show(@"Please enter some text in the textbox", @"Message", MessageBoxButtons.OK);
            }
        }
        void aSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            statusLabel.Text = @"Idle";
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (aSynthesizer != null)
            {
                if (aSynthesizer.State == SynthesizerState.Speaking)
                {
                    aSynthesizer.Pause();
                    statusLabel.Text = @"Paused";
                    resumeButton.Enabled = true;
                }
            }
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            if (aSynthesizer != null)
            {
                if (aSynthesizer.State == SynthesizerState.Paused)
                {
                    aSynthesizer.Resume();
                    statusLabel.Text = @"Speaking";
                }
                resumeButton.Enabled = false;
            } 
        }

        private void stopButton_Click(object sender, EventArgs e)
        {

            if (aSynthesizer != null)
            {
                aSynthesizer.Dispose();
                statusLabel.Text = @"Idle";
                pauseButton.Enabled = false;
                resumeButton.Enabled = false;
                stopButton.Enabled = false;
            } 
        }

        private void loadTextButton_Click(object sender, EventArgs e)
        {
            speechOpenFileDialog.ShowDialog();
        }

        private void speechOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            speechTextBox.Text = File.ReadAllText(speechOpenFileDialog.FileName.ToString());
        } 
       
    }
}
